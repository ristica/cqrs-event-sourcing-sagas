using System;
using BankAccount.Infrastructure.Buses;
using BankAccount.Infrastructure.EventHandling;
using BankAccount.Infrastructure.Eventing;
using Microsoft.Practices.Unity;

namespace BankAccount.Configuration.Buses
{
    public sealed class EventBus : Bus, IEventBus
    {
        public void Publish<T>(T @event) where T : Event
        {
            try
            {
                var handler = this.GetHandler<T>();
                ((IEventHandler<T>)IoCServiceLocator.Container.Resolve(handler)).Handle(@event);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
