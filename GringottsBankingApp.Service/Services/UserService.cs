using GringottsBankingApp.Core.Models;
using GringottsBankingApp.Core.Repositories;
using GringottsBankingApp.Core.Services;
using GringottsBankingApp.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GringottsBankingApp.Service.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IGenericRepository<User> repository) : base(unitOfWork, repository)
        {
        }

        public async Task<User> GetWithAccountsByIdAsync(int userId)
        {
            return await _unitOfWork.Users.GetWithAccountsByIdAsync(userId);
        }
    }
}
