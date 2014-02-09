using System.Windows;
using System.Windows.Data;

namespace AncoraMVVM.Phone7.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var trueIsVisible = parameter as string != "Collapsed";

            if (value is bool && (bool)value && trueIsVisible)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
