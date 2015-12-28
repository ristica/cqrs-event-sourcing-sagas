using BankAccount.CommandStack.Storage;
using BankAccount.CommandStack.Storage.Abstraction;
using BankAccount.CommandStack.Storage.CustomEventStore;
using BankAccount.CommandStack.Storage.NEventStore;
using BankAccount.Configuration.Utils;
using BankAccount.Infrastructure.Buses;
using BankAccount.Infrastructure.CommandHandling;
using BankAccount.Infrastructure.EventHandling;
using BankAccount.Infrastructure.Snapshoting;
using BankAccount.Infrastructure.Storage;
using BankAccount.QueryStack.Db;
using BankAccount.QueryStack.Reporting;
using EventStore;
using EventStore.Dispatcher;
using EventStore.Persistence.SqlPersistence.SqlDialects;
using StructureMap;

namespace BankAccount.Configuration
{
    public sealed class ServiceLocator
    {
        #region Fields

        private static readonly bool IsInitialized;
        private static readonly object LockThis = new object();
        private static readonly IContainer _container = new Container();

        #endregion

        #region C-Tor

        static ServiceLocator()
        {
            if (IsInitialized) return;
            lock (LockThis)
            {
                ContainerBootstrapper.BootstrapStructureMap(_container);

                CommandBus = _container.GetInstance<ICommandBus>();
                QueryStackRepository = _container.GetInstance<IQueryStackRepository>();
                SnapshotStore = _container.GetInstance<ISnapshotStore>();
                EventStore = _container.GetInstance<IEventStore>();
                CommandDatabase = _container.GetInstance<ICommandStackDatabase>();
                QueryDatabase = _container.GetInstance<IQueryStackDatabase>();

                IsInitialized = true;
            }
        }

        #endregion

        #region Properties

        public static IContainer Container => _container;

        public static ICommandBus CommandBus { get; }

        public static IQueryStackRepository QueryStackRepository { get; }

        public static ISnapshotStore SnapshotStore { get; }

        public static IEventStore EventStore { get; }

        public static ICommandStackDatabase CommandDatabase { get; }

        public static IQueryStackDatabase QueryDatabase { get; }

        #endregion
    }

    public static class ContainerBootstrapper
    {
        public static void BootstrapStructureMap(IContainer container)
        {
            container.Configure(x =>
            {
                x.For<IEventStore>().Singleton().Use<CustomEventStore>();
                x.For<ISnapshotStore>().Singleton().Use<CustomSnapshotStore>();
                x.For<ICommandHandlerFactory>().Use<StructureMapCommandHandlerFactory>();
                x.For<IEventHandlerFactory>().Use<StructureMapEventHandlerFactory>();
                x.For<ICommandBus>().Use<CommandBus>();
                x.For<IEventBus>().Use<EventBus>();
                x.For<IQueryStackRepository>().Use<QueryStackRepository>();
                x.For<ICommandStackDatabase>().Use<CommandStackDatabase>();
                x.For<IQueryStackDatabase>().Use<QueryStackDatabase>();
                x.For<IDispatchCommits>().Use<CommitsDispatcher>();

                //x.For(typeof(ICommandStackRepository<>)).Use(typeof(CustomCommandStackRepository<>));
                x.For(typeof(ICommandStackRepository<>)).Use(typeof(NEventStoreCommandStackRepository<>));

                x.For<IStoreEvents>().Singleton().Use(s => CreateEventStore(s.GetInstance<IDispatchCommits>()));
            });
        }

        private static IStoreEvents CreateEventStore(IDispatchCommits bus)
        {
            return Wireup.Init()
                         .LogToOutputWindow()
                         //.UsingInMemoryPersistence()
                         .UsingSqlPersistence("NEventStore") // Connection string is in app.config
                         .WithDialect(new MsSqlDialect())
                         .EnlistInAmbientTransaction() // two-phase commit
                         .InitializeStorageEngine()
                         //.TrackPerformanceInstance("example")
                         .UsingJsonSerialization()
                         .Compress()
                         //.EncryptWith(EncryptionKey)
                         .HookIntoPipelineUsing(new AuthorizationPipelineHook())
                         .UsingSynchronousDispatchScheduler()
                         .DispatchTo(bus)
                         .Build();
        }
    }
}
