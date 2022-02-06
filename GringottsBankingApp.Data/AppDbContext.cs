using GringottsBankingApp.Core.Models;
using GringottsBankingApp.Data.Configurations;
using GringottsBankingApp.Data.Seeds;
using Microsoft.EntityFrameworkCore;

namespace GringottsBankingApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transfer> Transfers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TransferConfiguration());

            modelBuilder.ApplyConfiguration(new AccountSeed(new int[] { 1, 2, 3, 4 }));
            modelBuilder.ApplyConfiguration(new UserSeed(new int[] { 1, 2, 3, 4 }));
        }
    }
}
