using BankAccount.Commands;
using BankAccount.Domain;
using BankAccount.Infrastructure;
using BankAccount.Infrastructure.Buses;
using BankAccount.Infrastructure.Storage;
using BankAccount.ProcessManager.Base;

namespace BankAccount.ProcessManager
{
    public class MoneyTransferSaga : Saga,
        IAmStartedBy<ChangeBalanceCommand>
    {
        #region C-Tor

        public MoneyTransferSaga(IBus bus, ICommandStackRepository repository) 
            : base(bus, repository)
        {
        }

        #endregion

        #region Handling commands

        public void Handle(ChangeBalanceCommand message)
        {
            var aggregate = this.Repository.GetById<AccountDomainModel>(message.Id);
            aggregate.ChangeBalance(message.Amount, message.Version);
            this.Repository.Save(aggregate);
        }

        #endregion
    }
}
