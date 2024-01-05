namespace RizeniZakazek.NUnitTests
{
    using NUnit.Framework;


    public class Tests
    {
        private MainForm _mainForm { get; set; } = null!;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}