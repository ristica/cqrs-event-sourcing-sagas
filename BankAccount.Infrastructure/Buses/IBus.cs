namespace BankAccount.Infrastructure.Buses
{
    public interface IBus
    {
        void RegisterHandler<T>();
    }
}
