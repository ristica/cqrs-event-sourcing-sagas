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
            ISagaBus bus, 
            ICommandStackRepository<Domain.CustomerDomainModel> repository) 
            : base(bus, repository)
        {
        }

        #endregion

        #region Handling commands

        public void Handle(ChangeContactDetailsCommand message)
        {
            var aggregate = this.Repository.GetById(message.Id);
            aggregate.ChangeContact(
                message.Email,
                message.Phone);

            this.Repository.Save(aggregate, aggregate.Version);
        }

        #endregion

    }
}
