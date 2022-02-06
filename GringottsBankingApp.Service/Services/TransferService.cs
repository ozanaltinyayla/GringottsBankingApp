using GringottsBankingApp.Core.Models;
using GringottsBankingApp.Core.Repositories;
using GringottsBankingApp.Core.Services;
using GringottsBankingApp.Core.UnitOfWorks;
using System.Collections.Generic;

namespace GringottsBankingApp.Service.Services
{
    public class TransferService : GenericService<Transfer>, ITransferService
    {
        public TransferService(IUnitOfWork unitOfWork, IGenericRepository<Transfer> repository) : base(unitOfWork, repository)
        {
        }

        public IEnumerable<IEnumerable<Transfer>> GetAllById(int accountId)
        {
            return _unitOfWork.Transfers.GetAllById(accountId);
        }

        public void TransferMoney(Transfer transferParameters)
        {
            _unitOfWork.Transfers.TransferMoney(transferParameters);

            _unitOfWork.Commit();
        }
    }
}
