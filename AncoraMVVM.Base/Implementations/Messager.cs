using AncoraMVVM.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AncoraMVVM.Base
{
    public class Messager : IMessager
    {
        private List<Tuple<Type, object>> messages = new List<Tuple<Type, object>>();
        private object dicLock = new object();

        public void SendTo<TViewModel, TMessage>(TMessage msg)
            where TViewModel : ViewModelBase
            where TMessage : class
        {
            lock (dicLock)
                messages.Add(Tuple.Create(typeof(TViewModel), (object)msg));
        }

        public TMessage Receive<TViewModel, TMessage>()
            where TViewModel : ViewModelBase
            where TMessage : class
        {
            var viewModel = typeof(TViewModel);
            Tuple<Type, object> pair = null;
            lock (dicLock)
                pair = messages.FirstOrDefault(x => x.Item1 == viewModel && x.Item2.GetType() == typeof(TMessage));

            if (pair == null)
                return null;
            else
                return (TMessage)pair.Item2;
        }
    }
}
