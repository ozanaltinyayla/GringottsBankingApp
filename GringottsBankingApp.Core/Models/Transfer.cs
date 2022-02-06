using System;

namespace GringottsBankingApp.Core.Models
{
    public class Transfer
    {
        public int Id { get; set; }
        public int SenderAccountId { get; set; }
        public int ReceiverAccountId { get; set; }
        public decimal TransferAmount { get; set; }
        public DateTime TransferDate { get; set; }
    }
}
