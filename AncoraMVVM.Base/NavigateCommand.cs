using AncoraMVVM.Base.Interfaces;
using AncoraMVVM.Base.IoC;
using System;
using System.Windows.Input;

namespace AncoraMVVM.Base
{
    public class NavigateCommand : ICommand
    {
        public string Target { get; set; }
        public NavigateCommand(string page)
        {
            Target = page;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            Dependency.Resolve<INavigationService>().Navigate(Target);
        }
    }
}
