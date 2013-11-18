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

        private void AssertFrameNotNull()
        {
            if (Frame == null)
                throw new InvalidOperationException("AncoraMVVM: the current RootVisual is null or not a PhoneApplicationFrame. Can't execute navigation operations, abort.");
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
            dispatcher.InvokeIfRequired(() => { AssertFrameNotNull(); Frame.Navigate(page); });
        }

        public override void GoBack()
        {
            dispatcher.InvokeIfRequired(() => { AssertFrameNotNull(); Frame.GoBack(); });
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
            dispatcher.InvokeIfRequired(() =>
            {
                AssertFrameNotNull();
                while (Frame.RemoveBackEntry() != null) ;
            });
        }

        public override void ClearLastStackEntries(int count)
        {
            dispatcher.InvokeIfRequired(() =>
            {
                AssertFrameNotNull();
                while (count > 0 && Frame.RemoveBackEntry() != null)
                    count--;
            });
        }
    }
}
