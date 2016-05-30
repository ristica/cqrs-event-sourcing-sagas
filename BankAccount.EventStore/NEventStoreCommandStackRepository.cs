using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using BankAccount.Infrastructure.Domain;
using BankAccount.Infrastructure.Storage;
using EventStore;

namespace BankAccount.EventStore
{
    public sealed class NEventStoreCommandStackRepository : ICommandStackRepository
    {
        #region Fields

        private readonly IStoreEvents _eventStore;

        #endregion

        #region C-Tor

        public NEventStoreCommandStackRepository(
            IStoreEvents eventStore)
        {
            this._eventStore = eventStore;
        }

        #endregion

        #region ICommandStackRepository implementation

        /// <summary>
        /// here we are saving aggregate's event into event store
        /// in the next step the dispatcher will notify the bus
        /// about the saved events, so that the bus can raisse them
        /// </summary>
        /// <param name="aggregate"></param>
        public void Save(AggregateRoot aggregate)
        {
            using (var scope = new TransactionScope())
            {
                OpenCreateStream(this._eventStore, aggregate);
                scope.Complete();
            }
        }

        /// <summary>
        /// replaying the aggregate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById<T>(Guid id) where T : AggregateRoot, new()
        {
            try
            {
                var obj = new T();
                int version = 0;

                var latestSnapshot = this._eventStore.Advanced.GetSnapshot(id, int.MaxValue);
                if (latestSnapshot?.Payload != null)
                {
                    obj = (T)Convert.ChangeType(latestSnapshot.Payload, latestSnapshot.Payload.GetType());
                    version = latestSnapshot.StreamRevision + 1;
                }

                IEnumerable<Commit> commits = this._eventStore.Advanced.GetFrom(id, version, int.MaxValue).ToList();

                foreach (var c in commits)
                {
                    obj.LoadsFromHistory(c.Events);
                }

                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Helpers

        private void OpenCreateStream(IStoreEvents store, AggregateRoot aggregate)
        {
            var changes = aggregate.GetUncommittedChanges().ToList();
            if (!changes.Any()) return;

            using (var stream = store.OpenStream(aggregate.Id, 0, int.MaxValue))
            {
                var version = aggregate.Version;
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
        }

        #endregion
    }
}
