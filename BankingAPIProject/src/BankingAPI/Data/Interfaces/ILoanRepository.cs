using BankingAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingAPI.Data.Interfaces
{
    public interface ILoanRepository
    {
        Task AddLoanAsync(Loan loan);
        Task<Loan> GetLoanByIdAsync(int loanId);
        Task<IEnumerable<Loan>> GetLoansByUserIdAsync(int userId);
    }
}
