using GringottsBankingApp.Core.Models;
using System.Threading.Tasks;

namespace GringottsBankingApp.Core.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account> GetWithUserByIdAsync(int accountId);
    }
}
