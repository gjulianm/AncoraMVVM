using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncoraMVVM.Base.Interfaces
{
    public interface INavigationService
    {
        void Navigate(string page);
        void Navigate(Uri page);
        void GoBack();
        bool CanGoBack { get; }
        void ClearNavigationStack();
    }
}
