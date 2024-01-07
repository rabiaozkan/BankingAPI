using BankingAPI.DTOs;
using BankingAPI.Models;
using System.Threading.Tasks;

namespace BankingAPI.Services
{
    public interface IAccountService
    {
        Task<Account> CreateAccountAsync(AccountCreationDto accountCreationDto);
        Task<decimal?> GetBalanceAsync(int accountId);
        Task<bool> UpdateBalanceAsync(int accountId, BalanceUpdateDto balanceUpdateDto);
        Task<bool> DepositAsync(TransactionDto transactionDto);
        Task<bool> WithdrawAsync(TransactionDto transactionDto);
    }
}
