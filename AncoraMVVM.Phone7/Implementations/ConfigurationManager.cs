using AncoraMVVM.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;

namespace AncoraMVVM.Phone.Implementations
{
    internal static class TypeExtensions
    {
        public static bool HasParameterlessConstructor(this Type type)
        {
            return type.GetConstructor(Type.EmptyTypes) != null;
        }
    }

    public class ConfigurationManager : IConfigurationManager
    {
        private static Dictionary<string, object> cachedObjects = new Dictionary<string, object>();
        private static object dicLock = new object();
        private static object storageLock = new object();

        private IsolatedStorageSettings config = IsolatedStorageSettings.ApplicationSettings;

        public T Get<T>(ConfigItem<T> key)
        {
            object cached;

            T item;

            try
            {
                lock (dicLock)
                    if (cachedObjects.TryGetValue(key.Key, out cached))
                        return (T)cached;

                lock (storageLock)
                {
                    if (!config.TryGetValue<T>(key.Key, out item))
                    {
                        item = key.DefaultValue;

                        config.Add(key.Key, item);
                        config.Save();
                    }
                }
            }
            catch (InvalidCastException ex)
            {
                Debug.WriteLine("AncoraMVVM: Invalid cast on config for key {0}: {1}", key.Key, ex);
                item = default(T);
                lock (storageLock)
                {
                    config.Remove(key.Key);
                    config.Save();
                }
            }

            cachedObjects[key.Key] = item;

            return item;
        }

        public void Set<T>(ConfigItem<T> key, T item)
        {
            lock (dicLock)
                cachedObjects[key.Key] = item;

            lock (storageLock)
            {
                config[key.Key] = item;
                config.Save();
            }
        }
    }
}
