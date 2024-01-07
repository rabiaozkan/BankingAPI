using System.ComponentModel.DataAnnotations;

public class AccountCreationDto
{
    [Required]
    public int UserId { get; set; } // Kullanıcının benzersiz kimliği

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Başlangıç bakiyesi sıfırdan büyük olmalıdır.")]
    public decimal InitialDeposit { get; set; } // Başlangıç bakiyesi

    [Required]
    [RegularExpression("checking|savings", ErrorMessage = "Hesap türü 'checking' veya 'savings' olmalıdır.")]
    public string AccountType { get; set; } // Hesap türü
}
