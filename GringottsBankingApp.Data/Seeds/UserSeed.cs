using GringottsBankingApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GringottsBankingApp.Data.Seeds
{
    public class UserSeed : IEntityTypeConfiguration<User>
    {
        private readonly int[] _ids;

        public UserSeed(int[] ids)
        {
            _ids = ids;
        }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User { Id = _ids[0], Name = "Gnarlak" },
                new User { Id = _ids[1], Name = "Gornuk" },
                new User { Id = _ids[2], Name = "Griphook" },
                new User { Id = _ids[3], Name = "Burgock" }
                );
        }
    }
}
