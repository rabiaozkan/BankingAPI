using BankingAPI.Models;
using BankingAPI.Data.Interfaces;

namespace BankingAPI.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankingContext  _context;

        public AccountRepository(BankingContext  context)
        {
            _context = context;
        }

        // Hesap ekleme
        public async Task AddAccountAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        // Hesabı ID'ye göre getirme
        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            return await _context.Accounts.FindAsync(accountId);
        }

        // Hesap güncelleme
        public async Task UpdateAccountAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        // Hesap silme
        public async Task DeleteAccountAsync(Account account)
        {
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
        }
    }
}