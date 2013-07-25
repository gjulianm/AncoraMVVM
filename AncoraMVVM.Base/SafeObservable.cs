using AncoraMVVM.Base.Interfaces;
using AncoraMVVM.Base.IoC;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;


namespace AncoraMVVM.Base
{
    // Adapted from http://www.deanchalk.me.uk/post/Thread-Safe-Dispatcher-Safe-Observable-Collection-for-WPF.aspx.

    public class SafeObservable<T> : IList<T>, INotifyCollectionChanged
    {
        private IList<T> collection = new List<T>();
        private IDispatcher dispatcher;

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        protected object sync = new object();

        public SafeObservable(IEnumerable<T> source)
            : this()
        {
            collection = new List<T>(source);
        }

        public SafeObservable()
        {
            dispatcher = Dependency.Resolve<IDispatcher>();
        }

        public SafeObservable(IDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        #region Event raisers.
        void RaiseCollectionReset()
        {
            var copy = CollectionChanged;
            if (copy != null)
            {
                if (!dispatcher.IsUIThread)
                    dispatcher.BeginInvoke(RaiseCollectionReset);
                else
                    copy(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        void RaiseCollectionAdd(T item, int index)
        {
            var copy = CollectionChanged;
            if (copy != null)
            {
                if (!dispatcher.IsUIThread)
                    dispatcher.BeginInvoke(() => RaiseCollectionAdd(item, index));
                else
                    copy(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
            }
        }

        void RaiseCollectionRemove(T item, int index)
        {
            var copy = CollectionChanged;
            if (copy != null)
            {
                if (!dispatcher.IsUIThread)
                    dispatcher.BeginInvoke(() => RaiseCollectionRemove(item, index));
                else
                    copy(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
            }
        }

        void RaiseCollectionReplace(T value, T old, int index)
        {
            var copy = CollectionChanged;
            if (copy != null)
            {
                if (!dispatcher.IsUIThread)
                    dispatcher.BeginInvoke(() => RaiseCollectionReplace(value, old, index));
                else
                    copy(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, old, index));
            }
        }
        #endregion

        public virtual void Add(T item)
        {
            int index;

            lock (sync)
            {
                collection.Add(item);
                index = collection.Count;
            }

            RaiseCollectionAdd(item, index);
        }

        public void BulkAdd(IEnumerable<T> items)
        {
            int added = 0;
            foreach (var item in items)
            {
                Add(item);
                added++;
                if (added >= 5)
                {
                    TaskEx.Delay(10).RunSynchronously();
                    added = 0;
                }
            }
        }


        public virtual void Clear()
        {
            lock (sync)
                collection.Clear();

            RaiseCollectionReset();
        }

        public virtual bool Contains(T item)
        {
            bool result;
            lock (sync)
                result = collection.Contains(item);
            return result;
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            lock (sync)
                collection.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                int result;
                lock (sync)
                    result = collection.Count;
                return result;
            }
        }

        public bool IsReadOnly
        {
            get { return collection.IsReadOnly; }
        }

        public bool Remove(T item)
        {
            bool result;
            int index;
            lock (sync)
            {
                index = collection.IndexOf(item);
                if (index == -1)
                    return false;

                result = collection.Remove(item);
            }

            if (result)
                RaiseCollectionRemove(item, index);

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            IEnumerable<T> copyList;
            lock (sync)
                copyList = collection.ToList();

            return copyList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(T item)
        {
            int result;
            lock (sync)
                result = collection.IndexOf(item);
            return result;
        }

        public void Insert(int index, T item)
        {
            lock (sync)
                collection.Insert(index, item);

            RaiseCollectionAdd(item, index);
        }

        public void RemoveAt(int index)
        {
            T item;
            lock (sync)
            {
                if (collection.Count == 0 || collection.Count <= index)
                {
                    return;
                }
                item = collection[index];
                collection.RemoveAt(index);
            }

            RaiseCollectionRemove(item, index);
        }

        public T this[int index]
        {
            get
            {
                T result;
                lock (sync)
                    result = collection[index];
                return result;
            }
            set
            {
                T old;
                lock (sync)
                {
                    if (collection.Count == 0 || collection.Count <= index || collection[index].Equals(value))
                        return;
                    old = collection[index];
                    collection[index] = value;
                }

                RaiseCollectionReplace(value, old, index);
            }

        }
    }
}