namespace BankAccount.Infrastructure.Commanding
{
    public interface IAmStartedBy<in T>  where T : Command
    {
        void Handle(T message); 
    }
}
