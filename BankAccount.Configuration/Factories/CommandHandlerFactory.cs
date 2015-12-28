using System;
using System.Collections.Generic;
using System.Linq;
using BankAccount.Infrastructure.CommandHandling;
using BankAccount.Infrastructure.Commanding;
using Microsoft.Practices.Unity;

namespace BankAccount.Configuration.Factories
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        public ICommandHandler<T> GetHandler<T>() where T : Command
        {
            try
            {
                var handlers = GetHandlerTypes<T>().ToList();

                var cmdHandler = handlers.Select(handler =>
                    (ICommandHandler<T>)IoCServiceLocator.Container.Resolve(handler)).FirstOrDefault();

                return cmdHandler;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        private IEnumerable<Type> GetHandlerTypes<T>() where T : Command
        {
            var types = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!assembly.GetName().FullName.ToLowerInvariant().Contains("commandstack"))
                    continue;

                var handlers = assembly
                    .GetExportedTypes()
                    .Where(x => x.GetInterfaces().Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
                    .Where(h => h.GetInterfaces().Any(ii => ii.GetGenericArguments().Any(aa => aa == typeof(T))))
                    .ToList();

                types.AddRange(handlers);
            }

            return types;
        }

    }
}
