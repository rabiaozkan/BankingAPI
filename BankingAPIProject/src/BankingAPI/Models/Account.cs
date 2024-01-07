using System.ComponentModel.DataAnnotations;

namespace BankingAPI.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Bakiye negatif olamaz.")]
        public decimal Balance { get; set; }

        [Required]
        [RegularExpression("checking|savings", ErrorMessage = "Hesap türü 'checking' veya 'savings' olmalıdır.")]
        public string AccountType { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
