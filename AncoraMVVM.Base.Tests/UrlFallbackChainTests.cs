using NUnit.Framework;

namespace AncoraMVVM.Base.Tests
{
    [TestFixture]
    public class UrlFallbackChainTests
    {
        // TODO: More tests.

        [Test]
        public void Condition_ValidUrl_True()
        {
            var chain = new UrlFallbackChain("", "");
            Assert.IsTrue(chain.Condition("http://www.google.es"));
        }

        [Test]
        public void Condition_InvalidUrl_False()
        {
            var chain = new UrlFallbackChain("", "");
            Assert.IsFalse(chain.Condition("aswwes"));
        }

        [Test]
        public void Condition_NullUrl_False()
        {
            var chain = new UrlFallbackChain("", "");
            Assert.IsFalse(chain.Condition(null));
        }
    }
}
