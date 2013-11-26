
namespace AncoraMVVM.Base.Interfaces
{
    public interface INotificationService
    {
        void ShowMessage(string message);
        void ShowWarning(string message);
        void ShowError(string message);
        void ShowProgressIndicatorMessage(string message);
        bool Prompt(string message);
    }
}
