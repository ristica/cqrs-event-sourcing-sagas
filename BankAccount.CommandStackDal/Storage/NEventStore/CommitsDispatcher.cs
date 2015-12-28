using System;
using System.Linq;
using BankAccount.Infrastructure;
using BankAccount.Infrastructure.Buses;
using EventStore;
using EventStore.Dispatcher;

namespace BankAccount.CommandStackDal.Storage.NEventStore
{
    public class CommitsDispatcher : IDispatchCommits
    {
        #region Fields

        private readonly IEventBus _eventBus;

        #endregion

        #region C-Tor

        public CommitsDispatcher(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        #endregion

        #region IDispatchCommits impementation

        public void Dispatch(Commit commit)
        {
            try
            {
                foreach (var ev in commit.Events.Select(@event => Converter.ChangeTo(@event.Body, @event.Body.GetType())))
                {
                    this._eventBus.Publish(ev);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            
        }

        #endregion
    }
}
