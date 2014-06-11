using AncoraMVVM.Base.Diagnostics;
using AncoraMVVM.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AncoraMVVM.Base.AutoSettings
{
    public abstract class Setting : ObservableObject
    {
        public string Name { get; set; }

        public Setting(string title)
        {
            Name = title;
        }
    }

    public abstract class Setting<T> : Setting
    {
        public ConfigItem<T> Configuration { get; set; }

        public Setting(string title, ConfigItem<T> config)
            : base(title)
        {
            Configuration = config;

            if (config != null)
                Value = config.Value;

            this.PropertyChanged += Setting_PropertyChanged;
        }

        void Setting_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
                OnValueChanged();
        }

        protected virtual void OnValueChanged()
        {
            Configuration.Value = Value;
        }

        private T _value;
        public virtual T Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (!object.Equals(value, _value))
                {
                    _value = value;
                    RaisePropertyChanged("Value");
                }
            }
        }

    }

    public class StringSetting : Setting<string>
    {
        public StringSetting(string title, ConfigItem<string> config)
            : base(title, config)
        {
        }
    }

    public class NumericSetting : Setting<int?>
    {
        public NumericSetting(string title, ConfigItem<int?> config)
            : base(title, config)
        {
        }
    }

    public class MultipleChoiceSetting<T> : Setting<T>
    {
        public MultipleChoiceSetting(string title, ConfigItem<T> config, Dictionary<T, string> enumNames)
            : base(title, config)
        {
            EnumNames = enumNames;
            Options = EnumNames.Values.ToList();

            if (Configuration != null)
                SelectedIndex = Options.IndexOf(EnumNames[Value]);

        }

        public Dictionary<T, string> EnumNames { get; set; }

        public List<string> Options { get; set; }

        private int selectedIndex;
        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                if (selectedIndex != value)
                {
                    selectedIndex = value;
                    TrySetValue();
                }
            }
        }

        protected void TrySetValue()
        {
            try
            {
                if (SelectedIndex >= 0 && SelectedIndex < Options.Count)
                    Configuration.Value = EnumNames.FirstOrDefault(x => x.Value == Options[SelectedIndex]).Key;
            }
            catch (Exception e)
            {
                AncoraLogger.Instance.LogException("Error parsing MultipleChoiceSetting's enum value.", e);
            }
        }
    }

    public class BoolSetting : Setting<bool?>
    {
        public BoolSetting(string title, ConfigItem<bool?> config)
            : base(title, config)
        {
        }
    }

    public class SeparatorSetting : Setting
    {
        public SeparatorSetting(string title)
            : base(title)
        {
        }
    }
}
