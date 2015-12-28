using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BankAccount.DbModel.Entities;

namespace BankAccount.DbModel.EventDb
{
    public class EventDbContext : DbContext
    {
        public EventDbContext() : base("Name=EventStoreConnectionString")
        {
            Database.SetInitializer(new EventInitializer());
        }

        public DbSet<EventEntity> EventSet { get; set; }
        public DbSet<SnapshotEntity> SnapshotSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
