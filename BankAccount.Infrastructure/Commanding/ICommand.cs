using System;

namespace BankAccount.Infrastructure.Commanding
{
    public interface ICommand
    {
        Guid Id { get; }
    }
}
