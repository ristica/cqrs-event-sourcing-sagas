using System;
using BankAccount.CommandHandlers.Base;
using BankAccount.Commands;
using BankAccount.Infrastructure.CommandHandling;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.CommandHandlers
{
    public class ChangeContactDetailsCommandHandler : BaseBankAccountCommandHandler, ICommandHandler<ChangeContactDetailsCommand>
    {
        public ChangeContactDetailsCommandHandler(ICommandStackRepository<Domain.BankAccount> repository) 
            : base(repository)
        {
        }

        public void Execute(ChangeContactDetailsCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var aggregate = this.Repository.GetById(command.Id);

            aggregate.ChangeContact(command.Email, command.Phone);

            this.Repository.Save(aggregate, aggregate.Version);
        }
    }
}
