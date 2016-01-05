using BankAccount.Events;
using BankAccount.Infrastructure;

namespace BankAccount.Denormalizers.Denormalizer
{
    public class MoneyTransferDenormalizer : 
        IHandleMessage<BalanceChangedEvent>
    {
        public void Handle(BalanceChangedEvent message)
        {
            // do nothing here
            // ...
        }
    }
}
