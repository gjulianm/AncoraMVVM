using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AncoraMVVM;

namespace Goinout.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged 
    {
        protected INavigationService Navigator {get; private set;}
        protected INotificationService Notificator { get; private set; }
        protected IDispatcher Dispatcher { get; private set; }

        public ViewModelBase(INavigationService navigationService, INotificationService notificationService, IDispatcher dispatcher)
        {
            Navigator = navigationService;
            Notificator = notificationService;
            Dispatcher = dispatcher;
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
