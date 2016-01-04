using System.Linq;
using BankAccount.DbModel.Entities;
using BankAccount.DbModel.ItemDb;

namespace BankAccount.Denormalizers.Db
{
    public abstract class Database : IDatabase
    {
        private readonly BankAccountDbContext _ctx;

        public Database()
        {
            this._ctx = new BankAccountDbContext();
        }

        public IQueryable<CustomerEntity> Customers => this._ctx.CustomerSet;
    }
}
