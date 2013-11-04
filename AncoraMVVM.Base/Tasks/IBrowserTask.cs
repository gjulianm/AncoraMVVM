using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncoraMVVM.Base.Tasks
{
    public interface IBrowserTask : ITask
    {
        Uri Uri { get; set; }
    }
}
