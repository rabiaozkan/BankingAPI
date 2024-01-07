using System.ComponentModel.DataAnnotations;

namespace BankingAPI.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "İşlem tutarı negatif olamaz.")]
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Type { get; set; } // "deposit", "withdrawal", "transfer"
        public string Status { get; set; } // "completed", "pending", "failed"
        public string Description { get; set; }
    }
}