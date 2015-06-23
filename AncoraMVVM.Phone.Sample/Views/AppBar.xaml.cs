using AncoraMVVM.Base.ViewModelLocator;
using AncoraMVVM.Phone.Sample.ViewModels;
using Microsoft.Phone.Controls;

namespace AncoraMVVM.Phone.Sample.Views
{
    [ViewModel(typeof(AppBarModel))]
    public partial class AppBar : PhoneApplicationPage
    {
        public AppBar()
        {
            InitializeComponent();
        }
    }
}