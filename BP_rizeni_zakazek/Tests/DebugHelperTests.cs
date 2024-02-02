using NUnit.Framework;
using System.Diagnostics;
using BP_rizeni_zakazek.Helpers;

namespace BP_rizeni_zakazek.Tests
{
    [TestFixture]
    internal class DebugHelperTests
    {

        private DebugHelper _debugHelper;

        [Test]
        public void GetCallingMethodName_ReturnsCorrectMethodName()
        {
            string expectedMethodName = "TestMethod";
            string actualMethodName = TestMethod();

            Assert.That(actualMethodName, Is.EqualTo(expectedMethodName), "The calling method name should match the test method name.");
        }

        private string TestMethod()
        {
            return _debugHelper.GetCallingMethodName();
        }
    }
}