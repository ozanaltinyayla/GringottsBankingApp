﻿using GringottsBankingApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GringottsBankingApp.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetWithAccountsByIdAsync(int userId);
    }
}
