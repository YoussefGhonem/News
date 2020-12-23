
using System.Linq;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using News_API.Data;

using System.Net;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using News_API.Dtos.Photos;
using News_API.Helpers;
using News_API.Models;

namespace News_API.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INewsRepo _repo;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;   // Cloudinary setting     
        public PhotosController(IMapper mapper, INewsRepo repo, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _mapper = mapper;
            _repo = repo;
            _cloudinaryConfig = cloudinaryConfig;

            // Cloudinary setting
            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(acc);
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhotoForNews(int id)
        {
            var photoFromDB = await _repo.GetPhotoForNews(id);
            var photoReturn = _mapper.Map<PhotoForReturnDto>(photoFromDB);
            return Ok(photoReturn);

        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddPhotoForNews(int id, [FromForm] PhotoForCreateDto photoForCreateDto)
        {
            var getProductFromDB = await _repo.GetNews(id);
            var file = photoForCreateDto.File;
            var uploadResult = new ImageUploadResult();
            if (file != null && file.Length > 0)
            {

                using (var stream = file.OpenReadStream())// بتمكنلي من اني اقرأ اي ملف جاي من استريم
                {//  استخدمت يوزينج علشان دي بعد ما اخد الاستريم اللي انا محتاجه بيتم حذفه من الميموري علشان مايخدش مساحه ويعمل بطىء

                    var uploadParams = new ImageUploadParams() // هنا بيتم رفعها على الكلاود
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation()
                         .Width(500).Height(500).Crop("fill").Gravity("face")
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);// دي بترجعلنا كل حاجه اللي هستقبلها ف الانجلر
                }

            }
            photoForCreateDto.Url = uploadResult.Uri.ToString();
            photoForCreateDto.publicId = uploadResult.PublicId;
            var photo = _mapper.Map<Photo>(photoForCreateDto);
            if (!getProductFromDB.Photos.Any(p => p.IsMain))
                photo.IsMain = true;

            getProductFromDB.Photos.Add(photo);

            if (await _repo.SaveAll())
            {
                var PhotoToReturn = _mapper.Map<PhotoForReturnDto>(photo);

                return CreatedAtRoute("GetPhoto", new { id = photo.Id }, PhotoToReturn);
            }

            return BadRequest(" Uploaded Failed  ");

        }

        [HttpPost("{newsId}/{id}/setMain")]
        public async Task<IActionResult> SetMainPhoto(int id, int newsId)
        {
            var DesiredMainPhoto = await _repo.GetPhotoForNews(id);
            if (DesiredMainPhoto.IsMain)
                return BadRequest("This Is Main Photo");

            var CurrentMainPhoto = await _repo.GetMainPhotoForUser(newsId);

            CurrentMainPhoto.IsMain = false;
            DesiredMainPhoto.IsMain = true;


            if (await _repo.SaveAll())
                return NoContent();
            return BadRequest("Sorry You Can not Updated");

        }

        [HttpDelete("{newsId}/delete/{id}")]
        public async Task<IActionResult> deleteNewsPhoto(int newsId, int id)
        {
            var NewsFromRepo = await _repo.GetNews(newsId);
            if (!NewsFromRepo.Photos.Any(p => p.Id == id))
                return Unauthorized();

            var photo = await _repo.GetPhotoForNews(id);

            if (photo.PublicId != null)
            {
                var deletePhoto = new DeletionParams(photo.PublicId);
                var result = this._cloudinary.Destroy(deletePhoto);
                if (result.Result == "ok")
                {
                    _repo.Delete(photo);
                }
            }
            if (photo.PublicId == null)
            {
                _repo.Delete(photo);
            }
            if (await _repo.SaveAll())
                return Ok();
            return BadRequest("Delete Photo Fiald");

        }
    }
}