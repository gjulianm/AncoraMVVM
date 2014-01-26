
using AncoraMVVM.Base.IoC;
namespace AncoraMVVM.Base.Interfaces
{
    /// <summary>
    /// Represents a item stored in the configuration.
    /// Provides better type safety.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConfigItem<T>
    {
        public string Key { get; set; }
        public T DefaultValue { get; set; }

        /// <summary>
        /// Gets this item from the configuration.
        /// </summary>
        /// <returns>The item stored.</returns>
        public T Get()
        {
            // I don't like this approach, but it's soooo useful... 
            var config = Dependency.Resolve<IConfigurationManager>();
            return config.Get(this);
        }

        public void Set(T item)
        {
            var config = Dependency.Resolve<IConfigurationManager>();
            config.Set(this, item);
        }

        public T Value
        {
            get
            {
                return Get();
            }
            set
            {
                Set(value);
            }
        }
    }

    /// <summary>
    /// Configuration manager, with methods to retrieve and save items to and from
    /// the configuration storage.
    /// </summary>
    public interface IConfigurationManager
    {
        T Get<T>(ConfigItem<T> key);

        void Set<T>(ConfigItem<T> key, T item);
    }
}
