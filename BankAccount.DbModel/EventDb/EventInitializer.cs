using System.Data.Entity;

namespace BankAccount.DbModel.EventDb
{
    public class EventInitializer : DropCreateDatabaseAlways<EventDbContext>
    {
        protected override void Seed(EventDbContext context)
        {
            base.Seed(context);
        }
    }
}
