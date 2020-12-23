using System;
using System.ComponentModel.DataAnnotations;

namespace News_API.Dtos.News
{
    public class UpdateNewsDto
    {
         public string Headline { get; set; }
        public string Extract { get; set; }
        public string Text { get; set; }
        

        [Url(ErrorMessage="Please enter a valid url")]
        public string Source { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string AuthorName { get; set; }
        public string AuthorBio { get; set; }


    }
}