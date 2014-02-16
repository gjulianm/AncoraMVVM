using AncoraMVVM.Base;
using PropertyChanged;

namespace AncoraMVVM.Phone7.Sample.ViewModels
{
    [ImplementPropertyChanged]
    public class ConverterModel : ViewModelBase
    {
        public bool Toggled { get; set; }
    }
}
