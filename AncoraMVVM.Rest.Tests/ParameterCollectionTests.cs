using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AncoraMVVM.Rest;
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
            var pars = new object[] { "1", 1, "2", 2, "3", 3, "4"};

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
    }
}
