using AncoraMVVM.Base.Interfaces;
using AncoraMVVM.Base.IoC;
using AncoraMVVM.Phone.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncoraMVVM.Phone
{
    public class PhoneDependencyModule : DependencyModule
    {
        public PhoneDependencyModule() : base()
        {
            AddDep<INotificationService, NotificationService>();
            AddDep<IProgressIndicator, GlobalProgress>(true);
        }
    }
}
