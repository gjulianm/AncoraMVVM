using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AncoraMVVM.Base.ViewModelLocator
{
    public class ViewModelAttributePair
    {
        public Type Type { get; set; }
        public ViewModelAttribute Attribute { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ViewModelAttribute : Attribute
    {
        readonly Type viewModel;

        // This is a positional argument
        public ViewModelAttribute(Type viewModel)
        {
            this.viewModel = viewModel;
        }

        public Type ViewModel
        {
            get { return viewModel; }
        }

        private static Dictionary<Assembly, IEnumerable<ViewModelAttributePair>> cachedTypes = new Dictionary<Assembly, IEnumerable<ViewModelAttributePair>>();

        public static IEnumerable<ViewModelAttributePair> GetAttributedTypes(Assembly assembly)
        {
            IEnumerable<ViewModelAttributePair> attributedTypes;

            if (!cachedTypes.TryGetValue(assembly, out attributedTypes))
            {
                attributedTypes = from type in assembly.GetTypes()
                                  let attributes = type.GetCustomAttributes(typeof(ViewModelAttribute), true)
                                  where attributes != null && attributes.Length > 0
                                  select new ViewModelAttributePair { Type = type, Attribute = attributes.First() as ViewModelAttribute };
            }

            return attributedTypes;
        }
    }
}
