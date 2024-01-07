using BankingAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingAPI.Data.Interfaces
{
    public interface IAccountRepository
    {
        Task AddAccountAsync(Account account);
        Task<Account> GetAccountByIdAsync(int accountId);
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(Account account);
    }
}
