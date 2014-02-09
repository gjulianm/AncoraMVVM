
using AncoraMVVM.Base;
using AncoraMVVM.Base.Interfaces;
using AncoraMVVM.Base.IoC;
using Microsoft.Phone.Tasks;
using System;
using System.Windows;
namespace AncoraMVVM.Phone7.BindableAppBar
{
    public class BindableAppBarHyperlinkButton : BindableAppBarButton
    {
        #region Uri
        /// <summary>
        /// Uri Dependency Property
        /// </summary>
        public static readonly DependencyProperty UriProperty =
            DependencyProperty.Register(
                "Uri",
                typeof(string),
                typeof(BindableAppBarHyperlinkButton),
                new PropertyMetadata(null, OnUriChanged));

        /// <summary>
        /// Gets or sets the Uri property. This dependency property 
        /// indicates the Uri bound to the associated
        /// ApplicationBarMenuItem's Uri property.
        /// </summary>
        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        /// <summary>
        /// Handles changes to the Uri property.
        /// </summary>
        /// <param name="d">
        /// The <see cref="DependencyObject"/> on which
        /// the property has changed value.
        /// </param>
        /// <param name="e">
        /// Event data that is issued by any event that
        /// tracks changes to the effective value of this property.
        /// </param>
        private static void OnUriChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (BindableAppBarHyperlinkButton)d;
            string oldUri = (string)e.OldValue;
            string newUri = target.Uri;
            target.OnUriChanged(oldUri, newUri);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the
        /// Uri property.
        /// </summary>
        /// <param name="oldUri">The old Uri.</param>
        /// <param name="newUri">The new Uri.</param>
        protected virtual void OnUriChanged(string oldUri, string newUri)
        {
        }
        #endregion

        protected virtual void Navigate()
        {
            if (string.IsNullOrWhiteSpace(Uri))
                return;
            else if (Uri.StartsWith("/"))
                Dependency.Resolve<INavigationService>().Navigate(Uri);
            else if (System.Uri.IsWellFormedUriString(Uri, UriKind.Absolute))
                new WebBrowserTask() { Uri = new Uri(Uri, UriKind.Absolute) }.Show();
        }

        public BindableAppBarHyperlinkButton()
        {
            Command = new DelegateCommand(Navigate);
        }
    }
}
