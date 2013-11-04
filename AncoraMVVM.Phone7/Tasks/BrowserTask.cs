using AncoraMVVM.Base.Tasks;
using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncoraMVVM.Phone7.Tasks
{
    public class BrowserTask : IBrowserTask
    {
        public Uri Uri { get; set; }

        public void Show()
        {
            var task = new WebBrowserTask();
            task.Uri = Uri;
            task.Show();
        }
    }
}
