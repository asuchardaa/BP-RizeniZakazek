using NUnit.Framework;
using System.Diagnostics;
using BP_rizeni_zakazek.Helpers;

namespace BP_rizeni_zakazek.Tests
{
    [TestFixture]
    internal class DebugHelperTests
    {
        private DebugHelper _debugHelper;

        [SetUp]
        public void SetUp()
        {
            _debugHelper = new DebugHelper();
        }

        [Test]
        public void GetCallingMethodName_ReturnsCorrectMethodName()
        {
            string expectedMethodName = "TestMethod";
            string actualMethodName = TestMethod();

            Assert.That(actualMethodName, Is.EqualTo(expectedMethodName), "Metoda by mela sedet s volanym nazvem metody");
        }

        private string TestMethod()
        {
            return _debugHelper.GetCallingMethodName();
        }
    }

}