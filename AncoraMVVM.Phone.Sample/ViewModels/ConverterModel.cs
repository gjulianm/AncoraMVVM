using AncoraMVVM.Base;
using PropertyChanged;

namespace AncoraMVVM.Phone.Sample.ViewModels
{
    [ImplementPropertyChanged]
    public class ConverterModel : ViewModelBase
    {
        public bool Toggled { get; set; }
    }
}
