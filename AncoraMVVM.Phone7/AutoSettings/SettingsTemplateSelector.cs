using System.Windows;
using AncoraMVVM.Base.AutoSettings;

namespace AncoraMVVM.Phone.AutoSettings
{
    public class SettingsTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SeparatorTemplate { get; set; }
        public DataTemplate StringTemplate { get; set; }
        public DataTemplate NumericTemplate { get; set; }
        public DataTemplate MultipleChoiceTemplate { get; set; }
        public DataTemplate BoolTemplate { get; set; }


        public override DataTemplate SelectTemplate(
            object item, DependencyObject container)
        {
            if (item is StringSetting)
                return StringTemplate;
            else if (item is NumericSetting)
                return NumericTemplate;
            else if (item is BoolSetting)
                return BoolTemplate;
            else if (item is SeparatorSetting)
                return SeparatorTemplate;
            else if (IsMultipleChoiceSetting(item))
                return MultipleChoiceTemplate;
            else
                return null;
        }

        private bool IsMultipleChoiceSetting(object item)
        {
            var type = item.GetType();

            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(MultipleChoiceSetting<>);
        }
    }
}
