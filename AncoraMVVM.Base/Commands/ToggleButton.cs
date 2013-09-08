using System.Windows.Input;

namespace AncoraMVVM.Base
{
    /// <summary>
    /// A class that represents a button that can be toggled between two states.
    /// 
    /// Each state is set in the corresponding IconX/CommandX/TextX properties. The properties
    /// Icon/Command/Text change automatically when Toggled changes. If Toggled == true, the 
    /// properties ending with 1 are selected, else the selected ones are the ones ending with 2.
    /// </summary>
    public class ToggleButton : ObservableObject
    {
        public string Icon1 { get; set; }
        public string Icon2 { get; set; }
        public ICommand Command1 { get; set; }
        public ICommand Command2 { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }

        bool toggled;

        /// <summary>
        /// Returns whether the button is toggled or not.
        /// 
        /// If Toggled == true, the properties ending with 1 are selected, else the selected ones are the ones ending with 2.
        /// </summary>
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
    }
}
