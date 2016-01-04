using System.Data.Entity;

namespace BankAccount.DbModel.ItemDb
{
    public class BankAccountInitializer : DropCreateDatabaseAlways<BankAccountDbContext>
    {
        protected override void Seed(BankAccountDbContext context)
        {
            base.Seed(context);
        }
    }
}
