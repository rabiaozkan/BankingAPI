using BankingAPI.DTOs;
using BankingAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingAPI.Services
{
    public interface ITransactionService
    {
        Task<bool> DepositAsync(TransactionDto transactionDto);
        Task<bool> WithdrawAsync(TransactionDto transactionDto);
        Task<bool> TransferAsync(TransferDto transferDto);
        Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(int accountId);
    }
}
