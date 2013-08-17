using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncoraMVVM.Base
{
    public static class IListExtension
    {
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            foreach (var item in items)
                list.Add(item);
        }
    }
}
