using System;

namespace BankAccount.Infrastructure.Commanding
{
    public interface ICommand : IDomainMessage
    {
        Guid Id { get; }
    }
}
