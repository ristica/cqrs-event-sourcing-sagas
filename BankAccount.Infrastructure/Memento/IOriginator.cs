using BankAccount.Infrastructure.Domain;

namespace BankAccount.Infrastructure.Memento
{
    public interface IOriginator
    {
        BaseMemento GetMemento();
        void SetMemento(BaseMemento memento);
    }
}
