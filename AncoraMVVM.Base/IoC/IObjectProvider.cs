
namespace AncoraMVVM.Base.IoC
{
    public interface IObjectProvider
    {
        T Resolve<T>() where T : class;
    }
}
