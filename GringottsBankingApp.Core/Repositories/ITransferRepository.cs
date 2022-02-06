using GringottsBankingApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GringottsBankingApp.Core.Repositories
{
    public interface ITransferRepository : IGenericRepository<Transfer>
    {
        IEnumerable<IEnumerable<Transfer>> GetAllById(int accountId);
        void TransferMoney(Transfer transferParameters);
    }
}
