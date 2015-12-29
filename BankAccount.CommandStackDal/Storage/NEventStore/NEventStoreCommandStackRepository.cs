using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using BankAccount.CommandStackDal.Storage.Abstraction;
using BankAccount.Infrastructure.Domain;
using BankAccount.Infrastructure.Storage;
using EventStore;

namespace BankAccount.CommandStackDal.Storage.NEventStore
{
    public sealed class NEventStoreCommandStackRepository<T> : ICommandStackRepository<T> where T : AggregateRoot, new()
    {
        #region Fields

        private readonly IStoreEvents _eventStore;
        private readonly ICommandStackDatabase _database;

        #endregion

        #region C-Tor

        public NEventStoreCommandStackRepository(IStoreEvents eventStore, ICommandStackDatabase database)
        {
            this._eventStore = eventStore;
            this._database = database;
        }

        #endregion

        #region ICommandStackRepository implementation

        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            using (var scope = new TransactionScope())
            {
                OpenCreateStream(this._eventStore, aggregate);
                scope.Complete();
            }
        }

        public T GetById(Guid id)
        {
            try
            {
                var obj = new T();
                IEnumerable<Commit> commits;

                // check for last snapshot (if there are any)
                var latestSnapshot = this._eventStore.Advanced.GetSnapshot(id, int.MaxValue);
                if (latestSnapshot?.Payload != null)
                {
                    obj = (T)Convert.ChangeType(latestSnapshot.Payload, latestSnapshot.Payload.GetType());
                    commits = this._eventStore.Advanced.GetFrom(id, latestSnapshot.StreamRevision + 1, int.MaxValue).ToList();
                }
                else
                {
                    commits = this._eventStore.Advanced.GetFrom(id, 0, int.MaxValue).ToList();
                }

                foreach (var c in commits)
                {
                    obj.LoadsFromHistory(c.Events);
                }

                return obj;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Helpers

        private void OpenCreateStream(IStoreEvents store, AggregateRoot aggregate)
        {
            if (!aggregate.GetUncommittedChanges().Any()) return;

            var changes = aggregate.GetUncommittedChanges().ToList();
            using (var stream = store.OpenStream(aggregate.Id, 0, int.MaxValue))
            {
                var version = aggregate.Version < 0 ? 0 : aggregate.Version;
                foreach (var @event in changes)
                {
                    version++;
                    @event.Version = version;
                    stream.Add(new EventMessage { Body = @event });
                    stream.CommitChanges(Guid.NewGuid());

                    // make a snapshot every 10th event
                    if (version % 10 != 0) continue;

                    aggregate.Version = version;
                    store.Advanced.AddSnapshot(new Snapshot(aggregate.Id, version, aggregate));
                }
            }

            aggregate.MarkChangesAsCommitted();
            this._database.UpdateFromCache();
        }

        #endregion
    }
}
