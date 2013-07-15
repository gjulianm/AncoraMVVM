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
        public ViewModelBase(INavigationService navigationService, INotificationService notificationService, IDispatcher dispatcher)
        {
        }
    }
}
