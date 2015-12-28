using System.Collections.Generic;

namespace BankAccount.Infrastructure.Eventing
{
    public interface IEventProvider
    {
        void LoadsFromHistory(IEnumerable<object> history);
        IEnumerable<Event> GetUncommittedChanges();
    }
}
