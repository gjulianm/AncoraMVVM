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

        [TestInitialize]
        public void Init()
        {
            Dependency.Clear();
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

        private class DummyDependencyModule : DependencyModule
        {
            public DummyDependencyModule()
            {
                AddDep<IDummy, Dummy>();
            }
        }

        [TestMethod()]
        public void RegisterModule()
        {
            Dependency.RegisterModule(new DummyDependencyModule());
            Assert.IsTrue(Dependency.Resolve<IDummy>() is IDummy);
        }
    }
}
