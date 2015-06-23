using AncoraMVVM.Base.ViewModelLocator;
using AncoraMVVM.Phone.Sample.ViewModels;
using Microsoft.Phone.Controls;

namespace AncoraMVVM.Phone.Sample.Views
{
    [ViewModel(typeof(ConverterModel))]
    public partial class Converter : PhoneApplicationPage
    {
        public Converter()
        {
            InitializeComponent();
        }
    }
}