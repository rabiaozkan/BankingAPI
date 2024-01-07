using System.ComponentModel.DataAnnotations;

public class AutoPaymentSetupDto
{
    [Required]
    public int UserId { get; set; } // Kullanıcının benzersiz kimliği

    [Required]
    public int AccountId { get; set; } // Ödemenin yapılacağı hesap kimliği

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Ödeme miktarı sıfırdan büyük olmalıdır.")]
    public decimal Amount { get; set; } // Otomatik ödeme miktarı

    [Required]
    public string Frequency { get; set; } // Ödeme sıklığı (aylık, haftalık, yıllık)

    [Required]
    public DateTime PaymentDate { get; set; } // Otomatik ödemenin başlayacağı tarih

}
