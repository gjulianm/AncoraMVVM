using AncoraMVVM.Base.Interfaces;
using AncoraMVVM.Base.IoC;
using AncoraMVVM.Phone.Implementations;

namespace AncoraMVVM.Phone
{
    public class PhoneDependencyModule : DependencyModule
    {
        public PhoneDependencyModule()
            : base()
        {
            AddDep<INotificationService, NotificationService>();
            AddDep<IDispatcher, Dispatcher>();
            AddDep<IProgressIndicator, GlobalProgress>(true);
            AddDep<INavigationService, NavigationService>(true);
            AddDep<IConfigurationManager, ConfigurationManager>(true);
        }
    }
}
