﻿using BankAccount.Commands;
using BankAccount.Infrastructure;
using BankAccount.Infrastructure.Buses;
using BankAccount.Infrastructure.Storage;
using BankAccount.ProcessManager.Base;

namespace BankAccount.ProcessManager
{
    public class DeleteCustomerSaga : Saga,
        IAmStartedBy<DeleteCustomerCommand>
    {
        #region C-Tor

        public DeleteCustomerSaga(
            IBus bus, 
            ICommandStackRepository repository) 
            : base(bus, repository)
        {
        }

        #endregion

        #region Handling commands

        public void Handle(DeleteCustomerCommand message)
        {
            var aggregate = this.Repository.GetById<Domain.CustomerDomainModel>(message.Id);
            aggregate.DeleteCustomer();
            this.Repository.Save(aggregate);
        }

        #endregion
    }
}
