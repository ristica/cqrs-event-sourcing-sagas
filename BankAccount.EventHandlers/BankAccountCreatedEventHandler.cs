using BankAccount.CommandStackDal.Storage.Abstraction;
using BankAccount.EventHandlers.Base;
using BankAccount.Events;
using BankAccount.Infrastructure.EventHandling;

namespace BankAccount.EventHandlers
{
    public class BankAccountCreatedEventHandler : BaseBankAccountEventHandler, IEventHandler<BankAccountCreatedEvent>
    {
        public BankAccountCreatedEventHandler(ICommandStackDatabase database) 
            : base(database)
        {

        }

        public void Handle(BankAccountCreatedEvent handle)
        {
            this.Database.Save(new Domain.BankAccount
            {
                Id = handle.AggregateId,
                Customer = handle.Customer,
                Money = handle.Money,
                Version = handle.Version,
                Address = handle.Address,
                Contact = handle.Contact
            });
        }
    }
}
