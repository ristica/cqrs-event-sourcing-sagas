using BankAccount.Commands;
using BankAccount.Infrastructure;
using BankAccount.Infrastructure.Buses;
using BankAccount.Infrastructure.Storage;
using BankAccount.ProcessManager.Base;

namespace BankAccount.ProcessManager
{
    public class ChangeContactDetailsSaga : Saga,
        IAmStartedBy<ChangeContactDetailsCommand>
    {
        #region C-Tor

        public ChangeContactDetailsSaga(
            IBus bus, 
            ICommandStackRepository repository) 
            : base(bus, repository)
        {
        }

        #endregion

        #region Handling commands

        public void Handle(ChangeContactDetailsCommand message)
        {
            var aggregate = this.Repository.GetById<Domain.CustomerDomainModel>(message.Id);
            aggregate.ChangeContact(
                message.Email,
                message.Phone);

            this.Repository.Save(aggregate);
        }

        #endregion
    }
}
