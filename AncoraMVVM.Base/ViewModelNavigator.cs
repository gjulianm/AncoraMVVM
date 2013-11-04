using AncoraMVVM.Base.Interfaces;
using AncoraMVVM.Base.ViewModelLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace AncoraMVVM.Base
{
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public sealed class RootNamespaceAttribute : Attribute
    {
        readonly string rootNamespace;

        public RootNamespaceAttribute(string rootNamespace)
        {
            this.rootNamespace = rootNamespace;
        }

        public string RootNamespace
        {
            get { return rootNamespace; }
        }
    }

    public abstract class ViewModelNavigationService : INavigationService
    {
        public IDictionary<Type, string> ViewModelToUriMap { get; private set; }

        public ViewModelNavigationService()
        {
            ViewModelToUriMap = new Dictionary<Type, string>();
        }

        public void Initialize()
        {
            Initialize(Assembly.GetCallingAssembly());
        }

        public void Initialize(Assembly assembly)
        {
            Initialize(assembly,
                ViewModelAttribute.GetAttributedTypes(assembly)
                .ToDictionary(x => x.Type, x => x.Attribute.ViewModel));
        }

        public void Initialize(Assembly assembly, IDictionary<Type, Type> pageToViewModelMap)
        {
            var rootNamespace = GetRootNamespace(assembly);

            ViewModelToUriMap = pageToViewModelMap.ToDictionary(
                pair => pair.Value, // The ViewModel type is the key
                pair => GetUriFromPageType(pair.Key, rootNamespace)
                );
        }

        private string GetUriFromPageType(Type type, string rootNamespace)
        {
            var typeFullName = type.FullName;
            var typePath = typeFullName.Replace(rootNamespace, "");
            var pagePath = typePath.Replace('.', '/') + ".xaml";

            return pagePath;
        }

        private string GetRootNamespace(Assembly assembly)
        {
            var atts = assembly.GetCustomAttributes(typeof(RootNamespaceAttribute), false);

            if (atts == null || atts.Length == 0)
                throw new InvalidOperationException(String.Format("The assembly {0} doesn't have the RootNamespace attribute configured.", assembly.FullName));

            return (atts[0] as RootNamespaceAttribute).RootNamespace;
        }

        public void Navigate<T>() where T : ViewModelBase
        {
            Navigate(typeof(T));
        }

        public void Navigate(Type type)
        {
            string pageUri;

            if (!ViewModelToUriMap.TryGetValue(type, out pageUri))
                throw new KeyNotFoundException(String.Format("There's no page registered with the ViewModel {0}", type.FullName));

            Navigate(pageUri);
        }

        #region INavigationService members
        public abstract void Navigate(string page);

        public abstract void Navigate(Uri page);

        public abstract void GoBack();

        public abstract bool CanGoBack { get; }

        public abstract void ClearNavigationStack();

        public abstract void ClearLastStackEntries(int count);
        #endregion
    }
}
