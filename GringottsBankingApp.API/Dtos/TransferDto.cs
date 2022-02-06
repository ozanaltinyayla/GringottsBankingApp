using System;
using System.ComponentModel.DataAnnotations;

namespace GringottsBankingApp.API.Dtos
{
    public class TransferDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public int SenderAccountId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public int ReceiverAccountId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public decimal TransferAmount { get; set; }

        public DateTime TransferDate { get; set; }
    }
}
