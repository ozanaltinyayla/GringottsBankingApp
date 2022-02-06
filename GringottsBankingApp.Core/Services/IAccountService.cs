using GringottsBankingApp.Core.Models;
using System.Threading.Tasks;

namespace GringottsBankingApp.Core.Services
{
    public interface IAccountService : IGenericService<Account>
    {
        Task<Account> GetWithUserByIdAsync(int accountId);
    }
}
