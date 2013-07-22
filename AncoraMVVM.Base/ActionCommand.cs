using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace AncoraMVVM.Base
{
    public class DelegateCommand : ICommand
    {
        private Action<object> executeAction;
        private Func<object, bool> canExecuteAction;

        public DelegateCommand(Action<object> execute)
        {
            executeAction = execute;
            canExecuteAction = p => true;
        }

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            executeAction = execute;
            canExecuteAction = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecuteAction(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            executeAction(parameter);
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
}
