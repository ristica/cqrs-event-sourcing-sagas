using BankAccount.Configuration.Buses;
using BankAccount.Denormalizers.Dal;
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

                Bus                     = _container.Resolve<IBus>();
                QueryStackRepository    = _container.Resolve<IQueryStackRepository>();

                IsInitialized           = true;
            }
        }

        #endregion

        #region Properties

        public static IUnityContainer Container => _container;
        public static IBus Bus { get; }
        public static IQueryStackRepository QueryStackRepository { get; }

        #endregion
    }

    public static class Bootstrapper
    {
        public static void Initialize(IUnityContainer container)
        {
            container.RegisterType <IBus, Bus>();

            container.RegisterType <IQueryStackRepository, QueryStackRepository> ();
            container.RegisterType <ICommandStackRepository, NEventStoreCommandStackRepository>();

            container.RegisterType(typeof (IDenormalizerRepository<>), typeof (AccountDenormalizerRepository), "Account");
            container.RegisterType(typeof (IDenormalizerRepository<>), typeof(CustomerDenormalizerRepository), "Customer");

            container.RegisterType <IDispatchCommits, CommitsDispatcher> ();
            container.RegisterInstance<IStoreEvents>(CreateEventStore(container.Resolve<IDispatchCommits>()));

            // Bus handlers
            RegisterDenormalizer(container);

            RegisterSagaHandlers(container);
        }

        private static void RegisterSagaHandlers(IUnityContainer container)
        {
            var bus = container.Resolve<IBus>();

            bus.RegisterSaga<CreateCustomerSaga>();
            bus.RegisterSaga<ChangePersonDetailsSaga>();
            bus.RegisterSaga<ChangeContactDetailsSaga>();
            bus.RegisterSaga<ChangeAddressDetailsSaga>();
            bus.RegisterSaga<DeleteCustomerSaga>();

            bus.RegisterSaga<AccountSaga>();
            bus.RegisterSaga<MoneyTransferSaga>();
        }

        private static void RegisterDenormalizer(IUnityContainer container)
        {
            var bus = container.Resolve<IBus>();

            bus.RegisterHandler<CreateCustomerDenormalizer>();
            bus.RegisterHandler<UpdateCustomerDenormalizer>();
            bus.RegisterHandler<AccountDenormalizer>();
            bus.RegisterHandler<MoneyTransferDenormalizer>();
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
