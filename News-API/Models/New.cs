using System.Collections.Generic;
using System;

namespace News_API.Models
{
    public class New
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
        public ICollection<Photo> Photos{ get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        
        





    }
}