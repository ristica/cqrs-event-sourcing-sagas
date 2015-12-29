using System;
using BankAccount.CommandStackDal.Storage.Abstraction;

namespace BankAccount.EventHandlers.Base
{
    public abstract class BaseBankAccountEventHandler
    {
        protected readonly ICommandStackDatabase Database;

        protected BaseBankAccountEventHandler(ICommandStackDatabase database)
        {
            if (database == null)
            {
                throw new ArgumentNullException($"Database was not initialized");
            }

            this.Database = database;
        }
    }
}
