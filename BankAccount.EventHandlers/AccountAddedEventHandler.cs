using BankAccount.CommandStackDal.Abstraction;
using BankAccount.Domain;
using BankAccount.EventHandlers.Base;
using BankAccount.Events;
using BankAccount.Infrastructure.EventHandling;

namespace BankAccount.EventHandlers
{
    public class AccountAddedEventHandler : BaseCustomerEventHandler, IEventHandler<AccountAddedEvent>
    {
        public AccountAddedEventHandler(ICommandStackDatabase database) 
            : base(database)
        {
        }

        public void Handle(AccountAddedEvent handle)
        {
            this.Database.Save(new AccountDomainModel
            {
               Id = handle.AggregateId,
               CustomerId = handle.CustomerId,
               Version = handle.Version,
               Currency = handle.Currency
            });
        }
    }
}
