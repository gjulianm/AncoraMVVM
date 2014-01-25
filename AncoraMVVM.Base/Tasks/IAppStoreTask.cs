
namespace AncoraMVVM.Base.Tasks
{
    public enum AppStoreContentType { Applications, Media }

    public interface IAppStoreTask : ITask
    {
        AppStoreContentType ContentType { get; set; }
        string ContentId { get; set; }
    }
}
