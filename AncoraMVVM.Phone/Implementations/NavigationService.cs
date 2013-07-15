using AncoraMVVM.Base.Interfaces;
using AncoraMVVM.Base.IoC;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AncoraMVVM.Phone.Implementations
{
    public class NavigationService : INavigationService
    {
        private IDispatcher dispatcher;
        private PhoneApplicationFrame Frame
        {
            get
            {
                return Application.Current.RootVisual as PhoneApplicationFrame;
            }
        }

        public NavigationService()
        {
            dispatcher = Dependency.Resolve<IDispatcher>();
        }

        public void Navigate(string page)
        {
            Navigate(new Uri(page, UriKind.Relative));
        }

        public void Navigate(Uri page)
        {
            dispatcher.InvokeIfRequired(() => Frame.Navigate(page));
        }

        public void GoBack()
        {
            dispatcher.InvokeIfRequired(Frame.GoBack);
        }

        public bool CanGoBack
        {
            get
            {
                return Frame.CanGoBack;
            }
        }
    }
}
