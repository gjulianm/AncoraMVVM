using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AncoraMVVM.Base.Interfaces
{
    public interface IProgressIndicator : INotifyPropertyChanged
    {
        bool IsLoading { get; set; }
        string Text { get; set; }
    }
}
