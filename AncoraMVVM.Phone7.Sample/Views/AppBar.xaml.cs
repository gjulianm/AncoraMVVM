using AncoraMVVM.Base.ViewModelLocator;
using AncoraMVVM.Phone7.Sample.ViewModels;
using Microsoft.Phone.Controls;

namespace AncoraMVVM.Phone7.Sample.Views
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