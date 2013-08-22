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

        public ViewModelBase()
        {
            Navigator = Dependency.Resolve<INavigationService>();
            Notificator = Dependency.Resolve<INotificationService>();
            Dispatcher = Dependency.Resolve<IDispatcher>();
            Progress = Dependency.Resolve<IProgressIndicator>();
            Configuration = Dependency.Resolve<IConfigurationManager>();
        }

        public ViewModelBase(INavigationService navigationService, INotificationService notificationService, IDispatcher dispatcher, IProgressIndicator progress, IConfigurationManager configuration)
        {
            Navigator = navigationService;
            Notificator = notificationService;
            Dispatcher = dispatcher;
            Progress = progress;
            Configuration = configuration;
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
    }
}
