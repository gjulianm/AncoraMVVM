using AncoraMVVM.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AncoraMVVM.Phone.Implementations
{
    public class Dispatcher : IDispatcher
    {
        public void BeginInvoke(Action action)
        {
            Deployment.Current.Dispatcher.BeginInvoke(action);
        }

        public bool IsUIThread
        {
            get
            {
                return Deployment.Current.Dispatcher.CheckAccess();
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
