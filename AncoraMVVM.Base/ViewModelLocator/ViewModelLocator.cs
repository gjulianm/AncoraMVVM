using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace AncoraMVVM.Base.ViewModelLocator
{
    public abstract class ViewModelLocator
    {
        public bool SuppressDictionaryGenerationOutput { get; set; }

        public IDictionary<Type, Type> PageToViewModelMap { get; private set; }

        public ViewModelLocator()
        {
            PageToViewModelMap = new Dictionary<Type, Type>();
        }

        protected abstract bool IsPageType(Type type);

        protected bool IsViewModelType(Type type)
        {
            return type.IsSubclassOf(typeof(ViewModelBase));
        }

        private IEnumerable<Tuple<Type, Type>> GetAttributedPages(Assembly assembly)
        {
            var attributedTypes = ViewModelAttribute.GetAttributedTypes(assembly);

            foreach (var pair in attributedTypes)
            {
                if (!IsPageType(pair.Type))
                    Debug.WriteLine("AncoraMVVM: Warning: Type {0} is marked with ViewModelAttribute, but it's not a page class.", pair.Type.FullName);
                else if (!IsViewModelType(pair.Attribute.ViewModel))
                    Debug.WriteLine("AncoraMVVM: Warning: The argument for ViewModel attribute of type {0} is {1}, which does not inherit from ViewModelBase.", pair.Type.FullName, pair.Attribute.ViewModel.FullName);
                else
                    yield return Tuple.Create(pair.Type, pair.Attribute.ViewModel);
            }
        }

        protected void RegisterPagesAndViewModels(Assembly assembly)
        {
            var attributed = GetAttributedPages(assembly);
            WriteDictionaryToDebug(attributed);

            foreach (var pair in attributed)
                PageToViewModelMap.Add(pair.Item1, pair.Item2);
        }

        private void WriteDictionaryToDebug(IEnumerable<Tuple<Type, Type>> attributed)
        {
            if (SuppressDictionaryGenerationOutput)
                return;

            Debug.WriteLine("AncoraMVVM: Generated page dictionary:");
            Debug.WriteLine("    var dictionary = new Dictionary<Type, Type> {{ ");

            foreach (var pair in attributed)
                Debug.WriteLine("    {{ typeof({0}), typeof({1}), }}", pair.Item1.FullName, pair.Item2.FullName);

            Debug.WriteLine("    }};");
        }

        protected ViewModelBase GetViewModelForType(Type type)
        {
            var viewModelType = PageToViewModelMap[type];
            return (ViewModelBase)Activator.CreateInstance(viewModelType);
        }
    }
}
