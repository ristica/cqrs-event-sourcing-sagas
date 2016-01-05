using BankAccount.Commands;
using BankAccount.Infrastructure;
using BankAccount.Infrastructure.Buses;
using BankAccount.Infrastructure.Storage;
using BankAccount.ProcessManager.Base;

namespace BankAccount.ProcessManager
{
    public class ChangeAddressDetailsSaga : Saga,
        IAmStartedBy<ChangeAddressDetailsCommand>
    {
        #region C-Tor

        public ChangeAddressDetailsSaga(
            IBus bus, 
            ICommandStackRepository repository) 
            : base(bus, repository)
        {
        }

        #endregion

        #region Handling commands

        public void Handle(ChangeAddressDetailsCommand message)
        {
            var aggregate = this.Repository.GetById<Domain.CustomerDomainModel>(message.Id);
            aggregate.ChangeAddress(
                message.Street,
                message.Hausnumber,
                message.Zip,
                message.City,
                message.State);

            this.Repository.Save(aggregate);
        }

        #endregion

    }
}
