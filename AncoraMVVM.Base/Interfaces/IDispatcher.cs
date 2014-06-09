using System;

namespace AncoraMVVM.Base.Interfaces
{
    public interface IDispatcher
    {
        void BeginInvoke(Action action);
        bool IsUIThread { get; }
        void InvokeIfRequired(Action action);
    }

    public abstract class BaseDispatcher : IDispatcher
    {
        public abstract void BeginInvoke(Action action);
        public abstract bool IsUIThread { get; }

        public void InvokeIfRequired(Action action)
        {
            if (IsUIThread)
                action();
            else
                BeginInvoke(action);
        }
    }

    public class DummyDispatcher : BaseDispatcher
    {
        public override void BeginInvoke(Action action)
        {
            action();
        }

        public override bool IsUIThread { get { return true; } }
    }
}
