using AncoraMVVM.Base.IoC;
using NUnit.Framework;

namespace AncoraMVVM.Base.Tests
{
    [TestFixture]
    public class ViewModelBaseTests
    {
        private class DummyVM : ViewModelBase
        {
            public object Message { get; set; }

            public void ReceiveMessage()
            {
                Message = ReceiveMessage<object>();
            }
        }

        private class DummyVM2 : ViewModelBase
        {

        }

        [TearDown]
        public void CleanDependency()
        {
            Dependency.Clear();
        }

        [Test]
        public void ReceiveMessage_ReceivesCorrectMessageType()
        {
            var provider = new MockProvider();
            Dependency.Provider = provider;
            var messager = new Messager();

            provider.Messager = messager;

            var vm = new DummyVM();
            var o1 = new object();
            var o2 = (object)"test";

            messager.SendTo<DummyVM, object>(o1);
            messager.SendTo<DummyVM2, object>(o2);

            vm.ReceiveMessage();

            Assert.AreSame(o1, vm.Message);
        }
    }
}
