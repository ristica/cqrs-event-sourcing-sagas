using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BankAccount.DbModel.Entities;

namespace BankAccount.DbModel.ItemDb
{
    public class BankAccountDbContext : DbContext
    {
        public BankAccountDbContext() : base("Name=BankAccountStoreConnectionString")
        {
            Database.SetInitializer(new BankAccountInitializer());
        }

        public DbSet<BankAccountEntity> BankAccountSet { get; set; }
        public DbSet<BankAccountBalanceEntity> BankAccountBalanceSet { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
