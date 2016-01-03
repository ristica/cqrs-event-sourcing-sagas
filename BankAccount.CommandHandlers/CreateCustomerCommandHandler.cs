using System;
using BankAccount.CommandHandlers.Base;
using BankAccount.Commands;
using BankAccount.Infrastructure.CommandHandling;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.CommandHandlers
{
    public class CreateCustomerCommandHandler : BaseBankAccountCommandHandler, ICommandHandler<CreateCustomerCommand>
    {
        public CreateCustomerCommandHandler(ICommandStackRepository<Domain.CustomerDomainModel> repository) 
            : base(repository)
        {
        }

        public void Execute(CreateCustomerCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var aggregate = new Domain.CustomerDomainModel();
            aggregate.CreateNewCustomer(
                command.Id, 
                command.Person.FirstName, 
                command.Person.LastName, 
                command.Person.IdCard,
                command.Person.IdNumber,
                command.Person.Dob, 
                command.Contact.Email, 
                command.Contact.PhoneNumber,
                command.Address.Street,
                command.Address.Zip,
                command.Address.Hausnumber,
                command.Address.City,
                command.Address.State);
            aggregate.Version = 0;

            this.Repository.Save(aggregate, aggregate.Version);
        }
    }
}
