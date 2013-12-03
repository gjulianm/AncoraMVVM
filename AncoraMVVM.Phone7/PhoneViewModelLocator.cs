using AncoraMVVM.Base;
using AncoraMVVM.Base.ViewModelLocator;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Navigation;

namespace AncoraMVVM.Phone
{
    public class PhoneViewModelLocator : ViewModelLocator
    {
        private PhoneApplicationFrame RootFrame { get; set; }

        private void Initialize(PhoneApplicationFrame rootFrame)
        {
            rootFrame.Navigated += OnRootFrameNavigated;
            rootFrame.Navigating += OnRootFrameNavigating;

            RootFrame = rootFrame;
        }


        public void InitializeAndFindPages(PhoneApplicationFrame rootFrame)
        {
            Initialize(rootFrame);

            RegisterPagesAndViewModels(Assembly.GetCallingAssembly());
        }

        public void InitializeWithGivenMap(PhoneApplicationFrame rootFrame, IDictionary<Type, Type> pageToViewModelMap)
        {
            Initialize(rootFrame);

            foreach (var pair in pageToViewModelMap)
                PageToViewModelMap.Add(pair.Key, pair.Value);
        }

        protected virtual void OnRootFrameNavigated(object sender, NavigationEventArgs e)
        {
            var page = e.Content as PhoneApplicationPage;

            if (page != null)
            {
                ViewModelBase viewModel = null;

                if (page.DataContext as ViewModelBase == null)
                {
                    var pageType = page.GetType();

                    if (PageToViewModelMap.ContainsKey(pageType))
                    {
                        viewModel = GetViewModelForType(pageType);
                        page.DataContext = viewModel;

                        RoutedEventHandler handler = null;

                        handler = (s, ea) =>
                        {
                            viewModel.OnLoad();
                            page.Loaded -= handler; // OnLoad is called just once.
                        };

                        page.Loaded += handler;
                    }
                }
                else
                {
                    viewModel = page.DataContext as ViewModelBase;
                }

                if (viewModel != null)
                    viewModel.OnNavigate();

            }
        }


        protected virtual void OnRootFrameNavigating(object sender, NavigatingCancelEventArgs e)
        {
            if (RootFrame != null)
            {
                var page = RootFrame.Content as PhoneApplicationPage;

                if (page != null)
                {
                    var viewModel = page.DataContext as ViewModelBase;

                    if (viewModel != null)
                        viewModel.OnNavigating(e);
                }
            }
        }

        protected override bool IsPageType(Type type)
        {
            return typeof(PhoneApplicationPage).IsAssignableFrom(type);
        }
    }
}
