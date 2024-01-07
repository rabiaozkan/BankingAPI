using BankingAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingAPI.Data.Interfaces
{
    public interface ITransactionRepository
    {
        Task AddTransactionAsync(Transaction transaction);
        Task<Transaction> GetTransactionByIdAsync(int transactionId);
        Task UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(Transaction transaction);
        Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId);

    }
}
        

