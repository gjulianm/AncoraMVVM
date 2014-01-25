using AncoraMVVM.Base.Interfaces;
using AncoraMVVM.Base.IoC;
using AncoraMVVM.Base.Tasks;
using Microsoft.Phone.Tasks;

namespace AncoraMVVM.Phone7.Tasks
{
    public class AppStoreTask : IAppStoreTask
    {
        public AppStoreContentType ContentType { get; set; }
        public string ContentId { get; set; }

        public void Show()
        {
            var task = new MarketplaceDetailTask();
            task.ContentIdentifier = ContentId;

            if (ContentType == AppStoreContentType.Media)
                task.ContentType = MarketplaceContentType.Music;
            else
                task.ContentType = MarketplaceContentType.Applications;

            Dependency.Resolve<IDispatcher>().InvokeIfRequired(task.Show);
        }
    }
}
