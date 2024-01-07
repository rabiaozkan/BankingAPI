using System.ComponentModel.DataAnnotations;

namespace BankingAPI.DTOs
{
    public class PaymentDto
    {
        public int PaymentId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int AccountId { get; set; }
        public int? LoanId { get; set; } // Opsiyonel, sadece kredi ödemeleri için
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Ödeme miktarı sıfırdan büyük olmalıdır.")]
        public decimal Amount { get; set; }

        // public string Description { get; set; } // Ödeme açıklaması
        // public string Currency { get; set; } // Para birimi
    }
}
