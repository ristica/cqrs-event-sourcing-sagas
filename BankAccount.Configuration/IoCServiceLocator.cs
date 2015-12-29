using BankAccount.CommandHandlers;
using BankAccount.CommandStackDal.Storage;
using BankAccount.CommandStackDal.Storage.Abstraction;
using BankAccount.CommandStackDal.Storage.NEventStore;
using BankAccount.Configuration.Buses;
using BankAccount.EventHandlers;
using BankAccount.Infrastructure.Buses;
using BankAccount.Infrastructure.Storage;
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

                CommandBus = _container.Resolve<ICommandBus>();
                QueryStackRepository = _container.Resolve<IQueryStackRepository>();
                CommandDatabase = _container.Resolve<ICommandStackDatabase>();

                IsInitialized = true;
            }
        }

        #endregion

        #region Properties

        public static IUnityContainer Container => _container;

        public static ICommandBus CommandBus { get; }

        public static IQueryStackRepository QueryStackRepository { get; }

        public static ICommandStackDatabase CommandDatabase { get; }

        #endregion
    }

    public static class Bootstrapper
    {
        public static void Initialize(IUnityContainer container)
        {
            //container.RegisterType <ICommandHandlerFactory, CommandHandlerFactory> ();
            //container.RegisterType <IEventHandlerFactory, EventHandlerFactory> ();

            container.RegisterType <ICommandBus, CommandBus> ();
            container.RegisterType <IEventBus, EventBus> ();

            container.RegisterType <IQueryStackRepository, QueryStackRepository> ();
            container.RegisterType(typeof(ICommandStackRepository<>), typeof(NEventStoreCommandStackRepository<>));

            container.RegisterType <ICommandStackDatabase, CommandStackDatabase> ();

            container.RegisterType <IDispatchCommits, CommitsDispatcher> ();
            container.RegisterInstance<IStoreEvents>(CreateEventStore(container.Resolve<IDispatchCommits>()));

            // Bus handlers
            RegisterCommandHandlers(container);
            RegisterEventHandlers(container);
        }

        private static void RegisterEventHandlers(IUnityContainer container)
        {
            var bus = container.Resolve<IEventBus>();
            
            bus.RegisterHandler<BankAccountCreatedEventHandler>();
            bus.RegisterHandler<BankAccountDeletedEventHandler>();
            bus.RegisterHandler<CustomerChangedEventHandler>();
            bus.RegisterHandler<ContactChangedEventHandler>();
            bus.RegisterHandler<BalanceChangedEventHandler>();
            bus.RegisterHandler<CurrencyChangedEventHandler>();
            bus.RegisterHandler<AddressChangedEventHandler>();
        }

        private static void RegisterCommandHandlers(IUnityContainer container)
        {
            var bus = container.Resolve<ICommandBus>();

            bus.RegisterHandler<CreateBankAccountCommandHandler>();
            bus.RegisterHandler<DeleteBankAccountCommandHandler>();
            bus.RegisterHandler<ChangeAddressDetailsCommandHandler>();
            bus.RegisterHandler<ChangeCurrencyCommandHandler>();
            bus.RegisterHandler<TransferMoneyCommandHandler>();
            bus.RegisterHandler<ChangeCustomerDetailsCommandHandler>();
            bus.RegisterHandler<ChangeContactDetailsCommandHandler>();
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
