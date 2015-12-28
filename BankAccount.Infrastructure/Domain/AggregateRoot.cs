using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BankAccount.Infrastructure.Eventing;
using EventStore;

namespace BankAccount.Infrastructure.Domain
{
    public abstract class AggregateRoot : IEventProvider
    {
        #region Fields

        private readonly List<Event> _changes;

        #endregion

        #region Properties

        public Guid Id { get; set; }
        public int Version { get; set; }
        //private int EventVersion { get; set; }

        #endregion

        #region C-Tor

        protected AggregateRoot()
        {
            this._changes = new List<Event>();
        }

        #endregion

        public IEnumerable<Event> GetUncommittedChanges()
        {
            return this._changes;
        }

        public void MarkChangesAsCommitted()
        {
            this._changes.Clear();
        }

        public void LoadsFromHistory(IEnumerable<object> history)
        {
            foreach (var e in history)
            {
                ApplyChangeFromHistory(e);
            }

            #region CustomEventStore

            //Version = ((Event)history.Last()).Version;

            #endregion

            #region NEventStore

            var ev = (EventMessage)history.Last();
            var @event = Converter.ChangeTo(ev.Body, ev.Body.GetType());
            Version = @event.Version;

            #endregion
        }

        protected void ApplyChange(Event @event, bool addToUncommitedChanges = true)
        {
            dynamic d = this;
            d.Handle(Converter.ChangeTo(@event, @event.GetType()));

            if (addToUncommitedChanges)
            {
                this._changes.Add(@event);
            }
        }

        private void ApplyChangeFromHistory(object @event)
        {
            dynamic d = this;

            #region CustomEventStore

            //d.Handle(Converter.ChangeTo(@event, @event.GetType()));

            #endregion

            #region NEventStore

            var ev = @event as EventMessage;
            if (ev != null)
            {
                d.Handle(Converter.ChangeTo(ev.Body, ev.Body.GetType()));
            }
            else
            {
                throw new ArgumentNullException($"@event is not of type EventMessage");
            }

            #endregion
        }
    }
}
