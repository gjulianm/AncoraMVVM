
using AncoraMVVM.Base.IoC;
namespace AncoraMVVM.Base.Interfaces
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
    }
}
