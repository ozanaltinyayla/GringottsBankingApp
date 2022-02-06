using GringottsBankingApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GringottsBankingApp.Data.Seeds
{
    public class AccountSeed : IEntityTypeConfiguration<Account>
    {
        private readonly int[] _ids;

        public AccountSeed(int[] ids)
        {
            _ids = ids;
        }

        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasData(
                new Account { Id = 1, Deposit = 50, UserId = _ids[0] },
                    new Account { Id = 2, Deposit = 150, UserId = _ids[0] },
                    new Account { Id = 3, Deposit = 250, UserId = _ids[0] },
                    new Account { Id = 4, Deposit = 100, UserId = _ids[1] },
                    new Account { Id = 5, Deposit = 200, UserId = _ids[1] },
                    new Account { Id = 6, Deposit = 300, UserId = _ids[1] },
                    new Account { Id = 7, Deposit = 80, UserId = _ids[2] },
                    new Account { Id = 8, Deposit = 90, UserId = _ids[2] },
                    new Account { Id = 9, Deposit = 100, UserId = _ids[2] },
                    new Account { Id = 10, Deposit = 400, UserId = _ids[3] },
                    new Account { Id = 11, Deposit = 500, UserId = _ids[3] },
                    new Account { Id = 12, Deposit = 600, UserId = _ids[3] }
                );
        }
    }
}
