using BankAccount.Infrastructure.Commanding;

namespace BankAccount.Infrastructure.Buses
{
    public interface ICommandBus : IBus
    {
        void Send<T>(T command) where T : Command;
    }
}
