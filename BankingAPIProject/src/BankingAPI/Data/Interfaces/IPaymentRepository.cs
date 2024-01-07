using BankingAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingAPI.Data.Interfaces
{
    public interface IPaymentRepository
    {
        Task AddPaymentAsync(Payment payment);
        Task<Payment> GetPaymentByIdAsync(int paymentId);
        Task<IEnumerable<Payment>> GetPaymentsByAccountIdAsync(int accountId);
    }
}
