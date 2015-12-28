using BankAccount.Infrastructure.Commanding;

namespace BankAccount.Infrastructure.CommandHandling
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<T> GetHandler<T>() where T : Command;
    }
}
