using System;
using AncoraMVVM.Base.Tasks;

namespace AncoraMVVM.Phone.Tasks
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
