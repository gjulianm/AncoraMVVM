using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AncoraMVVM.Base;
using AncoraMVVM.Base.AutoSettings;
using AncoraMVVM.Base.Interfaces;
using PropertyChanged;

namespace AncoraMVVM.Phone.Sample.ViewModels
{
    public enum ChoicesEnum
    {
        Choice1, Choice2, Choice3
    }


    public class SampleSettingsData
    {
        public ObservableCollection<Setting> Settings
        {
            get
            {
                return new ObservableCollection<Setting>
                {
                    new BoolSetting("This is a boolean setting", null),
                    new NumericSetting("A numeric setting", null),
                    new StringSetting("String time!", null),
                    new SeparatorSetting("More interesting things"),
                    new MultipleChoiceSetting<ChoicesEnum?>("Choose one!", null,
                        new Dictionary<ChoicesEnum?,string>
                        {
                            { ChoicesEnum.Choice1, "This is 1" },
                            { ChoicesEnum.Choice2, "And this is 2" },
                            { ChoicesEnum.Choice3, "Third choice!" }
                        })
                };
            }
        }

        public IEnumerable<MultipleChoiceSetting<ChoicesEnum?>> MultipleChoices { get { return Settings.OfType<MultipleChoiceSetting<ChoicesEnum?>>(); } }
        public IEnumerable<NumericSetting> Numerics { get { return Settings.OfType<NumericSetting>(); } }
        public IEnumerable<StringSetting> Strings { get { return Settings.OfType<StringSetting>(); } }
        public IEnumerable<SeparatorSetting> Separators { get { return Settings.OfType<SeparatorSetting>(); } }
        public IEnumerable<BoolSetting> Bools { get { return Settings.OfType<BoolSetting>(); } }
    }

    [ImplementPropertyChanged]
    public class AutoSettingsModel : ViewModelBase
    {
        public SafeObservable<Setting> Settings { get; set; }

        public AutoSettingsModel()
        {
            var boolConfig = new ConfigItem<bool?>
            {
                Key = "bool",
                DefaultValue = false
            };

            var numConfig = new ConfigItem<int?>
            {
                Key = "int",
                DefaultValue = 42
            };

            var strConfig = new ConfigItem<string>
            {
                Key = "str",
                DefaultValue = "hello"
            };

            var multipleChoiceConfig = new ConfigItem<ChoicesEnum?>
            {
                Key = "enum",
                DefaultValue = ChoicesEnum.Choice2
            };

            Settings = new SafeObservable<Setting>
            {
                new BoolSetting("This is a boolean setting", boolConfig),
                new NumericSetting("A numeric setting", numConfig),
                new StringSetting("String time!", strConfig),
                new SeparatorSetting("More interesting things"),
                new MultipleChoiceSetting<ChoicesEnum?>("Choose one!", multipleChoiceConfig,
                    new Dictionary<ChoicesEnum?,string>
                    {
                        { ChoicesEnum.Choice1, "This is 1" },
                        { ChoicesEnum.Choice2, "And this is 2" },
                        { ChoicesEnum.Choice3, "Third choice!" }
                    })
            };
        }
    }
}
