using NUnit.Framework;
using System.Collections.Generic;

namespace AncoraMVVM.Base.Tests.Collections
{
    [TestFixture]
    public class DictionaryExtensionTests
    {
        private class DummyClass
        {
            public DummyClass()
            {
                Name = "created from DummyClass()";
            }

            public string Name { get; set; }
        }

        [Test]
        public void GetOrCreate_ElementExists_DoesntCreateIt()
        {
            var dic = new Dictionary<int, DummyClass>();
            var val = new DummyClass { Name = "asd" };

            dic[0] = val;

            Assert.AreSame(val, dic.GetOrCreate(0));
            Assert.AreEqual(val.Name, dic.GetOrCreate(0).Name);
            Assert.AreEqual(1, dic.Count);
        }

        [Test]
        public void GetOrCreate_ElementDoesntExists_CreatesIt()
        {
            var dic = new Dictionary<int, DummyClass>();
            var val = dic.GetOrCreate(0);

            Assert.AreEqual(1, dic.Count);
            Assert.IsNotNull(val);
            Assert.AreEqual(new DummyClass().Name, val.Name);
        }
    }
}
