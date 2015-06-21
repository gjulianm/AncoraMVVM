using System;
using System.Reflection;

// Idea: http://stackoverflow.com/questions/18594308/using-localized-strings-in-a-listpicker-populated-from-enum
namespace AncoraMVVM.Base.Converters
{
    public abstract class BaseEnumToStringConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                Type type = value.GetType();
                string name = Enum.GetName(type, value);
                if (name != null)
                {
                    FieldInfo field = type.GetField(name);
                    if (field != null)
                    {
                        LocalizedDescriptionAttribute attr =
                               Attribute.GetCustomAttribute(field,
                                 typeof(LocalizedDescriptionAttribute)) as LocalizedDescriptionAttribute;

                        if (attr != null)
                        {
                            return GetLocalizedString(attr.Description);
                        }
                    }
                }
            }
            return null;
        }

        protected abstract string GetLocalizedString(string id);

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
