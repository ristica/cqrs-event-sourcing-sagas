using System;
using System.Collections.Generic;
using System.Linq;
using BankAccount.Infrastructure.Buses;
using BankAccount.Infrastructure.Commanding;
using BankAccount.Infrastructure.Eventing;
using Infrastructure;
using Microsoft.Practices.Unity;

namespace BankAccount.Configuration.Buses
{
    public sealed class SagaBus : ISagaBus
    {
        #region Fields

        private static readonly IDictionary<Type, Type> RegisteredSagas = new Dictionary<Type, Type>();
        private static readonly IList<Type> RegisteredHandlers = new List<Type>();

        #endregion

        #region C-Tor

        public SagaBus()
        {
        }

        #endregion

        #region ISagaBus

        public void Send<T>(T command) where T : Command
        {
            this._Send(command);
        }

        public void RaiseEvent<T>(T @event) where T : Event
        {
            this._Send(@event);
        }

        public void RegisterSaga<T>()
        {
            var sagaType = typeof(T);
            if (!sagaType.GetInterfaces().Any(i => i.Name.StartsWith(typeof(IAmStartedBy<>).Name)))
            {
                throw new InvalidOperationException("The specified saga must implement the IAmStartedBy<T> interface.");
            }

            var messageType = sagaType.
                GetInterfaces().
                First(i => i.Name.StartsWith(typeof(IAmStartedBy<>).Name)).
                GenericTypeArguments.
                First();

            RegisteredSagas.Add(messageType, sagaType);
        }

        public void RegisterHandler<T>()
        {
            RegisteredHandlers.Add(typeof(T));
        }

        #endregion

        #region Helpers

        private void _Send<T>(T message)
        {
            DeliverMessageToSagas(message);
            DeliverMessageToHandlers(message);
        }

        private void DeliverMessageToSagas<T>(T message)
        {
            var messageType = message.GetType();

            var openInterface = typeof(IAmStartedBy<>);
            var closedInterface = openInterface.MakeGenericType(messageType);
            var sagasToStartup = (from s in RegisteredSagas.Values
                                  where closedInterface.IsAssignableFrom(s)
                                  select s).ToList();

            if (sagasToStartup.Any())
            {
                foreach (var sagaInstance in sagasToStartup.Select(s => IoCServiceLocator.Container.Resolve(s)))
                {
                    ((dynamic)sagaInstance).Handle((dynamic)message);
                }
            }
            else
            {
                openInterface = typeof(IHandleMessage<>);
                closedInterface = openInterface.MakeGenericType(messageType);
                var sagasToNotify = from s in RegisteredSagas.Values
                                    where closedInterface.IsAssignableFrom(s)
                                    select s;
                foreach (var sagaInstance in sagasToNotify.Select(s => IoCServiceLocator.Container.Resolve(s)))
                {
                    ((dynamic)sagaInstance).Handle((dynamic)message);
                }
            }
        }

        private void DeliverMessageToHandlers<T>(T message)
        {
            if (RegisteredHandlers == null || !RegisteredHandlers.Any())
                return;

            var messageType = message.GetType();
            var openInterface = typeof(IHandleMessage<>);
            var closedInterface = openInterface.MakeGenericType(messageType);
            var handlersToNotify = from h in RegisteredHandlers
                                   where closedInterface.IsAssignableFrom(h)
                                   select h;
            foreach (var handlerInstance in handlersToNotify.Select(h => IoCServiceLocator.Container.Resolve(h)))
            {
                ((dynamic)handlerInstance).Handle((dynamic)message);
            }
        }

        #endregion
    }
}
