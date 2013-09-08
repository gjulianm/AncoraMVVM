using AncoraMVVM.Base.Interfaces;
using AncoraMVVM.Base.IoC;
using System;
using System.Windows.Input;

namespace AncoraMVVM.Base
{
    /// <summary>
    /// A command which the only purpose to serve as a "navigation button".
    /// </summary>
    public class NavigateCommand : ICommand
    {
        private INavigationService navigator;

        /// <summary>
        /// The target page.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Create a NavigateCommand.
        /// </summary>
        /// <param name="navigator">Navigation service to use.</param>
        /// <param name="page">Target page.</param>
        public NavigateCommand(INavigationService navigator, string page)
        {
            Target = page;
            this.navigator = navigator;
        }

        /// <summary>
        /// Create a NavigateCommand using Dependency to resolve the INavigationService.
        /// </summary>
        /// <param name="page">Target page.</param>
        public NavigateCommand(string page)
            : this(Dependency.Resolve<INavigationService>(), page)
        {
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

#pragma warning disable 67 // Disable "not used" warning. Of course it's not used, but it must be implemented.
        public event EventHandler CanExecuteChanged;
#pragma warning restore 67

        /// <summary>
        /// Navigates to the target.
        /// </summary>
        /// <param name="parameter">Parameter</param>
        public void Execute(object parameter)
        {
            navigator.Navigate(Target);
        }
    }
}
