using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AncoraMVVM.Base.IoC.Test
{
    [TestClass()]
    public class DependencyTests
    {
        private interface IDummy
        {
        }

        private class Dummy : IDummy
        {
        }

        private class Dummy2 : IDummy
        {
        }

        private class DummyDependencyModule : DependencyModule
        {
            public DummyDependencyModule()
            {
                AddDep<IDummy, Dummy>();
            }
        }

        private class DummyProvider : IObjectProvider
        {
            public T Resolve<T>() where T : class
            {
                return new Dummy2() as T;
            }
        }

        [TestInitialize]
        public void Init()
        {
            Dependency.Clear();
            Dependency.Provider = null;
        }

        [TestMethod()]
        public void Register_GenericArguments_Ok()
        {
            Dependency.Register<IDummy, Dummy>();
            Assert.IsTrue(Dependency.Resolve<IDummy>() is IDummy);
        }

        [TestMethod()]
        public void Register_Types_Ok()
        {
            Dependency.Register(typeof(IDummy), typeof(Dummy));
            Assert.IsTrue(Dependency.Resolve<IDummy>() is IDummy);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_Duplicated_ThrowsException()
        {
            Dependency.Register<IDummy, Dummy>();
            Dependency.Register<IDummy, Dummy>();
        }

        [TestMethod]
        public void Register_Singleton_SameInstance()
        {
            Dependency.Register<IDummy, Dummy>(true);
            var resolve1 = Dependency.Resolve<IDummy>();
            var resolve2 = Dependency.Resolve<IDummy>();
            Assert.AreSame(resolve1, resolve2);
        }

        [TestMethod()]
        public void RegisterModule()
        {
            Dependency.RegisterModule(new DummyDependencyModule());
            Assert.IsTrue(Dependency.Resolve<IDummy>() is IDummy);
        }

        [TestMethod]
        public void Provider_ReturnsObject()
        {
            Dependency.Provider = new DummyProvider();
            var obj = Dependency.Resolve<IDummy>();

            Assert.IsTrue(obj is IDummy);
        }

        [TestMethod]
        public void Provider_OverridesSettings()
        {
            Dependency.Provider = new DummyProvider();
            Dependency.Register<IDummy, Dummy>();

            var obj = Dependency.Resolve<IDummy>();

            Assert.IsTrue(obj is Dummy2); // DummyProvider returns Dummy2 objects.
        }
    }
}
