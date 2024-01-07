using System.ComponentModel.DataAnnotations;

public class BalanceUpdateDto
{
    [Required]
    public int AccountId { get; set; } // Bakiye güncellemesi yapılacak hesabın kimliği

    [Required]
    public decimal Amount { get; set; } // Güncellenen miktar (pozitif veya negatif olabilir)

}
