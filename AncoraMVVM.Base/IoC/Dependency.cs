using System;
using System.Collections.Generic;

namespace AncoraMVVM.Base.IoC
{
    public static class Dependency
    {
        private static Dictionary<Type, Type> typeMap = new Dictionary<Type, Type>();
        private static Dictionary<Type, object> singletonMap = new Dictionary<Type, object>();

        /// <summary>
        /// Clears the internal dictionaries. For testing purposes.
        /// </summary>
        internal static void Clear()
        {
            typeMap.Clear();
            singletonMap.Clear();
        }

        public static void Register<T, TImpl>(bool singleton = false)
        {
            Register(typeof(T), typeof(TImpl), singleton);
        }

        public static void Register(Type generic, Type impl, bool singleton = false)
        {
            if (!singleton)
                typeMap.Add(generic, impl);
            else
                singletonMap.Add(generic, Activator.CreateInstance(impl));
        }

        public static T Resolve<T>()
        {
            object val;
            if (singletonMap.TryGetValue(typeof(T), out val))
                return (T)val;
            else
                return (T)Activator.CreateInstance(typeMap[typeof(T)]);
        }

        public static void RegisterModule(IDependencyModule module)
        {
            foreach (var pair in module.Dependencies)
                Register(pair.Key, pair.Value.TargetType, pair.Value.IsSingleton);
        }
    }
}
