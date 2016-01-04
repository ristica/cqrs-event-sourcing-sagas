using System.Linq;
using BankAccount.DbModel.Entities;

namespace BankAccount.Denormalizers.Db
{
    public interface IDatabase
    {
        IQueryable<CustomerEntity> Customers { get; }
    }
}
