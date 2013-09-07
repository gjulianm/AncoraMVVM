using System;
using System.Collections.Generic;

namespace AncoraMVVM.Base
{
    public static class IListExtension
    {
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            if (list == null)
                throw new NullReferenceException("The List can't be null.");

            foreach (var item in items)
                list.Add(item);
        }
    }
}
