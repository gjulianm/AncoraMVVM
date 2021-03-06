﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;
using AncoraMVVM.Base.Interfaces;
using AncoraMVVM.Base.IoC;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace AncoraMVVM.Phone.Implementations
{
    // Source: http://www.jeff.wilcox.name/2011/07/creating-a-global-progressindicator-experience-using-the-windows-phone-7-1-sdk-beta-2/
    public class GlobalProgress : IProgressIndicator
    {
        private ProgressIndicator indicator;
        private IDispatcher dispatcher;

        public GlobalProgress()
        {
            dispatcher = Dependency.Resolve<IDispatcher>();
            var frame = Application.Current.RootVisual as PhoneApplicationFrame;

            if (frame != null)
                Initialize(frame);

        }

        public void Initialize(PhoneApplicationFrame frame)
        {
            indicator = new ProgressIndicator();

            frame.Navigated += OnRootFrameNavigated;
        }

        private void OnRootFrameNavigated(object sender, NavigationEventArgs e)
        {
            var ee = e.Content;
            var pp = ee as PhoneApplicationPage;

            if (pp != null)
                pp.SetValue(SystemTray.ProgressIndicatorProperty, indicator);
        }

        private int loadingCount;
        public bool IsLoading
        {
            get
            {
                return loadingCount > 0;
            }
            set
            {
                if (value)
                    ++loadingCount;
                else
                    --loadingCount;

                if (loadingCount < 0)
                    loadingCount = 0;

                dispatcher.InvokeIfRequired(NotifyValueChanged);
            }
        }

        private void NotifyValueChanged()
        {
            if (indicator != null)
            {
                indicator.IsIndeterminate = loadingCount > 0;

                // for now, just make sure it's always visible.
                if (indicator.IsVisible == false)
                    indicator.IsVisible = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public string Text
        {
            get
            {
                return indicator.Text;
            }
            set
            {
                dispatcher.InvokeIfRequired(() => indicator.Text = value);
            }
        }


        public void ClearIndicator()
        {
            loadingCount = 0;
            Text = string.Empty;
            NotifyValueChanged();
        }
    }
}
