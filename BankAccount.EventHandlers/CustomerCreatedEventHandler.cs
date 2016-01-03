using BankAccount.CommandStackDal.Abstraction;
using BankAccount.EventHandlers.Base;
using BankAccount.Events;
using BankAccount.Infrastructure.EventHandling;

namespace BankAccount.EventHandlers
{
    public class CustomerCreatedEventHandler : BaseBankAccountEventHandler, IEventHandler<CustomerCreatedEvent>
    {
        public CustomerCreatedEventHandler(ICommandStackDatabase database) 
            : base(database)
        {

        }

        public void Handle(CustomerCreatedEvent handle)
        {
            this.Database.Save(new Domain.CustomerDomainModel
            {
                Id          = handle.AggregateId,
                Person      = handle.Person,
                Version     = handle.Version,
            });
        }
    }
}
