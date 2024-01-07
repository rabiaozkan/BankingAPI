using System.ComponentModel.DataAnnotations;

namespace BankingAPI.DTOs
{
    public class TransferDto
    {
        [Required]
        public int FromAccountId { get; set; }
        [Required]
        public int ToAccountId { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Transfer miktarı sıfırdan büyük olmalıdır.")]
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        [StringLength(200, ErrorMessage = "Açıklama en fazla 200 karakter olabilir.")]
        public string Description { get; set; }
    }
}
