using System;
using System.Threading.Tasks;
using BankingAPI.Data.Interfaces;

namespace BankingAPI.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BankingContext _context;
        private bool _disposed = false;

        public IUserRepository Users { get; private set; }
        public IAccountRepository Accounts { get; private set; }
        public ITransactionRepository Transactions { get; private set; }
        public ILoanRepository Loans { get; private set; }
        public IPaymentRepository Payments { get; private set; }

        public UnitOfWork(BankingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Users = new UserRepository(_context);
            Accounts = new AccountRepository(_context);
            Transactions = new TransactionRepository(_context);
            Loans = new LoanRepository(_context);
            Payments = new PaymentRepository(_context);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
