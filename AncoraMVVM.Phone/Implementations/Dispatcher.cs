﻿using System;
using System.Windows;
using AncoraMVVM.Base.Interfaces;

namespace AncoraMVVM.Phone.Implementations
{
    public class Dispatcher : BaseDispatcher
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

        public override void BeginInvoke(Action action)
        {
            PhoneDispatcher.BeginInvoke(action);
        }

        public override bool IsUIThread
        {
            get
            {
                return PhoneDispatcher.CheckAccess();
            }
        }
    }
}
