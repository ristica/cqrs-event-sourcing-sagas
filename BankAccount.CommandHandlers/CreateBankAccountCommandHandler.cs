using System;
using BankAccount.CommandHandlers.Base;
using BankAccount.Commands;
using BankAccount.Infrastructure.CommandHandling;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.CommandHandlers
{
    public class CreateBankAccountCommandHandler : BaseBankAccountCommandHandler, ICommandHandler<CreateBankAccountCommand>
    {
        public CreateBankAccountCommandHandler(ICommandStackRepository<Domain.BankAccount> repository) 
            : base(repository)
        {
        }

        public void Execute(CreateBankAccountCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var aggregate = new Domain.BankAccount();
            aggregate.CreateNewBankAccount(
                command.Id, 
                command.FirstName, 
                command.LastName, 
                command.IdCard,
                command.IdNumber,
                command.Dob, 
                command.Email, 
                command.Phone,
                command.Money.Balance,
                command.Money.Currency,
                command.Address.Street,
                command.Address.Zip,
                command.Address.Hausnumber,
                command.Address.City,
                command.Address.State);
            aggregate.Version = -1;

            this.Repository.Save(aggregate, aggregate.Version);
        }
    }
}
