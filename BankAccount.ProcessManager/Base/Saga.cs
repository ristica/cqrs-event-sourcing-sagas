using System;
using BankAccount.Infrastructure.Buses;
using BankAccount.Infrastructure.Storage;
using EventStore;

namespace BankAccount.ProcessManager.Base
{
    public abstract class Saga
    {
        #region Properties

        protected Guid SagaId { get; set; }
        protected ISagaBus Bus { get; private set; }
        protected IStoreEvents EventStore { get; private set; }
        protected ICommandStackRepository<Domain.BankAccount> Repository { get; private set; }

        #endregion

        #region C-Tor

        protected Saga(
            ISagaBus bus, 
            IStoreEvents eventStore, 
            ICommandStackRepository<Domain.BankAccount> repository)
        {
            if (bus == null)
            {
                throw new ArgumentNullException($"bus");
            }
            if (eventStore == null)
            {
                throw new ArgumentNullException($"eventStore");
            }
            if (repository == null)
            {
                throw new ArgumentNullException($"repository");
            }

            this.Bus = bus;
            this.EventStore = eventStore;
            this.Repository = repository;
        }

        #endregion
    }
}
