using System;
using BankAccount.CommandHandlers.Base;
using BankAccount.Commands;
using BankAccount.Infrastructure.CommandHandling;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.CommandHandlers
{
    public class ChangePersonDetailsCommandHandler : BaseBankAccountCommandHandler, ICommandHandler<ChangePersonDetailsCommand>
    {
        public ChangePersonDetailsCommandHandler(ICommandStackRepository<Domain.CustomerDomainModel> repository) 
            : base(repository)
        {
        }

        public void Execute(ChangePersonDetailsCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var aggregate = this.Repository.GetById(command.Id);

            aggregate.ChangePerson(
                command.FirstName, 
                command.LastName, 
                command.IdCard, 
                command.IdNumber);

            this.Repository.Save(aggregate, aggregate.Version);
        }
    }
}
