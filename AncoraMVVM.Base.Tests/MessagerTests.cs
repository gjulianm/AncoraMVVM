using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AncoraMVVM.Base.Tests
{
    [TestFixture]
    public class MessagerTests
    {
        private class DummyVM : ViewModelBase
        {

        }

        private class DummyVM2 : ViewModelBase
        {

        }

        [Test]
        public void Receive_MessageAvailable_Received()
        {
            var messager = new Messager();
            var obj = new object();
            messager.SendTo<DummyVM, object>(obj);

            var received = messager.Receive<DummyVM, object>();
            Assert.AreSame(obj, received);
        }

        [Test]
        public void Receive_MessageNotAvailable_ReturnsNull()
        {
            var messager = new Messager();

            var received = messager.Receive<DummyVM, object>();
            Assert.IsNull(received);
        }

        [Test]
        public void Receive_MessagesForOtherVM_ReturnsNull()
        {
            var messager = new Messager();
            var obj = new object();
            messager.SendTo<DummyVM2, object>(obj);

            var received = messager.Receive<DummyVM, object>();
            Assert.IsNull(received);
        }

        [Test]
        public void Receive_MessageIsOtherType_ReturnsNull()
        {
            var messager = new Messager();
            var obj = new List<int>();
            messager.SendTo<DummyVM, List<int>>(obj);

            var received = messager.Receive<DummyVM, EventArgs>();
            Assert.IsNull(received);
        }

        [Test]
        public void Receive_MessageIsInheritedType_ReturnsNull()
        {
            var messager = new Messager();
            var obj = new List<int>();
            messager.SendTo<DummyVM, List<int>>(obj);

            var received = messager.Receive<DummyVM, object>();
            Assert.IsNull(received);
        }

        [Test]
        public void Receive_VariousMessagesSent_ReturnsCorrectObject()
        {
            var messager = new Messager();
            var obj = new List<int>();
            messager.SendTo<DummyVM2, List<int>>(new List<int> { 1, 2, 3 });
            messager.SendTo<DummyVM, Exception>(new Exception());
            messager.SendTo<DummyVM2, object>(new object());
            messager.SendTo<DummyVM, List<int>>(obj);

            var received = messager.Receive<DummyVM, List<int>>();
            Assert.AreSame(obj, received);
        }

        [Test]
        public void Receive_MessageOverwrite_ReturnsLast()
        {
            var messager = new Messager();
            var obj = new List<int>();
            messager.SendTo<DummyVM, List<int>>(new List<int> { 1, 2, 3 });
            messager.SendTo<DummyVM, List<int>>(obj);

            var received = messager.Receive<DummyVM, List<int>>();
            Assert.AreSame(obj, received);
        }
    }
}
