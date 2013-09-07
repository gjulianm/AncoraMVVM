using System;
using System.Collections.Generic;

namespace AncoraMVVM.Base.Collections
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Zips two sequences in one list of tuples.
        /// The result has the length of the shortest list.
        /// </summary>
        /// <typeparam name="T1">Type of list 1.</typeparam>
        /// <typeparam name="T2">Type of list 2.</typeparam>
        /// <param name="first">First list to be zipped.</param>
        /// <param name="second">Second list to be zipped.</param>
        /// <returns>List of tuples</returns>
        public static IEnumerable<Tuple<T1, T2>> Zip<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second)
        {
            var enum1 = first.GetEnumerator();
            var enum2 = second.GetEnumerator();

            while (enum1.MoveNext() && enum2.MoveNext())
                yield return Tuple.Create(enum1.Current, enum2.Current);
        }
    }
}
