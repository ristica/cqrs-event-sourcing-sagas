using System;
using BankAccount.Infrastructure.Domain;

namespace BankAccount.Infrastructure.Storage
{
    public interface ICommandStackRepository<out T> where T : AggregateRoot, new()
    {
        void Save(AggregateRoot aggregate, int expectedVersion);
        T GetById(Guid id);
    }
}
