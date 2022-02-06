
namespace GringottsBankingApp.Core.Models
{
    public class Account
    {
        public int Id { get; set; }
        public decimal Deposit { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
