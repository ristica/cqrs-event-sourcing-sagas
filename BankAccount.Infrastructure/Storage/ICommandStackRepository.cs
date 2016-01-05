using System;
using BankAccount.Infrastructure.Domain;

namespace BankAccount.Infrastructure.Storage
{
    public interface ICommandStackRepository
    {
        void Save(AggregateRoot aggregate);
        T GetById<T>(Guid id) where T : AggregateRoot, new();
    }
}
