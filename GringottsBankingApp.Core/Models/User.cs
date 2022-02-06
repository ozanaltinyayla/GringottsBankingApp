using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GringottsBankingApp.Core.Models
{
    public class User
    {
        public User()
        {
            Accounts = new Collection<Account>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
