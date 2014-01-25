using AncoraMVVM.Base;
using AncoraMVVM.Base.Interfaces;
using AncoraMVVM.Base.IoC;
using AncoraMVVM.Base.Tasks;
using AncoraMVVM.Phone.Implementations;
using AncoraMVVM.Phone7.Tasks;

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
            AddDep<IMessager, Messager>(true);
        }
    }
}
