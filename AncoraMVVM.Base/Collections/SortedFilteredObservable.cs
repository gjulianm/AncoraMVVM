using System;
using System.Collections.Generic;
using System.Linq;

namespace AncoraMVVM.Base.Collections
{
    public enum SortOrder { Ascending, Descending }

    /// <summary>
    /// A observable collection that supports filtering and 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SortedFilteredObservable<T> : SafeObservable<T>
    {
        IComparer<T> comparer;
        public IComparer<T> Comparer
        {
            get { return comparer ?? Comparer<T>.Default; }
            set
            {
                if (comparer != value)
                {
                    comparer = value;
                    ReorderList();
                }
            }
        }

        SortOrder sortOrder;
        public SortOrder SortOrder
        {
            get { return sortOrder; }
            set
            {
                if (sortOrder != value)
                {
                    sortOrder = value;
                    ReorderList();
                }
            }
        }

        Predicate<T> filter;
        /// <summary>
        /// The filter used. If Filter(object) returns true, object is discarded.
        /// </summary>
        public Predicate<T> Filter
        {
            get { return filter; }
            set
            {
                filter = value;

                ReevaluateInList();
                ReevaluateDiscarded();
            }
        }

        List<T> discardedItems;

        /// <summary>
        /// Create a instance without sorting nor filtering configured.
        /// </summary>
        public SortedFilteredObservable()
        {
            discardedItems = new List<T>();
        }

        /// <summary>
        /// Create a instance with sorting enabled using the given comparer.
        /// </summary>
        /// <param name="comparer">Comparer.</param>
        public SortedFilteredObservable(IComparer<T> comparer)
            : this(null, comparer)
        {
        }


        /// <summary>
        /// Create a instance without sorting nor filtering configured.
        /// </summary>
        /// <param name="filter">Filter function, discards an item if predicate is true.</param>
        public SortedFilteredObservable(Predicate<T> filter)
            : this()
        {
        }

        /// <summary>
        /// Create a instance with sorting enabled using the given comparer.
        /// </summary>
        /// <param name="comparer">Comparer.</param>
        /// <param name="filter">Filter function, discards an item if predicate is true.</param>
        public SortedFilteredObservable(Predicate<T> filter, IComparer<T> comparer)
            : this(filter)
        {
            Comparer = comparer;
        }


        #region Filter functions.
        internal void ReevaluateDiscarded()
        {
            List<T> itemsAdded = new List<T>();
            foreach (var item in discardedItems)
            {
                if (!Matches(item))
                {
                    itemsAdded.Add(item);
                    OrderedInsert(item);
                }
            }

            foreach (var item in itemsAdded)
                discardedItems.Remove(item);
        }

        internal void ReevaluateInList()
        {
            List<T> itemsToDelete = new List<T>();
            foreach (var item in this)
            {
                if (Matches(item))
                {
                    itemsToDelete.Add(item);
                    discardedItems.Add(item);
                }
            }

            foreach (var item in itemsToDelete)
                this.Remove(item);
        }

        internal bool Matches(T item)
        {
            if (filter == null)
                return false;

            return filter.Invoke(item);
        }
        #endregion

        #region Sorting functions
        internal bool DoesAGoBeforeB(T a, T b)
        {
            var isAGreaterThanB = Comparer.Compare(a, b) > 0;

            if (SortOrder == SortOrder.Descending)
                return isAGreaterThanB;
            else
                return !isAGreaterThanB;
        }

        internal bool AreEqual(T a, T b)
        {
            return Comparer.Compare(a, b) == 0;
        }

        internal void OrderedInsert(T item)
        {
            int i = 0;
            while (i < Count && DoesAGoBeforeB(item, base[i]))
                i++;

            Insert(i, item);
        }

        private void ReorderList()
        {
            IEnumerable<T> ordered;

            if (sortOrder == SortOrder.Descending)
                ordered = this.OrderByDescending(x => x, Comparer);
            else
                ordered = this.OrderBy(x => x, Comparer);

            ordered = ordered.ToList();

            var copyOfCurrent = this.ToList();
            var orderedWithIndexes = ordered.Select((item, index) => new { item, index });
            Clear();

            // And now readd the items, notifying of the corresponding replacements.
            int i = 0;
            foreach (var pair in orderedWithIndexes)
            {
                collection.Add(pair.item);
                RaiseCollectionReplace(pair.item, copyOfCurrent[pair.index], i);
                i++;
            }

            /* Note: Why use this Select(item, index) instead of an easy Zip? Because Zip is giving some weird error, not being able
             * to load System.Runtime on test runs. So, I'm sticking with this. */
        }
        #endregion

        public override void Add(T item)
        {
            if (Matches(item))
                discardedItems.Add(item);
            else
                OrderedInsert(item);
        }
    }
}
