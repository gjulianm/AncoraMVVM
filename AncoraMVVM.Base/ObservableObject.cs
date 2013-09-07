using System.ComponentModel;

namespace AncoraMVVM.Base
{
    /// <summary>
    /// A base class to implement easily INotifyPropertyChanged and the RaisePropertyChanged method.
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            var evHandler = PropertyChanged;
            if (evHandler != null)
                evHandler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
