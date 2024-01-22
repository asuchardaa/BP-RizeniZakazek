using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BP_rizeni_zakazek.utils;
using NUnit.Framework;

namespace BP_rizeni_zakazek.Tests
{
    [TestFixture]

    internal class OrderManagerTests
    {
        private OrderManager _orderManager;
        private DataGridView _dataGridView;
        private DataGridViewRow _testRow;
        private int _dateColumnIndex;
        private int _dateOfFinishColumnIndex;

        [SetUp]
        public void Setup()
        {
            _orderManager = new OrderManager();
            _dataGridView = new DataGridView();
            _dataGridView.Columns.Add("Date", "Date");
            _dataGridView.Columns.Add("dateOfFinish", "Dokončeno");
            _dateOfFinishColumnIndex = 1;
            _dateColumnIndex = 0;

            _testRow = new DataGridViewRow();
            _testRow.CreateCells(_dataGridView);

        }

        [Test]
        public void DetermineOrderStatus_VraciHotovo_KdyzVyrobenoRovnaOriginalPocetAOhybNe()
        {
            string status = _orderManager.DetermineOrderStatus("10", "10", "NE");
            Assert.That(status, Is.EqualTo("Hotovo"));
        }

        [Test]
        public void DetermineOrderStatus_VraciRozpracovano_KdyzVyrobenoNeniRovnoOriginalPocet()
        {
            string status = _orderManager.DetermineOrderStatus("5", "10", "NE");
            Assert.That(status, Is.EqualTo("Rozpracováno"));
        }

        [Test]
        public void DetermineOrderStatus_VraciRozpracovano_KdyzOhybAno()
        {
            string status = _orderManager.DetermineOrderStatus("10", "10", "ANO");
            Assert.That(status, Is.EqualTo("Rozpracováno"));
        }

        [Test]
        public void DetermineOrderStatus_VraciNehotovo_KdyzVyrobenoJeNula()
        {
            string status = _orderManager.DetermineOrderStatus("0", "10", "NE");
            Assert.That(status, Is.EqualTo("Nehotovo"));
        }

        [Test]
        public void DetermineOrderStatus_VraciChybuPrev_KdyzVyrobenoNeboOriginalPocetNeniCislo()
        {
            string status = _orderManager.DetermineOrderStatus("neniCislo", "10", "NE");
            Assert.That(status, Is.EqualTo("chyba přev"));

            status = _orderManager.DetermineOrderStatus("10", "neniCislo", "NE");
            Assert.That(status, Is.EqualTo("chyba přev"));
        }

        [Test]
        public void DetermineOrderStatus_VraciSpatne_ProOstatniPripady()
        {
            string status = _orderManager.DetermineOrderStatus("10", "10", "NeplatnaHodnota");
            Assert.That(status, Is.EqualTo("Špatně"));

            status = _orderManager.DetermineOrderStatus("5", "10", "");
            Assert.That(status, Is.EqualTo("Špatně"));
        }

        [Test]
        public void GetColorForStatus_Unknown_ReturnsLightSlateGray()
        {
            var status = "neznámý";
            var result = _orderManager.GetColorForStatus(status);
            Assert.That(result, Is.EqualTo(Color.LightSlateGray));
        }

        [Test]
        public void GetColorForStatus_InProgress_ReturnsYellow()
        {
            var status = "rozpracováno";
            var result = _orderManager.GetColorForStatus(status);
            Assert.That(result, Is.EqualTo(Color.Yellow));
        }

        [Test]
        public void GetColorForStatus_Done_ReturnsGreen()
        {
            var status = "hotovo";
            var result = _orderManager.GetColorForStatus(status);
            Assert.That(result, Is.EqualTo(Color.Green));
        }

        [Test]
        public void GetColorForStatus_Other_ReturnsWhite()
        {
            var status = "jakýkoliv jiný stav";
            var result = _orderManager.GetColorForStatus(status);
            Assert.That(result, Is.EqualTo(Color.White));
        }

        [Test]
        public void GetColorForStatus_CaseInsensitive()
        {
            var status = "HoTOvo";
            var result = _orderManager.GetColorForStatus(status);
            Assert.That(result, Is.EqualTo(Color.Green));
        }

        [Test]
        public void HighlightOverdueDates_NastaviBarvuNaCervenou_KdyzDatumJeVMinulosti()
        {
            _testRow.Cells[_dateColumnIndex].Value = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
            _orderManager.HighlightOverdueDates(_testRow, _dateColumnIndex);
            var cellStyle = _testRow.Cells[_dateColumnIndex].Style.BackColor;
            Assert.That(cellStyle, Is.EqualTo(Color.Red));
        }
    }
}
