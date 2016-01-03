using System;
using BankAccount.Infrastructure.Buses;
using BankAccount.Infrastructure.Storage;

namespace BankAccount.ProcessManager.Base
{
    public abstract class Saga
    {
        #region Properties

        protected Guid SagaId { get; set; }
        protected ISagaBus Bus { get; private set; }
        protected ICommandStackRepository<Domain.CustomerDomainModel> Repository { get; private set; }

        #endregion

        #region C-Tor

        protected Saga(
            ISagaBus bus, 
            ICommandStackRepository<Domain.CustomerDomainModel> repository)
        {
            if (bus == null)
            {
                throw new ArgumentNullException($"bus");
            }

            if (repository == null)
            {
                throw new ArgumentNullException($"repository");
            }

            this.Bus = bus;
            this.Repository = repository;
        }

        #endregion
    }
}
