using System;
using System.Collections.Generic;
using System.Linq;
using BankAccount.Infrastructure.EventHandling;
using BankAccount.Infrastructure.Eventing;
using Microsoft.Practices.Unity;

namespace BankAccount.Configuration.Utils
{
    public class EventHandlerFactory : IEventHandlerFactory
    {
        public IEnumerable<IEventHandler<T>> GetHandlers<T>() where T : Event
        {
            var handlers = GetHandlerType<T>();

            var lstHandlers = handlers.Select(
                handler => (IEventHandler<T>)IoCServiceLocator.Container.Resolve(handler)).ToList();
            return lstHandlers;
        }

        private static IEnumerable<Type> GetHandlerType<T>() where T : Event
        {
            var types = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!assembly.GetName().FullName.ToLowerInvariant().Contains("commandstack"))
                    continue;

                var handlers = assembly
                    .GetExportedTypes()
                    .Where(x => x.GetInterfaces().Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(IEventHandler<>)))
                    .Where(h => h.GetInterfaces().Any(ii => ii.GetGenericArguments().Any(aa => aa == typeof(T))))
                    .ToList();

                types.AddRange(handlers);
            }

            return types;
        }
    }
}
