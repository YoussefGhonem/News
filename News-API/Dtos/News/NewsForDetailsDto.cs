using System;
using System.Collections.Generic;
using News_API.Dtos.Photos;
using News_API.Models;

namespace News_API.Dtos.News
{
    public class NewsForDetailsDto
    {
        public int Id { get; set; }
        public string Headline { get; set; }
        public string Extract { get; set; }
        public string Text { get; set; }
        public DateTime PublishDate { get; set; }
        public string Source { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string AuthorName { get; set; }
        public string AuthorBio { get; set; }
         public string PhotoURL { get; set; }
        public ICollection<PhotoForDetailsDto> photos{ get; set; }
    }
}