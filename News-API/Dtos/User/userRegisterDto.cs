using System.ComponentModel.DataAnnotations;

namespace News_API.Dtos.User
{
    public class UserRegisterDto
    {
        [StringLength(256), Required, EmailAddress]
        public string Email { get; set; }
        [StringLength(256), Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}