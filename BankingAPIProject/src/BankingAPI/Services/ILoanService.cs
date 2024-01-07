using BankingAPI.DTOs;
using BankingAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingAPI.Services
{
    public interface ILoanService
    {
        Task<Loan> ApplyForLoanAsync(LoanApplicationDto applicationDto);
        Task<bool> MakePaymentAsync(PaymentDto paymentDto);
        Task<Loan> GetLoanDetailsAsync(int loanId);
        Task<IEnumerable<Loan>> GetUserLoansAsync(int userId);
        Task<LoanStatusDto> GetLoanStatusAsync(int loanId);
    }
}
