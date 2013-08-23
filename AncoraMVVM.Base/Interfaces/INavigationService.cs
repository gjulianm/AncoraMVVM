using System;

namespace AncoraMVVM.Base.Interfaces
{
    public interface INavigationService
    {
        void Navigate(string page);
        void Navigate(Uri page);
        void Navigate<T>() where T : ViewModelBase;
        void Navigate(Type type);
        void GoBack();
        bool CanGoBack { get; }
        void ClearNavigationStack();
    }
}
