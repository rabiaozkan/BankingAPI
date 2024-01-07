using System.ComponentModel.DataAnnotations;

namespace BankingAPI.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int UserId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Kredi miktarı sıfırdan büyük olmalıdır.")]
        public decimal Amount { get; set; }

        [Range(0, 100, ErrorMessage = "Faiz oranı 0 ile 100 arasında olmalıdır.")]
        public decimal InterestRate { get; set; }
        public int DurationMonths { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; } // "approved", "rejected", "pending"
        public decimal MonthlyPayment { get; set; }
    }
}