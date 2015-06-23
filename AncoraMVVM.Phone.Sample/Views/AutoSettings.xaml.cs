using AncoraMVVM.Base.ViewModelLocator;
using AncoraMVVM.Phone.Sample.ViewModels;
using Microsoft.Phone.Controls;

namespace AncoraMVVM.Phone.Sample.Views
{
    [ViewModel(typeof(AutoSettingsModel))]
    public partial class AutoSettings : PhoneApplicationPage
    {
        public AutoSettings()
        {
            InitializeComponent();
        }
    }
}