using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingAPI.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IAccountRepository Accounts { get; }
        ITransactionRepository Transactions { get; }
        ILoanRepository Loans { get; }
        IPaymentRepository Payments { get; }

        Task<int> CommitAsync();
    }
}
