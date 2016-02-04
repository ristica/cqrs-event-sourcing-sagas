using BankAccount.Commands;
using BankAccount.Infrastructure;
using BankAccount.Infrastructure.Buses;
using BankAccount.Infrastructure.Storage;
using BankAccount.ProcessManager.Base;

namespace BankAccount.ProcessManager
{
    public class ChangePersonDetailsSaga : Saga,
        IAmStartedBy<ChangePersonDetailsCommand>
    {
        #region C-Tor

        public ChangePersonDetailsSaga(
            IBus bus, 
            ICommandStackRepository repository) 
            : base(bus, repository)
        {
        }

        #endregion

        #region Handling commands

        public void Handle(ChangePersonDetailsCommand message)
        {
            var aggregate = this.Repository.GetById<Domain.CustomerDomainModel>(message.Id);
            aggregate.ChangePerson(
                message.FirstName, 
                message.LastName, 
                message.IdCard, 
                message.IdNumber);

            this.Repository.Save(aggregate);
        }

        #endregion
    }
}
