using NUnit.Framework;

namespace BP_rizeni_zakazek.NUnitTests
{
    public class Tests
    {
        private CSVManager _csvManager { get; set; } = null!;

        //private DataGridViewHelper _dataGridViewHelper = new DataGridViewHelper();
        //private OrderManager _orderManager = new OrderManager();

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