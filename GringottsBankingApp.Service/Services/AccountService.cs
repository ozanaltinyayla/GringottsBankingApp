using GringottsBankingApp.Core.Models;
using GringottsBankingApp.Core.Repositories;
using GringottsBankingApp.Core.Services;
using GringottsBankingApp.Core.UnitOfWorks;
using System.Threading.Tasks;

namespace GringottsBankingApp.Service.Services
{
    public class AccountService : GenericService<Account>, IAccountService
    {
        public AccountService(IUnitOfWork unitOfWork, IGenericRepository<Account> repository) : base(unitOfWork, repository)
        {
        }

        public async Task<Account> GetWithUserByIdAsync(int accountId)
        {
            return await _unitOfWork.Accounts.GetWithUserByIdAsync(accountId);
        }
    }
}
