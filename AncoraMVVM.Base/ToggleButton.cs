using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AncoraMVVM.Base
{
    public class ToggleButton : INotifyPropertyChanged
    {
        public string Icon1 { get; set; }
        public string Icon2 { get; set; }
        public ICommand Command1 { get; set; }
        public ICommand Command2 { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }

        bool toggled;
        public bool Toggled
        {
            get
            {
                return toggled;
            }
            set
            {
                if (toggled != value)
                {
                    toggled = value;
                    RaisePropertyChanged("Toggled");
                    RaisePropertyChanged("Icon");
                    RaisePropertyChanged("Command");
                    RaisePropertyChanged("Text");
                }
            }
        }

        public string Icon
        {
            get { return Toggled ? Icon1 : Icon2; }
        }

        public ICommand Command
        {
            get { return Toggled ? Command1 : Command2; }
        }

        public string Text
        {
            get { return Toggled ? Text1 : Text2; }
        }

        #region INotifyPropertyChanged
        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
