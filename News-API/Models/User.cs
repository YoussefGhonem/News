using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace News_API.Models
{
    public class User:IdentityUser<int>
    {
         public string Bio { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection <New> News{ get; set; }
        
        
    }
}