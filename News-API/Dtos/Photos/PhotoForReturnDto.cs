
using System;

namespace News_API.Dtos.Photos
{
    public class PhotoForReturnDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }  // Cloudinary 
        public int ProductId { get; set; }
        public bool IsMain { get; set; }
  

    }
}