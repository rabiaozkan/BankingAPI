using System.ComponentModel.DataAnnotations;

namespace BankingAPI.DTOs
{
    public class UserUpdateDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string NewRole { get; set; } 
    }
}