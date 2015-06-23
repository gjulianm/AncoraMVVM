using System;
using AncoraMVVM.Base;
using AncoraMVVM.Base.Interfaces;
using AncoraMVVM.Base.IoC;
using PropertyChanged;

namespace AncoraMVVM.Phone.Sample.ViewModels
{

    public class SampleItem
    {
        public string Title { get; set; }
        public Type Type { get; set; }

        public static SampleItem Create<T>(string title) where T : ViewModelBase
        {
            return new SampleItem
            {
                Title = title,
                Type = typeof(T)
            };
        }

        public void Navigate()
        {
            var nav = Dependency.Resolve<INavigationService>();
            nav.Navigate(Type);
        }
    }

    [ImplementPropertyChanged]
    public class MainPageModel : ViewModelBase
    {
        public SafeObservable<SampleItem> Samples { get; set; }
        public SampleItem SelectedSample { get; set; }

        public MainPageModel()
        {
            Samples = new SafeObservable<SampleItem>();

            this.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "SelectedSample" && SelectedSample != null)
                    SelectedSample.Navigate();
            };

            Samples.Add(SampleItem.Create<AppBarModel>("app bar"));
            Samples.Add(SampleItem.Create<ConverterModel>("converter"));
            Samples.Add(SampleItem.Create<AutoSettingsModel>("automatic settings"));
            Samples.Add(SampleItem.Create<ProgressModel>("progress indicator"));
        }

        public override void OnNavigate()
        {
            SelectedSample = null;
        }
    }
}
