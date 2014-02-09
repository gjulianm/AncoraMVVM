
using System;
using System.Windows;
using System.Windows.Input;
namespace AncoraMVVM.Phone7.BindableAppBar
{
    public class BindableAppBarToggleMenuItem : BindableAppBarMenuItem
    {
        #region Text1
        /// <summary>
        /// Text1 Dependency Property
        /// </summary>
        public static readonly DependencyProperty Text1Property =
            DependencyProperty.Register(
                "Text1",
                typeof(string),
                typeof(BindableAppBarToggleMenuItem),
                new PropertyMetadata(null, OnText1Changed));

        /// <summary>
        /// Gets or sets the Text1 property. This dependency property 
        /// indicates the Text1 bound to the associated
        /// ApplicationBarIconButton's Text1 property.
        /// </summary>
        public string Text1
        {
            get { return (string)GetValue(Text1Property); }
            set { SetValue(Text1Property, value); }
        }

        /// <summary>
        /// Handles changes to the Text1 property.
        /// </summary>
        /// <param name="d">
        /// The <see cref="DependencyObject"/> on which
        /// the property has changed value.
        /// </param>
        /// <param name="e">
        /// Event data that is issued by any event that
        /// tracks changes to the effective value of this property.
        /// </param>
        private static void OnText1Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (BindableAppBarToggleMenuItem)d;
            string oldText = (string)e.OldValue;
            string newText = target.Text1;
            target.OnText1Changed(oldText, newText);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the
        /// Text1 property.
        /// </summary>
        /// <param name="oldText">The old Text value.</param>
        /// <param name="newText">The new Text value.</param>
        protected virtual void OnText1Changed(string oldText, string newText)
        {
            if (!Toggled)
                Text = Text1;
        }
        #endregion

        #region Command1
        /// <summary>
        /// Command11 Dependency Property
        /// </summary>
        public static readonly DependencyProperty Command1Property =
            DependencyProperty.Register(
                "Command1",
                typeof(ICommand),
                typeof(BindableAppBarToggleMenuItem),
                new PropertyMetadata(null, OnCommand1Changed));

        /// <summary>
        /// Gets or sets the Command1 property. This dependency property 
        /// indicates the Command1 to execute when the button gets clicked.
        /// </summary>
        public ICommand Command1
        {
            get { return (ICommand)GetValue(Command1Property); }
            set { SetValue(Command1Property, value); }
        }

        /// <summary>
        /// Handles changes to the Command1 property.
        /// </summary>
        /// <param name="d">
        /// The <see cref="DependencyObject"/> on which
        /// the property has changed value.
        /// </param>
        /// <param name="e">
        /// Event data that is issued by any event that
        /// tracks changes to the effective value of this property.
        /// </param>
        private static void OnCommand1Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (BindableAppBarToggleMenuItem)d;
            ICommand oldCommand1 = (ICommand)e.OldValue;
            ICommand newCommand1 = target.Command1;
            target.OnCommand1Changed(oldCommand1, newCommand1);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to
        /// the Command1 property.
        /// </summary>
        /// <param name="oldCommand1">The old Command1 value.</param>
        /// <param name="newCommand1">The new Command1 value.</param>
        protected virtual void OnCommand1Changed(
            ICommand oldCommand1, ICommand newCommand1)
        {
            if (oldCommand1 != null)
            {
                oldCommand1.CanExecuteChanged -=
                    this.Command1CanExecuteChanged;
            }

            if (newCommand1 != null)
            {
                if (!Toggled)
                {
                    this.IsEnabled =
                        newCommand1.CanExecute(this.CommandParameter1);
                }

                newCommand1.CanExecuteChanged +=
                    this.Command1CanExecuteChanged;
            }
        }

        private void Command1CanExecuteChanged(object sender, EventArgs e)
        {
            if (!Toggled)
            {
                this.IsEnabled =
                    this.Command1.CanExecute(this.CommandParameter1);
            }
        }
        #endregion

        #region CommandParameter1
        /// <summary>
        /// CommandParameter1 Dependency Property
        /// </summary>
        public static readonly DependencyProperty CommandParameter1Property =
            DependencyProperty.Register(
                "CommandParameter1",
                typeof(object),
                typeof(BindableAppBarToggleMenuItem),
                new PropertyMetadata(null, OnCommandParameter1Changed));

        /// <summary>
        /// Gets or sets the CommandParameter1 property.
        /// This dependency property indicates the parameter to be passed
        /// to the Command when the button gets pressed.
        /// </summary>
        public object CommandParameter1
        {
            get { return GetValue(CommandParameter1Property); }
            set { SetValue(CommandParameter1Property, value); }
        }

        /// <summary>
        /// Handles changes to the CommandParameter1 property.
        /// </summary>
        /// <param name="d">
        /// The <see cref="DependencyObject"/> on which
        /// the property has changed value.
        /// </param>
        /// <param name="e">
        /// Event data that is issued by any event that
        /// tracks changes to the effective value of this property.
        /// </param>
        private static void OnCommandParameter1Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (BindableAppBarToggleMenuItem)d;
            object oldCommandParameter1 = e.OldValue;
            object newCommandParameter1 = target.CommandParameter1;
            target.OnCommandParameter1Changed(
                oldCommandParameter1, newCommandParameter1);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to
        /// the CommandParameter1 property.
        /// </summary>
        /// <param name="oldCommandParameter1">
        /// The old CommandParameter1 value.
        /// </param>
        /// <param name="newCommandParameter1">
        /// The new CommandParameter1 value.
        /// </param>
        protected virtual void OnCommandParameter1Changed(
            object oldCommandParameter1, object newCommandParameter1)
        {
            if (this.Command != null && !Toggled)
            {
                this.IsEnabled =
                    this.Command.CanExecute(this.CommandParameter1);
            }
        }
        #endregion

        #region Text2
        /// <summary>
        /// Text2 Dependency Property
        /// </summary>
        public static readonly DependencyProperty Text2Property =
            DependencyProperty.Register(
                "Text2",
                typeof(string),
                typeof(BindableAppBarToggleMenuItem),
                new PropertyMetadata(null, OnText2Changed));

        /// <summary>
        /// Gets or sets the Text2 property. This dependency property 
        /// indicates the Text2 bound to the associated
        /// ApplicationBarIconButton's Text2 property.
        /// </summary>
        public string Text2
        {
            get { return (string)GetValue(Text2Property); }
            set { SetValue(Text2Property, value); }
        }

        /// <summary>
        /// Handles changes to the Text2 property.
        /// </summary>
        /// <param name="d">
        /// The <see cref="DependencyObject"/> on which
        /// the property has changed value.
        /// </param>
        /// <param name="e">
        /// Event data that is issued by any event that
        /// tracks changes to the effective value of this property.
        /// </param>
        private static void OnText2Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (BindableAppBarToggleMenuItem)d;
            string oldText = (string)e.OldValue;
            string newText = target.Text2;
            target.OnText2Changed(oldText, newText);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the
        /// Text2 property.
        /// </summary>
        /// <param name="oldText">The old Text value.</param>
        /// <param name="newText">The new Text value.</param>
        protected virtual void OnText2Changed(string oldText, string newText)
        {
            if (Toggled)
                Text = Text2;
        }
        #endregion

        #region Command2
        /// <summary>
        /// Command22 Dependency Property
        /// </summary>
        public static readonly DependencyProperty Command2Property =
            DependencyProperty.Register(
                "Command2",
                typeof(ICommand),
                typeof(BindableAppBarToggleMenuItem),
                new PropertyMetadata(null, OnCommand2Changed));

        /// <summary>
        /// Gets or sets the Command2 property. This dependency property 
        /// indicates the Command2 to execute when the button gets clicked.
        /// </summary>
        public ICommand Command2
        {
            get { return (ICommand)GetValue(Command2Property); }
            set { SetValue(Command2Property, value); }
        }

        /// <summary>
        /// Handles changes to the Command2 property.
        /// </summary>
        /// <param name="d">
        /// The <see cref="DependencyObject"/> on which
        /// the property has changed value.
        /// </param>
        /// <param name="e">
        /// Event data that is issued by any event that
        /// tracks changes to the effective value of this property.
        /// </param>
        private static void OnCommand2Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (BindableAppBarToggleMenuItem)d;
            ICommand oldCommand2 = (ICommand)e.OldValue;
            ICommand newCommand2 = target.Command2;
            target.OnCommand2Changed(oldCommand2, newCommand2);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to
        /// the Command2 property.
        /// </summary>
        /// <param name="oldCommand2">The old Command2 value.</param>
        /// <param name="newCommand2">The new Command2 value.</param>
        protected virtual void OnCommand2Changed(
            ICommand oldCommand2, ICommand newCommand2)
        {
            if (oldCommand2 != null)
            {
                oldCommand2.CanExecuteChanged -=
                    this.Command2CanExecuteChanged;
            }

            if (newCommand2 != null)
            {
                if (Toggled)
                {
                    this.IsEnabled =
                        newCommand2.CanExecute(this.CommandParameter2);
                }

                newCommand2.CanExecuteChanged +=
                    this.Command2CanExecuteChanged;
            }
        }

        private void Command2CanExecuteChanged(object sender, EventArgs e)
        {
            if (Toggled)
            {
                this.IsEnabled =
                    this.Command2.CanExecute(this.CommandParameter2);
            }
        }
        #endregion

        #region CommandParameter2
        /// <summary>
        /// CommandParameter2 Dependency Property
        /// </summary>
        public static readonly DependencyProperty CommandParameter2Property =
            DependencyProperty.Register(
                "CommandParameter2",
                typeof(object),
                typeof(BindableAppBarToggleMenuItem),
                new PropertyMetadata(null, OnCommandParameter2Changed));

        /// <summary>
        /// Gets or sets the CommandParameter2 property.
        /// This dependency property indicates the parameter to be passed
        /// to the Command when the button gets pressed.
        /// </summary>
        public object CommandParameter2
        {
            get { return GetValue(CommandParameter2Property); }
            set { SetValue(CommandParameter2Property, value); }
        }

        /// <summary>
        /// Handles changes to the CommandParameter2 property.
        /// </summary>
        /// <param name="d">
        /// The <see cref="DependencyObject"/> on which
        /// the property has changed value.
        /// </param>
        /// <param name="e">
        /// Event data that is issued by any event that
        /// tracks changes to the effective value of this property.
        /// </param>
        private static void OnCommandParameter2Changed(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (BindableAppBarToggleMenuItem)d;
            object oldCommandParameter2 = e.OldValue;
            object newCommandParameter2 = target.CommandParameter2;
            target.OnCommandParameter2Changed(
                oldCommandParameter2, newCommandParameter2);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to
        /// the CommandParameter2 property.
        /// </summary>
        /// <param name="oldCommandParameter2">
        /// The old CommandParameter2 value.
        /// </param>
        /// <param name="newCommandParameter2">
        /// The new CommandParameter2 value.
        /// </param>
        protected virtual void OnCommandParameter2Changed(
            object oldCommandParameter2, object newCommandParameter2)
        {
            if (this.Command != null && Toggled)
            {
                this.IsEnabled =
                    this.Command.CanExecute(this.CommandParameter2);
            }
        }
        #endregion

        #region Toggled
        /// <summary>
        /// Toggled Dependency Property
        /// </summary>
        public static readonly DependencyProperty ToggledProperty =
            DependencyProperty.Register(
                "Toggled",
                typeof(bool),
                typeof(BindableAppBarToggleMenuItem),
                new PropertyMetadata(false, OnToggledChanged));

        /// <summary>
        /// Gets or sets the Toggled property. This dependency property 
        /// indicates the Toggled to execute when the button gets clicked.
        /// </summary>
        public bool Toggled
        {
            get { return (bool)GetValue(ToggledProperty); }
            set { SetValue(ToggledProperty, value); }
        }

        /// <summary>
        /// Handles changes to the Toggled property.
        /// </summary>
        /// <param name="d">
        /// The <see cref="DependencyObject"/> on which
        /// the property has changed value.
        /// </param>
        /// <param name="e">
        /// Event data that is issued by any event that
        /// tracks changes to the effective value of this property.
        /// </param>
        private static void OnToggledChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (BindableAppBarToggleMenuItem)d;
            bool oldToggled = (bool)e.OldValue;
            bool newToggled = target.Toggled;
            target.OnToggledChanged(oldToggled, newToggled);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to
        /// the Toggled property.
        /// </summary>
        /// <param name="oldToggled">The old Toggled value.</param>
        /// <param name="newToggled">The new Toggled value.</param>
        protected virtual void OnToggledChanged(
            bool oldToggled, bool newToggled)
        {
            if (oldToggled != newToggled)
            {
                if (newToggled)
                {
                    Text = Text2;
                    Command = Command2;
                    CommandParameter = CommandParameter2;
                }
                else
                {
                    Text = Text1;
                    Command = Command1;
                    CommandParameter = CommandParameter1;
                }
            }
        }
        #endregion
    }
}
