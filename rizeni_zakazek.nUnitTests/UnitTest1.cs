using System.Runtime.CompilerServices;
using BP_rizeni_zakazek;
using NUnit.Framework;

namespace RizeniZakazek.nUnitTests
{

    public class Tests
    {
        private MainForm _mainForm { get; set; } = null!;

        [SetUp]
        public void Setup()
        {
            _mainForm = new MainForm();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}