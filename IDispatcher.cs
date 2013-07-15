﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncoraMVVM
{
    public interface IDispatcher 
    {
        void BeginInvoke(Action action);
        bool IsUIThread { get; }
        void InvokeIfRequired(Action action);
    }
}
