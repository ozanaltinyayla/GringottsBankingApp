using GringottsBankingApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GringottsBankingApp.Data.Configurations
{
    public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.SenderAccountId).IsRequired();
            builder.Property(x => x.ReceiverAccountId).IsRequired();
            builder.Property(x => x.TransferAmount).IsRequired().HasColumnType("decimal(18,2)");
            builder.ToTable("Transfers");
        }
    }
}
