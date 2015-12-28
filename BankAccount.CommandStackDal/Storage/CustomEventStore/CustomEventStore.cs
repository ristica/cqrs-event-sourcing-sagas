using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using BankAccount.CommandStackDal.Exceptions;
using BankAccount.DbModel.Entities;
using BankAccount.DbModel.EventDb;
using BankAccount.Infrastructure;
using BankAccount.Infrastructure.Buses;
using BankAccount.Infrastructure.Domain;
using BankAccount.Infrastructure.Memento;
using BankAccount.Infrastructure.Storage;
using Newtonsoft.Json;

namespace BankAccount.CommandStackDal.Storage.CustomEventStore
{
    public class CustomEventStore : IEventStore
    {
        #region Fields

        private readonly List<BaseMemento> _mementos;
        private readonly IEventBus _eventBus;

        #endregion

        #region C-Tor

        public CustomEventStore(IEventBus eventBus)
        {
            if (eventBus == null)
            {
                throw new InvalidOperationException("EventBus is not initialized.");
            }

            _mementos = new List<BaseMemento>();
            _eventBus = eventBus;
        }

        #endregion

        #region IEventStore implementation

        public IEnumerable<object> GetEvents(Guid aggregateId)
        {
            using (var ctx = new EventDbContext())
            {
                var events = ctx.EventSet.Where(p => p.AggregateId == aggregateId).Select(p => p).ToList();
                if (!events.Any())
                {
                    throw new AggregateNotFoundException($"Aggregate with Id: {aggregateId} was not found");
                }
                foreach (var ev in events)
                {
                    yield return JsonConvert.DeserializeObject(ev.Body, Type.GetType(ev.EventName));
                }
            }
        }

        public IEnumerable<object> GetEvents(Guid aggregateId, int snapshotVersion)
        {
            using (var ctx = new EventDbContext())
            {
                var events = ctx.EventSet.Where(p => p.AggregateId == aggregateId && p.Sequence > snapshotVersion).Select(p => p);
                foreach (var ev in events)
                {
                    yield return JsonConvert.DeserializeObject(ev.Body, Type.GetType(ev.EventName));
                }
            }
        }

        public int? GetLastEventVersion(Guid aggregateId)
        {
            using (var ctx = new EventDbContext())
            {
                var last = ctx.EventSet.ToList().LastOrDefault(e => e.AggregateId == aggregateId);
                return last?.Sequence;
            }
        }

        public void Save(AggregateRoot aggregate)
        {
            var uncommittedChanges = aggregate.GetUncommittedChanges().ToList();
            var version = aggregate.Version;

            using (var ctx = new EventDbContext())
            {
                foreach (var @event in uncommittedChanges)
                {
                    version++;

                    if (version > 3)
                    {
                        if (version % 4 == 0)
                        {
                            var originator = (IOriginator)aggregate;
                            var memento = originator.GetMemento();
                            memento.Version = version;
                            SaveMemento(memento);
                        }
                    }

                    // important => or there is wrong version in body !!!
                    @event.Version = version;

                    ctx.EventSet.Add(
                        new EventEntity
                        {
                            AggregateId = @event.AggregateId,
                            EventDate = DateTime.Now,
                            Sequence = version,
                            EventName = @event.ToString(),
                            Body = new JavaScriptSerializer().Serialize(@event)
                        });

                }

                ctx.SaveChanges();
            }

            foreach (var @event in uncommittedChanges)
            {
                var desEvent = Converter.ChangeTo(@event, @event.GetType());
                this._eventBus.Publish(desEvent);
            }

            // remove all changes from the uncommited list
            aggregate.MarkChangesAsCommitted();
        }

        public T GetMemento<T>(Guid aggregateId) where T : BaseMemento
        {
            var memento = _mementos.Where(m => m.Id == aggregateId).Select(m=>m).LastOrDefault();
            return (T) memento;
        }

        public void SaveMemento(BaseMemento memento)
        {
            _mementos.Add(memento);
        }

        #endregion
    }
}
