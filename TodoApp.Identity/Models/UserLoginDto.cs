using System.ComponentModel.DataAnnotations;

namespace TodoApp.Identity.Models
{
    public class UserLoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}