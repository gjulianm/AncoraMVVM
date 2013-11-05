using AncoraMVVM.Base.Interfaces;
using System;
using System.Windows;

namespace AncoraMVVM.Phone.Implementations
{
    public class Dispatcher : IDispatcher
    {
        private System.Windows.Threading.Dispatcher PhoneDispatcher
        {
            get
            {
                return Deployment.Current.Dispatcher;
            }
        }

        private void AssertPhoneDispatcherNotNull()
        {
            if (PhoneDispatcher == null)
                throw new InvalidOperationException("AncoraMVVM: Deployment.Current.Dispatcher is null, can't make dispatcher calls.");
        }

        public void BeginInvoke(Action action)
        {
            PhoneDispatcher.BeginInvoke(action);
        }

        public bool IsUIThread
        {
            get
            {
                return PhoneDispatcher.CheckAccess();
            }
        }

        public void InvokeIfRequired(Action action)
        {
            if (IsUIThread)
                action();
            else
                BeginInvoke(action);
        }
    }
}
