using AncoraMVVM.Base.ViewModelLocator;
using AncoraMVVM.Phone.Sample.ViewModels;
using Microsoft.Phone.Controls;

namespace AncoraMVVM.Phone.Sample.Views
{
    [ViewModel(typeof(ProgressModel))]
    public partial class Progress : PhoneApplicationPage
    {
        public Progress()
        {
            InitializeComponent();
        }
    }
}