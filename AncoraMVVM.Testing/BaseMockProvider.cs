using AncoraMVVM.Base.Interfaces;
using AncoraMVVM.Base.IoC;
// ReSharper disable DoNotCallOverridableMethodsInConstructor
namespace AncoraMVVM.Testing
{
    public class BaseMockProvider : IObjectProvider
    {
        public static BaseMockProvider LastInstance { get; private set; }

        public BaseMockProvider()
        {
            LastInstance = this;
            NotificationService = GetSubstituteFor<INotificationService>();
            Dispatcher = GetSubstituteFor<IDispatcher>();
            Progress = GetSubstituteFor<IProgressIndicator>();
            NavigationService = GetSubstituteFor<INavigationService>();
            ConfigurationManager = GetSubstituteFor<IConfigurationManager>();
            Messager = GetSubstituteFor<IMessager>();
        }

        public INotificationService NotificationService { get; set; }
        public IDispatcher Dispatcher { get; set; }
        public IProgressIndicator Progress { get; set; }
        public INavigationService NavigationService { get; set; }
        public IConfigurationManager ConfigurationManager { get; set; }
        public IMessager Messager { get; set; }

        public virtual T GetSubstituteFor<T>() where T : class
        {
            return null;
        }

        public T Resolve<T>() where T : class
        {
            if (NotificationService is T)
                return (T)NotificationService;
            else if (Dispatcher is T)
                return (T)Dispatcher;
            else if (Progress is T)
                return (T)Progress;
            else if (NavigationService is T)
                return (T)NavigationService;
            else if (ConfigurationManager is T)
                return (T)ConfigurationManager;
            else if (Messager is T)
                return (T)Messager;
            else
                return GetSubstituteFor<T>();
        }

    }
}
