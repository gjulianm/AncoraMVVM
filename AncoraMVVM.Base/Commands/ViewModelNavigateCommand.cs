using AncoraMVVM.Base.Interfaces;
using AncoraMVVM.Base.IoC;
using System;
using System.Windows.Input;

namespace AncoraMVVM.Base
{
    /// <summary>
    /// Similar to NavigateCommand, but in this case the type of the ViewModel is the target.
    /// </summary>
    /// <typeparam name="T">ViewModelBase subclass to navigate to.</typeparam>
    public class ViewModelNavigateCommand<T> : ICommand where T : ViewModelBase
    {
        /// <summary>
        /// Target to navigate to. The type is a subclass of ViewModelBase.
        /// </summary>
        public Type Target { get; set; }

        public ViewModelNavigateCommand()
        {
            Target = typeof(T);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

#pragma warning disable 67
        public event EventHandler CanExecuteChanged;
#pragma warning restore 67

        public void Execute(object parameter)
        {
            Dependency.Resolve<INavigationService>().Navigate(Target);
        }
    }
}
