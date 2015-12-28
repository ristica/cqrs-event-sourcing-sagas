using BankAccount.Infrastructure.Commanding;

namespace BankAccount.Infrastructure.CommandHandling
{
    public interface ICommandHandler<in TCommand>
    {
        void Execute(TCommand command);
    }
}
