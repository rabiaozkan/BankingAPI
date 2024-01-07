namespace BankingAPI.Models
{
    public class SupportRequest
    {
        public int SupportRequestId { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public int Id { get; set; }
    }
}
