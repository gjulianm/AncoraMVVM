using AncoraMVVM.Base.Interfaces;
using AncoraMVVM.Base.IoC;
using NSubstitute;

namespace AncoraMVVM.Base.Tests
{
    public class MockProvider : IObjectProvider
    {
        public static MockProvider LastInstance { get; private set; }

        public MockProvider()
        {
            LastInstance = this;
            NotificationService = Substitute.For<INotificationService>();
            Dispatcher = Substitute.For<IDispatcher>();
            Progress = Substitute.For<IProgressIndicator>();
            NavigationService = Substitute.For<INavigationService>();
            ConfigurationManager = Substitute.For<IConfigurationManager>();

            Dispatcher.IsUIThread.Returns(true);
        }

        public INotificationService NotificationService { get; set; }
        public IDispatcher Dispatcher { get; set; }
        public IProgressIndicator Progress { get; set; }
        public INavigationService NavigationService { get; set; }
        public IConfigurationManager ConfigurationManager { get; set; }
        public IMessager Messager { get; set; }

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
                return Substitute.For<T>();
        }

    }
}
