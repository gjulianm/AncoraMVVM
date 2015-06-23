using System.Windows.Data;
using AncoraMVVM.Base.Converters;

namespace AncoraMVVM.Phone.Converters
{
    public class PhoneEnumToStringConverter : BaseEnumToStringConverter, IValueConverter
    {
        protected override string GetLocalizedString(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
