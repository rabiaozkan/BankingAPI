using System;
using System.Threading.Tasks;
using BankingAPI.Data;
using BankingAPI.DTOs;
using BankingAPI.Models;
using BankingAPI.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Services
{
    public class UserService : IUserService
    {
        private readonly BankingContext _context;
        private readonly AuthHelper _authHelper;

        public UserService(BankingContext context, AuthHelper authHelper)
        {
            _context = context;
            _authHelper = authHelper;
        }

        public async Task<User> RegisterAsync(UserRegistrationDto registrationDto)
        {
            // Kullanıcı adının benzersiz olup olmadığını kontrol et
            if (await _context.Users.AnyAsync(u => u.Username == registrationDto.Username))
            {
                throw new Exception("Username already exists");
            }

            // Şifreyi hash'le
            var hashedPassword = PasswordHasher.HashPassword(registrationDto.Password);

            // Yeni kullanıcı oluştur
            var user = new User
            {
                Username = registrationDto.Username,
                PasswordHash = hashedPassword,
                Email = registrationDto.Email,
                Role = "user",
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);

            if (user == null || !PasswordHasher.VerifyPassword(password, user.PasswordHash))
            {
                throw new Exception("Username or password is incorrect");
            }

            // Kullanıcının yetkilerini al
            var permissions = await GetUserPermissionsAsync(user);

            // GenerateToken metoduna gerekli tüm parametreleri geçir
            var token = _authHelper.GenerateToken(user.Username, user.Role, permissions);
            user.Token = token; // Token'ı kullanıcıya ata

            return user;
        }


        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<bool> UpdateUserAsync(int userId, UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                // Kullanıcı bilgilerini güncelle
                user.Email = userUpdateDto.Email;
                // Rol ve kullanıcı adı gibi hassas alanların güncellenmesini engelle
                // user.Role ve user.Username güncellemeleri burada yapılmamalı


                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                // Loglama
                LogError(ex);
                // Kullanıcıya uygun bir hata mesajı döndürme
                throw new Exception("An error occurred while updating the user.");
            }
        }

        public async Task<bool> UpdateUserRoleAsync(int userId, UserRoleUpdateDto roleUpdateDto)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Rol güncelleme işlemi
            user.Role = roleUpdateDto.NewRole;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<string> GenerateJwtTokenAsync(User user)
        {
            // GetUserPermissions metodunu asenkron olarak çağır
            var permissions = await GetUserPermissionsAsync(user);

            return _authHelper.GenerateToken(user.Username, user.Role, permissions);
        }

        // Örnek bir asenkron GetUserPermissionsAsync metodu
        private async Task<IEnumerable<string>> GetUserPermissionsAsync(User user)
        {
            // Kullanıcının yetkilerini asenkron olarak al
            // veritabanından yetkileri çekmek için:
            // var permissions = await dbContext.Permissions.Where(p => p.UserId == user.Id).ToListAsync();
            var permissions = new List<string>(); // Geçici olarak boş bir liste döndür
            return permissions;
        }

        private void LogError(Exception ex)
        {
            // Loglama işlemleri burada gerçekleştirilir: Dosyaya yazma, veritabanına kaydetme, vb.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}