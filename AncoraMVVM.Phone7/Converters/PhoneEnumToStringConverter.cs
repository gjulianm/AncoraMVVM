using AncoraMVVM.Base.Converters;
using System.Windows.Data;

namespace AncoraMVVM.Phone7.Converters
{
    public class PhoneEnumToStringConverter : BaseEnumToStringConverter, IValueConverter
    {
        protected override string GetLocalizedString(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
