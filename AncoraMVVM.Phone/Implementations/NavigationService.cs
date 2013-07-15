using AncoraMVVM.Base.Interfaces;
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
        private PhoneApplicationFrame Frame
        {
            get
            {
                return Application.Current.RootVisual as PhoneApplicationFrame;
            }
        }

        public void Navigate(string page)
        {
            Navigate(new Uri(page, UriKind.Relative));
        }

        public void Navigate(Uri page)
        {
            Frame.Navigate(page);
        }

        public void GoBack()
        {
            Frame.GoBack();
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
