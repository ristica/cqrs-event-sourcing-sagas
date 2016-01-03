using BankAccount.CommandStackDal.Abstraction;
using BankAccount.EventHandlers.Base;
using BankAccount.Events;
using BankAccount.Infrastructure.EventHandling;

namespace BankAccount.EventHandlers
{
    public class BankAccountDeletedEventHandler : BaseCustomerEventHandler, IEventHandler<BankAccountDeletedEvent>
    {
        public BankAccountDeletedEventHandler(ICommandStackDatabase database) 
            : base (database)
        {
        }

        public void Handle(BankAccountDeletedEvent handle)
        {
            this.Database.Delete(handle.AggregateId);
        }
    }
}
