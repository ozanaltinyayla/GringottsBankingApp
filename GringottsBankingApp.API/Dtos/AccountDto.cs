using System.ComponentModel.DataAnnotations;

namespace GringottsBankingApp.API.Dtos
{
    public class AccountDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public decimal Deposit { get; set; }

        public int UserId { get; set; }
    }
}
