
namespace AncoraMVVM.Base.Interfaces
{
    public interface IMessager
    {
        void SendTo<TViewModel, TMessage>(TMessage msg)
            where TViewModel : ViewModelBase
            where TMessage : class;
        TMessage Receive<TViewModel, TMessage>()
            where TViewModel : ViewModelBase
            where TMessage : class;
    }
}
