using System;
using System.Windows;
using System.Windows.Controls;


namespace AncoraMVVM.Phone7
{
    public static class TextBoxUpdater
    {
        public static readonly DependencyProperty UpdateBindingOnChangeProperty = DependencyProperty.RegisterAttached(
            "UpdateBindingOnChange",
            typeof(Boolean),
            typeof(TextBoxUpdater),
            new PropertyMetadata(false, OnUpdateChanged));

        public static void SetUpdateBindingOnChange(DependencyObject element, Boolean value)
        {
            element.SetValue(UpdateBindingOnChangeProperty, value);
        }

        public static bool GetUpdateBindingOnChange(DependencyObject element)
        {
            return (Boolean)element.GetValue(UpdateBindingOnChangeProperty);
        }

        private static void OnUpdateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var updateOnTextChange = GetUpdateBindingOnChange(d);
            var textBox = d as TextBox;

            if (textBox != null && updateOnTextChange)
            {
                textBox.TextChanged += (sender, ea) =>
                {
                    var binding = textBox.GetBindingExpression(TextBox.TextProperty);
                    if (binding != null)
                        binding.UpdateSource();
                };
            }
        }
    }
}
