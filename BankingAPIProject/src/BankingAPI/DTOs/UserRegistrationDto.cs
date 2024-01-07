using System.ComponentModel.DataAnnotations;

namespace BankingAPI.DTOs
{
    public class UserRegistrationDto
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}