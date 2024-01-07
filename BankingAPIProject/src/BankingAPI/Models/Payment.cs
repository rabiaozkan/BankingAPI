namespace BankingAPI.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentType { get; set; } // "automatic", "manual"
        public string Status { get; set; } // "completed", "pending", "failed"
    }
}