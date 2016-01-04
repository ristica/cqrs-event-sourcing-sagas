using System;
using System.Collections.Generic;
using System.Linq;
using BankAccount.Infrastructure.Eventing;
using EventStore;

namespace BankAccount.Infrastructure.Domain
{
    public abstract class AggregateRoot
    {
        #region Fields

        private readonly List<DomainEvent> _changes;

        #endregion

        #region Properties

        public Guid Id { get; set; }
        public int Version { get; set; }
        //private int EventVersion { get; set; }

        #endregion

        #region C-Tor

        protected AggregateRoot()
        {
            this._changes = new List<DomainEvent>();
        }

        #endregion

        public IEnumerable<DomainEvent> GetUncommittedChanges()
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

            var ev = (EventMessage)history.Last();
            var @event = Converter.ChangeTo(ev.Body, ev.Body.GetType());
            Version = @event.Version;
        }

        protected void ApplyChange(DomainEvent @event, bool addToUncommitedChanges = true)
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
            var ev = @event as EventMessage;
            if (ev != null)
            {
                d.Handle(Converter.ChangeTo(ev.Body, ev.Body.GetType()));
            }
            else
            {
                throw new ArgumentNullException($"@event is not of type EventMessage");
            }
        }
    }
}
