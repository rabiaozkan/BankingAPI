using BankingAPI.DTOs;
using BankingAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingAPI.Services
{
    public interface IPaymentService
    {
        Task<Payment> SetupAutoPaymentAsync(AutoPaymentSetupDto setupDto);
        Task<bool> MakePaymentAsync(PaymentDto paymentDto);
        Task<IEnumerable<Payment>> GetPendingAutoPaymentsAsync();
        Task<bool> ProcessPaymentAsync(int paymentId);
        Task<IEnumerable<Payment>> GetPaymentHistoryAsync(int accountId);
    }
}
