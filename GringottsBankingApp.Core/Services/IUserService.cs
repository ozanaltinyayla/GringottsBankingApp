using GringottsBankingApp.Core.Models;
using System.Threading.Tasks;

namespace GringottsBankingApp.Core.Services
{
    public interface IUserService : IGenericService<User>
    {
        Task<User> GetWithAccountsByIdAsync(int userId);
    }
}
