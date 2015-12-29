using System;
using System.Collections.Generic;
using System.Linq;
using BankAccount.Infrastructure.Exceptions;

namespace BankAccount.Configuration.Buses.Base
{
    public abstract class Bus
    {
        private static readonly IList<Type> RegisteredHandlers = new List<Type>();

        public void RegisterHandler<T>()
        {
            RegisteredHandlers.Add(typeof(T));
        }

        protected Type GetHandler<T>()
        {
            var handler = RegisteredHandlers.FirstOrDefault(h => h.GetInterfaces().Any(ii => ii.GetGenericArguments().Any(aa => aa == typeof(T))));
            if (handler == null)
            {
                throw new UnregisteredDomainCommandException("no handler registered");
            }
            return handler;
        }
    }
}
