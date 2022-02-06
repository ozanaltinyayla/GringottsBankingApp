using GringottsBankingApp.Core.Models;
using GringottsBankingApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GringottsBankingApp.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }

        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User> GetWithAccountsByIdAsync(int userId)
        {
            return await _appDbContext.Users.Include(x => x.Accounts).SingleOrDefaultAsync(x => x.Id == userId);
        }
    }
}
