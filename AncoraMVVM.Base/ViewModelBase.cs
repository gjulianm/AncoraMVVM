using AncoraMVVM.Base.Interfaces;
using AncoraMVVM.Base.IoC;
using System.ComponentModel;

namespace AncoraMVVM.Base
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected INavigationService Navigator { get; private set; }
        protected INotificationService Notificator { get; private set; }
        protected IDispatcher Dispatcher { get; private set; }
        protected IProgressIndicator Progress { get; private set; }
        protected IConfigurationManager Configuration { get; private set; }
        protected IMessager Messager { get; private set; }

        public ViewModelBase()
        {
            Navigator = Dependency.Resolve<INavigationService>();
            Notificator = Dependency.Resolve<INotificationService>();
            Dispatcher = Dependency.Resolve<IDispatcher>();
            Progress = Dependency.Resolve<IProgressIndicator>();
            Configuration = Dependency.Resolve<IConfigurationManager>();
            Messager = Dependency.Resolve<IMessager>();
        }

        public ViewModelBase(INavigationService navigationService,
            INotificationService notificationService, IDispatcher dispatcher, IProgressIndicator progress,
            IConfigurationManager configuration, IMessager messager)
        {
            Navigator = navigationService;
            Notificator = notificationService;
            Dispatcher = dispatcher;
            Progress = progress;
            Configuration = configuration;
            Messager = messager;
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            if (Dispatcher.IsUIThread)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            else
            {
                Dispatcher.BeginInvoke(() => RaisePropertyChanged(propertyName));
            }
        }

        public virtual void OnLoad()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnNavigate()
        {
        }

        protected virtual T ReceiveMessage<T>() where T : class
        {
            return Messager.Receive<T>(this.GetType());
        }

        public virtual void OnNavigating(CancelEventArgs e)
        {

        }
    }
}
