using BankAccount.CommandStackDal;
using BankAccount.CommandStackDal.Abstraction;
using BankAccount.Configuration.Buses;
using BankAccount.Denormalizers.Db;
using BankAccount.Denormalizers.Denormalizer;
using BankAccount.EventStore;
using BankAccount.Infrastructure.Buses;
using BankAccount.Infrastructure.Storage;
using BankAccount.ProcessManager;
using BankAccount.QueryStackDal;
using EventStore;
using EventStore.Dispatcher;
using EventStore.Persistence.SqlPersistence.SqlDialects;
using Microsoft.Practices.Unity;

namespace BankAccount.Configuration
{
    public static class IoCServiceLocator
    {
        #region Fields

        private static readonly bool IsInitialized;
        private static readonly object LockThis = new object();
        private static readonly  IUnityContainer _container = new UnityContainer();

        #endregion

        #region C-Tor

        static IoCServiceLocator()
        {
            if (IsInitialized) return;
            lock (LockThis)
            {
                Bootstrapper.Initialize(Container);

                SagaBus                 = _container.Resolve<ISagaBus>();
                QueryStackRepository    = _container.Resolve<IQueryStackRepository>();

                IsInitialized           = true;
            }
        }

        #endregion

        #region Properties

        public static IUnityContainer Container => _container;
        public static ISagaBus SagaBus { get; }
        public static IQueryStackRepository QueryStackRepository { get; }

        #endregion
    }

    public static class Bootstrapper
    {
        public static void Initialize(IUnityContainer container)
        {
            container.RegisterType <ISagaBus, SagaBus>();

            container.RegisterType <IQueryStackRepository, QueryStackRepository> ();
            container.RegisterType(typeof(ICommandStackRepository<>), typeof(NEventStoreCommandStackRepository<>));

            container.RegisterType <ICommandStackDatabase, CommandStackDatabase> ();
            container.RegisterType <IDatabase, Database>();

            container.RegisterType <IDispatchCommits, CommitsDispatcher> ();
            container.RegisterInstance<IStoreEvents>(CreateEventStore(container.Resolve<IDispatchCommits>()));

            // Bus handlers
            RegisterDenormalizer(container);

            RegisterSagaHandlers(container);
        }

        private static void RegisterSagaHandlers(IUnityContainer container)
        {
            var bus = container.Resolve<ISagaBus>();

            bus.RegisterSaga<CreateCustomerSaga>();
            bus.RegisterSaga<ChangePersonDetailsSaga>();
            bus.RegisterSaga<ChangeContactDetailsSaga>();
            bus.RegisterSaga<ChangeAddressDetailsSaga>();
            bus.RegisterSaga<DeleteCustomerSaga>();
        }

        private static void RegisterDenormalizer(IUnityContainer container)
        {
            var bus = container.Resolve<ISagaBus>();

            bus.RegisterHandler<CreateCustomerDenormalizer>();
            bus.RegisterHandler<UpdateCustomerDenormalizer>();
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
