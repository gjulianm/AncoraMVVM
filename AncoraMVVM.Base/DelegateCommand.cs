using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;



namespace AncoraMVVM.Base
{
#pragma warning disable 1998
    public class DelegateCommand : ICommand
    {
        private Action<object> executeAction;
        private Func<object, bool> canExecuteAction;

        public DelegateCommand(Action<object> execute)
            : this(execute, p => true)
        {
        }

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            executeAction = execute;
            canExecuteAction = canExecute;
        }

        public DelegateCommand(Action execute)
            : this(p => execute(), p => true)
        {
        }

        public DelegateCommand(Action execute, Func<bool> canExecute)
            : this(p => execute(), p => canExecute())
        {
        }

        public bool CanExecute(object parameter)
        {
            return canExecuteAction(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public async void Execute(object parameter)
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
#pragma warning restore 1998
}
