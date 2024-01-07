
using System.ComponentModel.DataAnnotations;

namespace BankingAPI.DTOs
{
    public class UserRoleUpdateDto
    {
        [Required]
        [RegularExpression("admin|user|auditor", ErrorMessage = "Rol 'admin', 'user' veya 'auditor' olmalıdır.")]
        public string NewRole { get; set; }
    }
}