using System;
using BankAccount.Configuration.Buses.Base;
using BankAccount.Infrastructure.Buses;
using BankAccount.Infrastructure.CommandHandling;
using BankAccount.Infrastructure.Commanding;
using Microsoft.Practices.Unity;

namespace BankAccount.Configuration.Buses
{
    public sealed class CommandBus : Bus, ICommandBus
    {
        public void Send<T>(T command) where T : Command
        {
            try
            {
                var handler = this.GetHandler<T>();
                ((ICommandHandler<T>)IoCServiceLocator.Container.Resolve(handler)).Execute(command);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
