using AncoraMVVM.Base.Collections;
using AncoraMVVM.Base.IoC;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AncoraMVVM.Base.Tests.Collections
{
    [TestFixture]
    public class SortedFilteredObservableTests
    {
        [TestFixtureSetUp]
        public static void Setup()
        {
            Dependency.Provider = new MockProvider();
        }

        [TestFixtureTearDown]
        public static void Teardown()
        {
            Dependency.Provider = null;
        }

        private SortedFilteredObservable<int> CreateRange(int items = 10)
        {
            var list = new SortedFilteredObservable<int>();

            for (int i = 0; i < items; i++)
                list.Add(i);
            return list;
        }

        private SortedFilteredObservable<int> CreateRandomRange(int items = 10)
        {
            var list = new SortedFilteredObservable<int>();
            var random = new Random();

            for (int i = 0; i < items; i++)
                list.Add(random.Next(items + 10));

            return list;
        }

        [Test]
        public void Filter_Null_NoRemovedElements()
        {
            var list = CreateRange(10);
            list.Discarder = null;

            Assert.AreEqual(10, list.Count);
        }

        [Test]
        public void Filter_Set_IsApplied()
        {
            var list = CreateRange(10);
            list.Discarder = x => x % 2 == 0;

            foreach (var item in list)
                Assert.IsTrue(item % 2 != 0);
        }

        [Test]
        public void Add_ItemMatchingFilter_NotAddedToList()
        {
            var list = CreateRange(10);
            list.Discarder = x => x % 2 == 0;

            list.Add(12);

            Assert.AreEqual(5, list.Count);
        }

        [Test]
        public void Add_ItemNotMatchingFilter_AddedToList()
        {
            var list = CreateRange(10);
            list.Discarder = x => x % 2 == 0;

            list.Add(11);

            Assert.AreEqual(6, list.Count);
        }

        [Test]
        public void Filter_Changes_IsReapplied()
        {
            var list = CreateRange(10);
            list.Discarder = x => x < 8;

            Assert.AreEqual(2, list.Count);

            list.Discarder = x => x > 2;

            foreach (var item in list)
                Assert.IsTrue(item <= 2);
        }

        [Test]
        public void Comparer_SortsListCorrectly()
        {
            var list = CreateRandomRange(10);

            list.Comparer = Comparer<int>.Default;

            for (int i = 0; i < list.Count - 1; i++)
                Assert.LessOrEqual(list[i], list[i + 1]);
        }

        [Test]
        public void SortOrder_Descending_OrderIsInverted()
        {
            var list = CreateRandomRange(10);

            list.Comparer = Comparer<int>.Default;
            list.SortOrder = SortOrder.Descending;

            for (int i = 0; i < list.Count - 1; i++)
                Assert.GreaterOrEqual(list[i], list[i + 1]);
        }
    }
}
