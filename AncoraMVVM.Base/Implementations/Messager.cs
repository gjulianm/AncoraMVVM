using AncoraMVVM.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AncoraMVVM.Base
{
    // TODO: Possible oversizing of the internal message list.
    public class Messager : IMessager
    {
        // Tuple.Item1 => target ViewModel type
        // Tuple.Item2 => Object to transfer
        // Tuple.Item3 => object type (useful when you want base types, but send concrete implementations.
        private List<Tuple<Type, object, Type>> messages = new List<Tuple<Type, object, Type>>();
        private object dicLock = new object();

        public void SendTo<TViewModel, TMessage>(TMessage msg)
            where TViewModel : ViewModelBase
            where TMessage : class
        {
            lock (dicLock)
                messages.Add(Tuple.Create(typeof(TViewModel), (object)msg, typeof(TMessage)));
        }

        public TMessage Receive<TViewModel, TMessage>()
            where TViewModel : ViewModelBase
            where TMessage : class
        {
            var viewModel = typeof(TViewModel);

            return Receive<TMessage>(viewModel);
        }


        public TMessage Receive<TMessage>(Type viewModelType) where TMessage : class
        {
            Tuple<Type, object, Type> pair = null;

            lock (dicLock)
            {
                pair = messages.LastOrDefault(x => x.Item1 == viewModelType && x.Item3 == typeof(TMessage));

                if (pair == null)
                    return null;

                messages.Remove(pair);
            }

            return (TMessage)pair.Item2;
        }
    }
}
