using System;
using AncoraMVVM.Base.Interfaces;
using AncoraMVVM.Base.IoC;
using AncoraMVVM.Base.Tasks;
using Microsoft.Phone.Tasks;

namespace AncoraMVVM.Phone.Tasks
{
    public class BrowserTask : IBrowserTask
    {
        public Uri Uri { get; set; }

        public void Show()
        {
            var task = new WebBrowserTask();
            task.Uri = Uri;
            Dependency.Resolve<IDispatcher>().InvokeIfRequired(task.Show);
        }
    }
}
