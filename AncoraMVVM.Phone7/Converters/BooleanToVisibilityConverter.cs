using System.Windows;
using System.Windows.Data;

namespace AncoraMVVM.Phone7.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var trueIsVisible = parameter as string != "Collapsed";
            var val = value is bool && (bool)value;

            if (!trueIsVisible)
                val = !val;

            if (val)
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
