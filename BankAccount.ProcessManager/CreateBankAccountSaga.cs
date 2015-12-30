using BankAccount.Commands;
using BankAccount.Infrastructure.Buses;
using BankAccount.Infrastructure.Commanding;
using BankAccount.Infrastructure.Storage;
using BankAccount.ProcessManager.Base;
using EventStore;

namespace BankAccount.ProcessManager
{
    public class CreateBankAccountSaga : Saga,
        IAmStartedBy<CreateBankAccountCommand>
    {
        #region C-Tor

        public CreateBankAccountSaga(
            ISagaBus bus, 
            IStoreEvents eventStore, 
            ICommandStackRepository<Domain.BankAccount> repository) 
            : base(bus, eventStore, repository)
        {
        }

        #endregion

        #region Handling commands

        public void Handle(CreateBankAccountCommand message)
        {
            var aggregate = Domain.BankAccount.Factory.CreateNewInstance(
                message.Id,
                message.Version,
                message.Customer.FirstName,
                message.Customer.LastName,
                message.Customer.IdCard,
                message.Customer.IdNumber,
                message.Customer.Dob,
                message.Contact.Email,
                message.Contact.PhoneNumber,
                message.Money.Balance,
                message.Money.Currency,
                message.Address.Street,
                message.Address.Zip,
                message.Address.Hausnumber,
                message.Address.City,
                message.Address.State);

            this.Repository.Save(aggregate, aggregate.Version);
        }

        #endregion
    }
}
