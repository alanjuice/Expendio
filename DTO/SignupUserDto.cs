using System.ComponentModel.DataAnnotations;

namespace Expendio.DTO
{
    public class SignupUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Required]
        [RegularExpression("^.*(?=.{8,})(?=.*[a-zA-Z])(?=.*\\d)(?=.*[!#$%&? \"]).*$", ErrorMessage ="Try a stronger password")]
        public string Password { get; set; } = String.Empty;

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = String.Empty;
    }
}
