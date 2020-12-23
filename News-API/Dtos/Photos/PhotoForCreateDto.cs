
using System;
using Microsoft.AspNetCore.Http;

namespace News_API.Dtos.Photos
{
    public class PhotoForCreateDto
    {
        public IFormFile File { get; set; }
        public string Url { get; set; }
        public DateTime DateAdded { get; set; }
        public string publicId { get; set; }
        // public int ProductId { get; set; }
        public PhotoForCreateDto()
        {
          DateAdded = DateTime.Now;  
        }

    }
}