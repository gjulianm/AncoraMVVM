using Microsoft.Phone.Controls;
using System;
using System.Windows;

// Source: https://bindableapplicationb.codeplex.com/

namespace AncoraMVVM.Phone7.BindableAppBar
{
    /// <summary>
    /// Provides an attached dependency property
    /// <see cref="ApplicationBarProperty"/> that can be used
    /// to set a <see cref="BindableAppBar"/>
    /// on a <see cref="PhoneApplicationPage"/> object.
    /// </summary>
    public static class Bindable
    {
        #region ApplicationBar
        /// <summary>
        /// ApplicationBar Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty ApplicationBarProperty =
            DependencyProperty.RegisterAttached(
                "ApplicationBar",
                typeof(BindableAppBar),
                typeof(Bindable),
                new PropertyMetadata(null, OnApplicationBarChanged));

        /// <summary>
        /// Gets the ApplicationBar property. This dependency property 
        /// indicates the BindableApplicationBar instance associated
        /// with a page.
        /// </summary>
        /// <param name="d">
        /// The dependency object.
        /// </param>
        /// <returns>
        /// Gets the application bar.
        /// </returns>
        public static BindableAppBar GetApplicationBar(
            DependencyObject d)
        {
            return
                (BindableAppBar)d.GetValue(ApplicationBarProperty);
        }

        /// <summary>
        /// Sets the ApplicationBar property. This dependency property
        /// indicates the BindableApplicationBar instance associated with
        /// a page.
        /// </summary>
        /// <param name="d">The dependency object.</param>
        /// <param name="value">The new value.</param>
        public static void SetApplicationBar(
            DependencyObject d, BindableAppBar value)
        {
            d.SetValue(ApplicationBarProperty, value);
        }

        /// <summary>
        /// Handles changes to the ApplicationBar property.
        /// </summary>
        /// <param name="d">
        /// The <see cref="DependencyObject"/> on which
        /// the property has changed value.
        /// </param>
        /// <param name="e">
        /// Event data that is issued by any event that
        /// tracks changes to the effective value of this property.
        /// </param>
        private static void OnApplicationBarChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var page = d as PhoneApplicationPage;

            if (page == null)
            {
                throw new InvalidOperationException(
                    "Bindable.ApplicationBar property needs to be set on a PhoneApplicationPage element.");
            }

            var oldApplicationBar = (BindableAppBar)e.OldValue;
            var newApplicationBar =
                (BindableAppBar)d.GetValue(ApplicationBarProperty);

            if (oldApplicationBar != newApplicationBar)
            {
                if (oldApplicationBar != null)
                {
                    oldApplicationBar.Detach(page);
                }

                if (newApplicationBar != null)
                {
                    newApplicationBar.Attach(page);
                }
            }
        }
        #endregion
    }
}