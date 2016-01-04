namespace BankAccount.Infrastructure
{
    public interface IAmStartedBy<in T>  where T : Message
    {
        void Handle(T message); 
    }
}
