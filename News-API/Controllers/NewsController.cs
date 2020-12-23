using System.Linq;
using System;
using System.Threading.Tasks;
using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using News_API.Data;
using News_API.Dtos.User;
using News_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using News_API.Dtos.News;
using News_API.Helpers;

namespace News_API.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INewsRepo _repo;

        public NewsController(IMapper mapper, INewsRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("{id}", Name = "GetNews")]
        public async Task<IActionResult> GetNews(int id)
        {
            var news = await _repo.GetNews(id);
            var newsReturn = _mapper.Map<NewsForDetailsDto>(news);
            return Ok(newsReturn);
        }
        
       
       [HttpGet]
        public async Task<IActionResult> GetAllNews([FromQuery] NewsParams newsParams)
        {
            var news = await _repo.GetAllNews(newsParams);
            var newsReturn = _mapper.Map<IEnumerable<NewsForListDto>>(news);
            Response.AddPagination(news.CurrentPage,news.PageSize,news.TotalCount,news.TotalPages);
            return Ok(newsReturn);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNews(AddNewsDto addNewsDto)
        {
            if (ModelState.IsValid)
            {
                addNewsDto.UserId = 1;
                var news = _mapper.Map<New>(addNewsDto);
                _repo.Add(news);
            }
            if (await _repo.SaveAll()) return NoContent();
            return BadRequest("Sorry Can not Save Changes");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,UpdateNewsDto updateNewsDto){
            var news=await _repo.GetNews(id);
            _mapper.Map(updateNewsDto,news);
            if(await _repo.SaveAll())return NoContent();
        
         return BadRequest("Sorry Can not Save Changes");
        }
    }
}