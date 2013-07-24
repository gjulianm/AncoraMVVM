using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;



namespace AncoraMVVM.Base
{
#pragma warning disable 1998
    public class DelegateCommand : ICommand
    {
        private Func<object, Task> executeAction;
        private Func<object, bool> canExecuteAction;

        public DelegateCommand(Action<object> execute)
        {
            executeAction = async (p) => execute(p);
            canExecuteAction = p => true;
        }

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            executeAction = async (p) => execute(p);
            canExecuteAction = canExecute;
        }

        public DelegateCommand(Func<object, Task> execute)
        {
            executeAction = execute;
            canExecuteAction = p => true;
        }

        public DelegateCommand(Func<object, Task> execute, Func<object, bool> canExecute)
        {
            executeAction = execute;
            canExecuteAction = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecuteAction(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public async void Execute(object parameter)
        {
            await executeAction(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }

        public void BindCanExecuteToProperty(INotifyPropertyChanged element, params string[] propertyNames)
        {
            element.PropertyChanged += (sender, e) =>
            {
                if (propertyNames.Contains(e.PropertyName))
                    RaiseCanExecuteChanged();
            };
        }
    }
#pragma warning restore 1998
}
