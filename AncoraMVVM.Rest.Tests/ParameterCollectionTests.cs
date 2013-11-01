using NUnit.Framework;

namespace AncoraMVVM.Rest.Tests
{
    [TestFixture]
    public class ParameterCollectionTests
    {
        [Test]
        public void Constructor_ParameterList_PairsCorrectly()
        {
            var pars = new object[] { "1", 1, "2", 2, "3", 3 };

            var collection = new ParameterCollection(pars);

            Assert.AreEqual(3, collection.Count, "Collection doesn't have the correct number of pairs");

            foreach (var par in collection)
                Assert.AreEqual(par.Key, par.Value.ToString());
        }

        [Test]
        public void Constructor_ParameterListOdd_IgnoresLastTerm()
        {
            var pars = new object[] { "1", 1, "2", 2, "3", 3, "4" };

            var collection = new ParameterCollection(pars);

            Assert.AreEqual(3, collection.Count, "Collection doesn't have the correct number of pairs");

            foreach (var par in collection)
                Assert.AreEqual(par.Key, par.Value.ToString());
        }

        [Test]
        public void BuildQueryString_NoParameters_ReturnsEmpty()
        {
            var collection = new ParameterCollection();

            Assert.AreEqual("", collection.BuildQueryString());
        }

        [Test]
        public void BuildQueryString_OneParameter_NoAmpersand()
        {
            var collection = new ParameterCollection(new object[] { "1", 1 });

            Assert.AreEqual("?1=1", collection.BuildQueryString());
        }

        [Test]
        public void BuildQueryString_VariousParameters_WellJoined()
        {
            var collection = new ParameterCollection(new object[] { "1", 1, "2", 2 });

            Assert.AreEqual("?1=1&2=2", collection.BuildQueryString());
        }

        [Test]
        public void BuildPostContent_NoParameters_ReturnsEmpty()
        {
            var collection = new ParameterCollection();

            Assert.AreEqual("", collection.BuildPostContent());
        }

        [Test]
        public void BuildPostContent_OneParameter_NoAmpersandNoQuestion()
        {
            var collection = new ParameterCollection(new object[] { "1", 1 });

            Assert.AreEqual("1=1", collection.BuildPostContent());
        }

        [Test]
        public void BuildPostContent_VariousParameters_WellJoined()
        {
            var collection = new ParameterCollection(new object[] { "1", 1, "2", 2 });

            Assert.AreEqual("1=1&2=2", collection.BuildPostContent());
        }

        [Test]
        public void ContainsKey_Contained_ReturnsTrue()
        {
            var collection = new ParameterCollection();
            collection.Add("test", 1);

            Assert.IsTrue(collection.ContainsKey("test"));
        }

        [Test]
        public void ContainsKey_Contained_ReturnsFalse()
        {
            var collection = new ParameterCollection();
            collection.Add("not", 1);

            Assert.IsFalse(collection.ContainsKey("test"));
        }

        [Test]
        public void IndexerGet_KeyDoesntExist_ReturnsNull()
        {
            var collection = new ParameterCollection();
            Assert.IsNull(collection["test"]);
        }

        [Test]
        public void IndexerGet_KeyExists_ReturnsValue()
        {
            var collection = new ParameterCollection();
            var testObj = new object();
            collection.Add("test", testObj);

            Assert.AreSame(testObj, collection["test"]);
        }

        [Test]
        public void IndexerSet_KeyDoesntExist_CreatesObject()
        {
            var collection = new ParameterCollection();

            collection["test"] = new object();

            Assert.AreEqual(1, collection.Count);
        }

        [Test]
        public void IndexerSet_KeyExists_ReplacesObject()
        {
            var collection = new ParameterCollection();

            collection.Add("test", new object());
            var i = 1;

            collection["test"] = i;

            Assert.AreEqual(i, collection["test"]);
        }
    }
}
