
namespace AncoraMVVM.Base.Interfaces
{
    /// <summary>
    /// Provides better type safety.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConfigItem<T>
    {
        public string Key { get; set; }
        public T DefaultValue { get; set; }
    }

    public interface IConfigurationManager
    {
        T Get<T>(ConfigItem<T> key);

        void Set<T>(ConfigItem<T> key, T item);
    }
}
