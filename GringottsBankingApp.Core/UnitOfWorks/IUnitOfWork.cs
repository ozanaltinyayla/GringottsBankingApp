using GringottsBankingApp.Core.Repositories;
using System.Threading.Tasks;

namespace GringottsBankingApp.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IAccountRepository Accounts { get; }
        IUserRepository Users { get; }
        ITransferRepository Transfers { get; }

        Task CommitAsync();
        void Commit();
    }
}
