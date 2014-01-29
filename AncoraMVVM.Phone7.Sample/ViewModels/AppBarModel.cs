using AncoraMVVM.Base;
using AncoraMVVM.Phone7.BindableAppBar;
using PropertyChanged;
using System;

namespace AncoraMVVM.Phone7.Sample.ViewModels
{
    [ImplementPropertyChanged]
    public class AppBarModel : ViewModelBase
    {
        public string NewItemText { get; set; }
        public DelegateCommand AddMenuItem { get; set; }
        public DelegateCommand AddButton { get; set; }
        public bool Toggled { get; set; }
        public SafeObservable<BindableAppBarMenuItem> MenuItems { get; set; }
        public SafeObservable<BindableAppBarButton> Buttons { get; set; }
        public double Opacity { get; set; }

        public AppBarModel()
        {
            MenuItems = new SafeObservable<BindableAppBarMenuItem>();
            Buttons = new SafeObservable<BindableAppBarButton>();

            AddButton = new DelegateCommand(() => Buttons.Add(new BindableAppBarButton { Text = NewItemText, IconUri = new Uri("/Toolkit.Content/ApplicationBar.Select.png", UriKind.Relative) }));
            AddMenuItem = new DelegateCommand(() => MenuItems.Add(new BindableAppBarMenuItem { Text = NewItemText }));

            var button = new BindableAppBarToggleButton
            {
                Text1 = "not toggled",
                Text2 = "toggled",
                IconUri1 = new Uri("/Toolkit.Content/ApplicationBar.Add.png", UriKind.Relative),
                IconUri2 = new Uri("/Toolkit.Content/ApplicationBar.Delete.png", UriKind.Relative)
            };

            Buttons.Add(button);

            this.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "Toggled")
                    button.Toggled = Toggled;
            };

            Opacity = 0.5;
        }
    }
}
