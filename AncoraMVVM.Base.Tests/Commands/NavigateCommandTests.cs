using AncoraMVVM.Base.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace AncoraMVVM.Base.Tests
{
    [TestClass]
    public class NavigateCommandTests
    {
        [TestMethod]
        public void Execute_NavigatesToTarget()
        {
            var nav = Substitute.For<INavigationService>();
            string page = "test";
            var command = new NavigateCommand(nav, page);

            command.Execute(null);

            nav.Received().Navigate(page);
        }

        [TestMethod]
        public void CanExecute_AlwaysTrue()
        {
            var nav = Substitute.For<INavigationService>();
            string page = "test";
            var command = new NavigateCommand(nav, page);

            Assert.IsTrue(command.CanExecute(null));
        }
    }
}
