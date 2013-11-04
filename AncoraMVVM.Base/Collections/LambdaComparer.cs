using System;
using System.Collections.Generic;

namespace AncoraMVVM.Base.Collections
{
    public class LambdaComparer<T> : IComparer<T>
    {
        public LambdaComparer(Comparison<T> comparison)
        {
            Comparison = comparison;
        }

        public int Compare(T x, T y)
        {
            return Comparison(x, y);
        }

        public Comparison<T> Comparison { get; set; }
    }
}
