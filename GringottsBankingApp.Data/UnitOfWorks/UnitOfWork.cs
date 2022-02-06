using GringottsBankingApp.Core.Repositories;
using GringottsBankingApp.Core.UnitOfWorks;
using GringottsBankingApp.Data.Repositories;
using System.Threading.Tasks;

namespace GringottsBankingApp.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private AccountRepository _accountRepository;
        private UserRepository _userRepository;
        private TransferRepository _transferRepository;

        public IAccountRepository Accounts => _accountRepository = _accountRepository ?? new AccountRepository(_context);

        public IUserRepository Users => _userRepository = _userRepository ?? new UserRepository(_context);

        public ITransferRepository Transfers => _transferRepository = _transferRepository ?? new TransferRepository(_context);

        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
