using AncoraMVVM.Base.Interfaces;
using AncoraMVVM.Base.IoC;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;


namespace AncoraMVVM.Base
{
    /// <summary>
    /// A thread-safe observable collection. Uses a dispatcher to run PropertyChanged event on the UI Thread.
    /// 
    /// Adapted from http://www.deanchalk.me.uk/post/Thread-Safe-Dispatcher-Safe-Observable-Collection-for-WPF.aspx.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SafeObservable<T> : IList<T>, IList, INotifyCollectionChanged
    {
        protected List<T> collection = new List<T>();
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
        protected void RaiseCollectionReset()
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

        protected void RaiseCollectionAdd(T item, int index)
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

        protected void RaiseCollectionRemove(T item, int index)
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

        protected void RaiseCollectionReplace(T value, T old, int index)
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
            foreach (var item in items)
                Add(item);
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
            get { return false; }
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

        #region IList members
        // I hate Ilist.
        int IList.Add(object value)
        {
            Add((T)value);
            return collection.Count - 1;
        }

        bool IList.Contains(object value)
        {
            return Contains((T)value);
        }

        int IList.IndexOf(object value)
        {
            return IndexOf((T)value);
        }

        void IList.Insert(int index, object value)
        {
            Insert(index, (T)value);
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        void IList.Remove(object value)
        {
            Remove((T)value);
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = (T)value;
            }
        }

        void ICollection.CopyTo(System.Array array, int index)
        {
            T[] typeArray = new T[array.Length];
            CopyTo(typeArray, index);
            typeArray.CopyTo(array, index);
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get { return null; }
        }
        #endregion
    }
}