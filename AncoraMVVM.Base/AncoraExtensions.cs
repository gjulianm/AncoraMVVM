using AncoraMVVM.Base.Interfaces;
using AncoraMVVM.Base.IoC;

namespace AncoraMVVM.Base
{
    public static class AncoraExtensions
    {
        public static void MessageAndNavigate<TViewModel, TMessage>(this INavigationService navigator, TMessage message)
            where TViewModel : ViewModelBase
            where TMessage : class
        {
            var messager = Dependency.Resolve<IMessager>();
            messager.SendTo<TViewModel, TMessage>(message);
            navigator.Navigate<TViewModel>();
        }

        public static void Loading(this IProgressIndicator indicator)
        {
            indicator.Loading("");
        }

        public static void Loading(this IProgressIndicator indicator, string title)
        {
            indicator.IsLoading = true;
            indicator.Text = title;
        }

        public static void Finished(this IProgressIndicator indicator)
        {
            indicator.IsLoading = false;
            indicator.Text = "";
        }
    }
}
