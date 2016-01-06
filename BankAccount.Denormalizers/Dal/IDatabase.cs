using System;
using BankAccount.ValueTypes;

namespace BankAccount.Denormalizers.Dal
{
    public interface IDatabase<in T>
    {
        void Create(T item);
        void Update(Guid aggregateId, State state, int version);
    }
}
