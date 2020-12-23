using System;
using System.ComponentModel.DataAnnotations;

namespace News_API.Dtos.News
{
    public class AddNewsDto
    {
        public string Headline { get; set; }
        public string Extract { get; set; }
        public string Text { get; set; }
        public DateTime PublishDate { get; set; }

        [Url(ErrorMessage="Please enter a valid url")]
        public string Source { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string AuthorName { get; set; }
        public string AuthorBio { get; set; }
        public int UserId { get; set; }
        public AddNewsDto()
        {
            this.PublishDate=DateTime.Now;
        }
    }
}