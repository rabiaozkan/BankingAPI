using System.ComponentModel.DataAnnotations;

namespace BankingAPI.DTOs
{
    public class LoanApplicationDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Kredi miktarı sıfırdan büyük olmalıdır.")]
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public int DurationMonths { get; set; }

        // public decimal AnnualIncome { get; set; } // Başvuranın yıllık geliri
        // public string Purpose { get; set; } // Kredinin kullanılacağı amaç
    }
}
