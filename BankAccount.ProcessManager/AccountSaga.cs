using BankAccount.Commands;
using BankAccount.Domain;
using BankAccount.Infrastructure;
using BankAccount.Infrastructure.Buses;
using BankAccount.Infrastructure.Storage;
using BankAccount.ProcessManager.Base;

namespace BankAccount.ProcessManager
{
    public class AccountSaga : Saga,
        IAmStartedBy<AddAccountCommand>,
        IHandleMessage<LockAccountCommand>,
        IHandleMessage<UnlockAccountCommand>,
        IHandleMessage<DeleteAccountCommand>
    {
        #region C-Tor

        public AccountSaga(IBus bus, ICommandStackRepository repository) 
            : base(bus, repository)
        {
        }

        #endregion

        #region Handling commands

        public void Handle(AddAccountCommand message)
        {
            var aggregate = AccountDomainModel.Factory.CreateNewInstance(
                message.Id,
                message.CustomerId,
                message.Version,
                message.Currency,
                message.AccountState);

            // here we are sending the aggregate to the NEventStore
            // to serialize the current state or current delta
            this.Repository.Save(aggregate);
        }

        public void Handle(LockAccountCommand message)
        {
            var aggregate = this.Repository.GetById<AccountDomainModel>(message.Id);
            aggregate.LockAccount();

            // here we are sending the aggregate to the NEventStore
            // to serialize the current state or current delta
            this.Repository.Save(aggregate);
        }

        public void Handle(UnlockAccountCommand message)
        {
            var aggregate = this.Repository.GetById<AccountDomainModel>(message.Id);
            aggregate.UnlockAccount();

            // here we are sending the aggregate to the NEventStore
            // to serialize the current state or current delta
            this.Repository.Save(aggregate);
        }

        public void Handle(DeleteAccountCommand message)
        {
            var aggregate = this.Repository.GetById<AccountDomainModel>(message.Id);
            aggregate.DeleteAccount();

            // here we are sending the aggregate to the NEventStore
            // to serialize the current state or current delta
            this.Repository.Save(aggregate);
        }

        #endregion
    }
}
