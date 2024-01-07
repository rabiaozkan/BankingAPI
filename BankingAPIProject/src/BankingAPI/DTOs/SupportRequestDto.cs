using System.ComponentModel.DataAnnotations;

namespace BankingAPI.DTOs
{
    public class SupportRequestDto
    {
        [Required]
        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
        public string Description { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
