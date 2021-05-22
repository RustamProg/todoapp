using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Identity.Models
{
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }
}