using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core;
using System.Linq;
using BankAccount.Infrastructure.Domain;
using BankAccount.Infrastructure.Eventing;
using BankAccount.Infrastructure.Exceptions;
using BankAccount.Infrastructure.Memento;
using BankAccount.Infrastructure.Snapshoting;
using BankAccount.Infrastructure.Storage;
using Newtonsoft.Json;

namespace BankAccount.CommandStackDal.Storage.CustomEventStore
{
    public class CustomCommandStackRepository<T> : ICommandStackRepository<T> where T : AggregateRoot, new()
    {
        #region Fields

        private readonly IEventStore _eventStore;
        private readonly ISnapshotStore _snapshotStore;
        private static readonly object LockStorage = new object();

        #endregion

        #region C-Tor

        public CustomCommandStackRepository(
            IEventStore store, 
            ISnapshotStore snapshotStore)
        {
            if (store == null)
            {
                throw new InvalidOperationException("EventStore is not initialized.");
            }

            if (snapshotStore == null)
            {
                throw new InvalidOperationException("SnapshotStore is not initialized.");
            }

            this._eventStore = store;
            this._snapshotStore = snapshotStore;
        }

        #endregion

        #region ICommandStackRepository

        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            if (!aggregate.GetUncommittedChanges().Any()) return;

            lock (LockStorage)
            {
                if (expectedVersion != -1)
                {
                    var lastSavedVersion = this._eventStore.GetLastEventVersion(aggregate.Id);
                    if (lastSavedVersion == null)
                    {
                        throw new ObjectNotFoundException($"No events for aggregate with id {aggregate.Id} found!");
                    }
                    if (lastSavedVersion.Value != expectedVersion)
                    {
                        throw new ConcurrencyException($"Aggregate {aggregate.Id} has been previously modified");
                    }
                }

                // save events to event store
                this._eventStore.Save(aggregate);

                // optinally => create a snapshot
                this.SaveSnapshot(aggregate);
            }
        }

        public T GetById(Guid id)
        {
            IEnumerable<object> events;
            Snapshot snapshot = null;
            var obj = new T();

            lock (LockStorage)
            {
                var memento = this._eventStore.GetMemento<BaseMemento>(id);
                if (memento != null)
                {
                    events = ((IEnumerable<Event>)this._eventStore.GetEvents(id)).Where(e => e.Version >= memento.Version).ToList();
                }
                else
                {
                    snapshot = this._snapshotStore.GetLastSnapshot(id);
                    events = snapshot != null ? 
                        this._eventStore.GetEvents(id, snapshot.LastEventSequence).ToList() : 
                        this._eventStore.GetEvents(id).ToList();
                }

                if (memento != null)
                    ((IOriginator)obj).SetMemento(memento);
            }

            if (snapshot != null)
            {
                if (!events.Any())
                {
                    return (T) JsonConvert.DeserializeObject(snapshot.Body, Type.GetType(snapshot.EntityTape));
                }

                obj = (T)JsonConvert.DeserializeObject(snapshot.Body, Type.GetType(snapshot.EntityTape));
                obj.LoadsFromHistory(events);
            }
            else
            {
                obj.LoadsFromHistory(events);
            }
            return obj;
        }

        #endregion

        #region Snapshot helper methods

        private void SaveSnapshot(AggregateRoot aggregateRoot)
        {
            var snapshotingEnabledKey = ConfigurationManager.AppSettings["SnapshotingEnabled"];
            if (snapshotingEnabledKey == null || snapshotingEnabledKey == "false")
            {
                return;
            }

            var snapshotOriginator = aggregateRoot as ISnapshotOriginator;
            if (snapshotOriginator == null)
                return;

            if (aggregateRoot.Version == -1)
            {
                var currentSnapshot = snapshotOriginator.GetCurrentSnapshot(0);
                currentSnapshot.LastEventSequence = 0;
                this._snapshotStore.SaveSnapshot(currentSnapshot);
            }
            else
            {
                if (!snapshotOriginator.ShouldTakeSnapshot())
                {
                    return;
                }

                var lastEventVersion = this._eventStore.GetLastEventVersion(aggregateRoot.Id);
                if (lastEventVersion == null || lastEventVersion.Value % 3 != 0)
                {
                    return;
                }

                var currentSnapshot = snapshotOriginator.GetCurrentSnapshot(lastEventVersion);
                currentSnapshot.LastEventSequence = lastEventVersion ?? 0;

                this._snapshotStore.SaveSnapshot(currentSnapshot);
            }
        }

        #endregion
    }
}
