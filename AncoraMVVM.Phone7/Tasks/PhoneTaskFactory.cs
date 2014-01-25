using AncoraMVVM.Base.Tasks;
using System;

namespace AncoraMVVM.Phone7.Tasks
{
    public class PhoneTaskFactory : ITaskFactory
    {
        public IBrowserTask CreateBrowserTask(Uri uri)
        {
            return new BrowserTask { Uri = uri };
        }

        public IAppStoreTask CreateAppStoreTask(AppStoreContentType contentType, string contentId)
        {
            return new AppStoreTask { ContentId = contentId, ContentType = contentType };
        }
    }
}
