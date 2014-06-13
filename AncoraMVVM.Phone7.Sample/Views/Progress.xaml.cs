using AncoraMVVM.Base.ViewModelLocator;
using AncoraMVVM.Phone7.Sample.ViewModels;
using Microsoft.Phone.Controls;

namespace AncoraMVVM.Phone7.Sample.Views
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