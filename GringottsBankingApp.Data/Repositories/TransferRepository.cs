using GringottsBankingApp.Core.Models;
using GringottsBankingApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GringottsBankingApp.Data.Repositories
{
    public class TransferRepository : GenericRepository<Transfer>, ITransferRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }

        public TransferRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<IEnumerable<Transfer>> GetAllById(int accountId)
        {
            List<List<Transfer>> transferHistory = new();

            var IncomingTransferHistory = _appDbContext.Transfers.Where(x => x.ReceiverAccountId == accountId).OrderByDescending(x => x.TransferDate).ToList();

            var OutgoingTransferHistory = _appDbContext.Transfers.Where(x => x.SenderAccountId == accountId).OrderByDescending(x => x.TransferDate).ToList();

            transferHistory.Add(IncomingTransferHistory);
            transferHistory.Add(OutgoingTransferHistory);

            return transferHistory;
        }

        public void TransferMoney(Transfer transferParameters)
        {
            var senderAccount = _appDbContext.Accounts.Include(x => x.User).SingleOrDefault(x => x.Id == transferParameters.SenderAccountId);

            var receiverAccount = _appDbContext.Accounts.Include(x => x.User).SingleOrDefault(x => x.Id == transferParameters.ReceiverAccountId);

            senderAccount.Deposit -= transferParameters.TransferAmount;

            receiverAccount.Deposit += transferParameters.TransferAmount;

            Transfer transaction = new()
            {
                SenderAccountId = transferParameters.SenderAccountId,
                ReceiverAccountId = transferParameters.ReceiverAccountId,
                TransferAmount = transferParameters.TransferAmount,
                TransferDate = DateTime.Now
            };

            _appDbContext.AddAsync(transaction);
        }
    }
}
