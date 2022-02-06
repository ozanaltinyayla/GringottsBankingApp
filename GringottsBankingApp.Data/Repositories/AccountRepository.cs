using GringottsBankingApp.Core.Models;
using GringottsBankingApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GringottsBankingApp.Data.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }

        public AccountRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Account> GetWithUserByIdAsync(int accountId)
        {
            return await _appDbContext.Accounts.Include(x => x.User).SingleOrDefaultAsync(x => x.Id == accountId);
        }
    }
}
