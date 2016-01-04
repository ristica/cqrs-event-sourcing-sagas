using System;
using System.Linq;
using BankAccount.Infrastructure;
using BankAccount.Infrastructure.Buses;
using EventStore;
using EventStore.Dispatcher;

namespace BankAccount.EventStore
{
    public class CommitsDispatcher : IDispatchCommits
    {
        #region Fields

        //private readonly IEventBus _eventBus;
        private readonly ISagaBus _bus;

        #endregion

        #region C-Tor

        public CommitsDispatcher(ISagaBus us)
        {
            this._bus = us;
        }

        #endregion

        #region IDispatchCommits impementation

        public void Dispatch(Commit commit)
        {
            try
            {
                foreach (var ev in commit.Events.Select(@event => Converter.ChangeTo(@event.Body, @event.Body.GetType())))
                {
                    //this._bus.Publish(ev);
                    this._bus.RaiseEvent(ev);
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
