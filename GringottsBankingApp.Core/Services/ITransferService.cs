using GringottsBankingApp.Core.Models;
using System.Collections.Generic;

namespace GringottsBankingApp.Core.Services
{
    public interface ITransferService : IGenericService<Transfer>
    {
        IEnumerable<IEnumerable<Transfer>> GetAllById(int accountId);
        void TransferMoney(Transfer transferParameters);
    }
}
