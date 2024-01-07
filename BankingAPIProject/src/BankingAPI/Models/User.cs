using System.ComponentModel.DataAnnotations;

namespace BankingAPI.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Kullanıcı adı 5 ile 50 karakter arasında olmalıdır.")]
        public string Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Geçersiz email adresi.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^.*(?=.{8,})(?=.*[a-zA-Z])(?=.*\\d)(?=.*[!@#$%^&*()_+={}?:~\\[\\]]).*$", ErrorMessage = "Şifre en az 8 karakter olmalı ve sayı, harf ve özel karakter içermelidir.")]
        public string PasswordHash { get; set; }

        [Required]
        [RegularExpression("admin|user|auditor", ErrorMessage = "Rol 'admin', 'user' veya 'auditor' olmalıdır.")]
        public string Role { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime? LastLogin { get; set; }
        public string Token { get; set; }
    }
}
