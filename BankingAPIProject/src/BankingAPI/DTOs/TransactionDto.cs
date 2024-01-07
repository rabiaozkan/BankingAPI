using System.ComponentModel.DataAnnotations;

namespace BankingAPI.DTOs
{
    public class TransactionDto
    {
        public int TransactionId { get; set; } // İşlemin benzersiz kimliği
        [Required]
        public int AccountId { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "İşlem tutarı sıfırdan büyük olmalıdır.")]
        public decimal Amount { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        [RegularExpression("deposit|withdrawal|transfer", ErrorMessage = "İşlem türü 'deposit', 'withdrawal' veya 'transfer' olmalıdır.")]
        public string TransactionType { get; set; }

        // public int DestinationAccountId { get; set; } // Transfer işlemleri için hedef hesap kimliği
        // public string Description { get; set; } // İşlem açıklaması
        // public string Currency { get; set; } // Para birimi
    }
}
