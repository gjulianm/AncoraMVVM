using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncoraMVVM.Base.IoC
{
    public class DependencyInfo
    {
        public Type TargetType { get; set; }
        public bool IsSingleton { get; set; }
    }

    public interface IDependencyModule
    {
        public IDictionary<Type, DependencyInfo> Dependencies { get; }
    }

    public class DependencyModule : IDependencyModule
    {
        public Dictionary<Type, DependencyInfo> Dependencies { get; set; }

        public DependencyModule()
        {
            Dependencies = new Dictionary<Type, DependencyInfo>();
        }

        protected void AddDep<T, TImpl>(bool singleton = false)
        {
            Dependencies.Add(typeof(T), 
                new DependencyInfo
                {
                    TargetType = typeof(TImpl),
                    IsSingleton = singleton
                });
        }
    }
}
