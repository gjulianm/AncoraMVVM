
using System;
namespace AncoraMVVM.Base.Tasks
{
    public interface ITaskFactory
    {
        IBrowserTask CreateBrowserTask(Uri uri);
        IAppStoreTask CreateAppStoreTask(AppStoreContentType contentType, string contentId); 
    }
}
