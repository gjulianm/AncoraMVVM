using System.ComponentModel;

namespace AncoraMVVM.Base
{
    public class ObservableObject : INotifyPropertyChanged
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
