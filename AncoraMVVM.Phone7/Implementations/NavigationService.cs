using AncoraMVVM.Base;
using AncoraMVVM.Base.Interfaces;
using AncoraMVVM.Base.IoC;
using Microsoft.Phone.Controls;
using System;
using System.Windows;

namespace AncoraMVVM.Phone.Implementations
{
    public class NavigationService : ViewModelNavigationService
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
            : base()
        {
            dispatcher = Dependency.Resolve<IDispatcher>();
        }

        public override void Navigate(string page)
        {
            Navigate(new Uri(page, UriKind.Relative));
        }

        public override void Navigate(Uri page)
        {
            dispatcher.BeginInvoke(() => Frame.Navigate(page));
        }

        public override void GoBack()
        {
            dispatcher.BeginInvoke(Frame.GoBack);
        }

        public override bool CanGoBack
        {
            get
            {
                return Frame.CanGoBack;
            }
        }

        public override void ClearNavigationStack()
        {
            dispatcher.BeginInvoke(() =>
            {
                while (Frame.RemoveBackEntry() != null) ;
            });
        }

        public override void ClearLastStackEntries(int count)
        {
            dispatcher.BeginInvoke(() =>
            {
                while (Frame.RemoveBackEntry() != null && count > 0)
                    count--;
            });
        }
    }
}
