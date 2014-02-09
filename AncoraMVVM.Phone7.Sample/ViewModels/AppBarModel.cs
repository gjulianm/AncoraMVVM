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

            AddButton = new DelegateCommand(() =>
            {
                if (Buttons.Count < 4)
                    Buttons.Add(new BindableAppBarButton { Text = NewItemText, IconUri = new Uri("/Toolkit.Content/ApplicationBar.Select.png", UriKind.Relative) });
                else
                    Notificator.ShowError("Woops, too many buttons.");
            });

            AddMenuItem = new DelegateCommand(() => MenuItems.Add(new BindableAppBarMenuItem { Text = NewItemText }));

            var button = new BindableAppBarToggleButton
            {
                Text1 = "not toggled",
                Text2 = "toggled",
                IconUri1 = new Uri("/Toolkit.Content/ApplicationBar.Add.png", UriKind.Relative),
                IconUri2 = new Uri("/Toolkit.Content/ApplicationBar.Delete.png", UriKind.Relative)
            };

            var hlButton = new BindableAppBarHyperlinkButton
            {
                Text = "external link",
                IconUri = new Uri("/Toolkit.Content/ApplicationBar.Check.png", UriKind.Relative),
                Uri = "http://github.com/gjulianm/AncoraMVVM"
            };

            var hlIButton = new BindableAppBarHyperlinkButton
            {
                Text = "internal link",
                IconUri = new Uri("/Toolkit.Content/ApplicationBar.Check.png", UriKind.Relative),
                Uri = "/Views/MainPage.xaml"
            };

            Buttons.Add(button);
            Buttons.Add(hlButton);
            Buttons.Add(hlIButton);

            MenuItems.Add(new BindableAppBarHyperlinkMenuItem { Text = "external link", Uri = "http://github.com/gjulianm/AncoraMVVM" });

            this.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "Toggled")
                    button.Toggled = Toggled;
            };

            Opacity = 0.5;
        }
    }
}
