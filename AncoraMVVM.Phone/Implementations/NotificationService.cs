﻿using AncoraMVVM.Base;
using AncoraMVVM.Base.Interfaces;
using AncoraMVVM.Base.IoC;
using System.Windows;

namespace AncoraMVVM.Phone.Implementations
{
    public class NotificationService : INotificationService
    {
        private IProgressIndicator Indicator
        {
            get
            {
                return Dependency.Resolve<IProgressIndicator>();
            }
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void ShowWarning(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButton.OK);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK);
        }

        public void ShowProgressIndicatorMessage(string message)
        {
            
        }
    }
}