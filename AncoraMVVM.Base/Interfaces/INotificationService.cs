using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncoraMVVM
{
    public interface INotificationService
    {
        void ShowMessage(string message);
        void ShowWarning(string message);
        void ShowError(string message);
        void ShowProgressIndicatorMessage(string message);
    }
}
