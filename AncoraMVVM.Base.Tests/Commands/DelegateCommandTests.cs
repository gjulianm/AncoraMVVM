using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace AncoraMVVM.Base.Test
{
    [TestClass]
    public class DelegateCommandTests
    {
        [TestMethod]
        public void CanExecute_NoConstructorParams_IsTrue()
        {
            var cmd = new DelegateCommand((p) => { });
            Assert.IsTrue(cmd.CanExecute(null));
        }

        [TestMethod]
        public void Execute_CalledActionWithParameter()
        {
            var param = new object();
            object receivedParam = null;
            var cmd = new DelegateCommand(p => receivedParam = p);

            cmd.Execute(param);
            Assert.AreSame(param, receivedParam);
        }

        private class DummyNotifier : INotifyPropertyChanged
        {
            private int number;
            public int Number
            {
                get
                {
                    return number;
                }
                set
                {
                    number = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Number"));
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        [TestMethod]
        public void BindToPropertyChange_Propagated()
        {
            bool wasCalled = false;
            var not = new DummyNotifier();
            var cmd = new DelegateCommand((p) => { }, (p) => p != null);
            cmd.BindCanExecuteToProperty(not, "Number");
            cmd.CanExecuteChanged += (s, e) => wasCalled = true;

            not.Number = 123;
            Assert.IsTrue(wasCalled);
        }

        [TestMethod]
        public void Execute_CalledAsyncAction_AsyncExecuted()
        {
            bool executed = false;
            var cmd = new DelegateCommand(async (p) =>
            {
                await Task.Delay(200);
                executed = true;
            });

            cmd.Execute(null);
            Assert.IsFalse(executed);
            Thread.Sleep(500);
            Assert.IsTrue(executed);
        }
    }
}