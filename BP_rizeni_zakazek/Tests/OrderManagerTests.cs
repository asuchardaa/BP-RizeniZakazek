using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BP_rizeni_zakazek.Managers;
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

        [SetUp]
        public void Setup()
        {
            _orderManager = new OrderManager();
            _dataGridView = new DataGridView();
            _dataGridView.Columns.Add("Date", "Date");
            _dataGridView.Columns.Add("dateOfFinish", "Dokončeno");
            _dataGridView.Columns.Add("StateOfOrder", "StateOfOrder");
            _dateColumnIndex = 0;

            _testRow = new DataGridViewRow();
            _testRow.CreateCells(_dataGridView);

        }

        [TearDown]
        public void Teardown()
        {
            _dataGridView.Rows.Clear();
        }


        private DataGridViewRow CreateTestRowWithData(List<string[]> detailsList)
        {
            int rowIndex = _dataGridView.Rows.Add();
            DataGridViewRow row = _dataGridView.Rows[rowIndex];
            row.Tag = detailsList;
            return row;
        }

        private DataGridViewRow CreateTestRowWithCells(int cellCount)
        {
            DataGridViewRow row = new DataGridViewRow();
            _dataGridView.Rows.Add(row);
            for (int i = 0; i < cellCount; i++)
            {
                if (i >= _dataGridView.Columns.Count)
                {
                    _dataGridView.Columns.Add($"Column{i}", $"Column{i}");
                }
            }
            return _dataGridView.Rows[_dataGridView.Rows.Count - 1];
        }


        [Test]
        [TestCase("Hotovo", "Green")]
        [TestCase("Rozpracováno", "Yellow")]
        [TestCase("Nezadáno", "LightBlue")]
        [TestCase("Více kusů", "Coral")]
        [TestCase("Neznámý status", "LightGray")] 
        public void UpdateColorStatusOrders_SetsCorrectColor(string status, string expectedColorName)
        {
            var row = CreateTestRowWithCells(6);
            _orderManager.UpdateColorStatusOrders(row, status);
            Color expectedColor = Color.FromName(expectedColorName);

            Assert.That(row.Cells[5].Style.BackColor, Is.EqualTo(expectedColor), $"Barva bunky je {expectedColorName} pro doonceny stav'{status}'.");
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
            Assert.That(status, Is.EqualTo("Špatně/Chyba"));

            status = _orderManager.DetermineOrderStatus("5", "10", "");
            Assert.That(status, Is.EqualTo("Špatně"));
        }

        [Test]
        public void DetermineOrderStatus_ReturnsDone_WhenNumCreatedEqualsNumOriginalAndCurveIsNo()
        {
            string numCreated = "10";
            string numOriginalAmount = "10";
            string curve = "NE";

            string status = _orderManager.DetermineOrderStatus(numCreated, numOriginalAmount, curve);

            Assert.That(status, Is.EqualTo("Hotovo"), "Stav 'Hotovo' kdyz sedi pocet vyrob. kusu a ohyb je 'NE'.");
        }

        [Test]
        public void DetermineOrderStatus_ReturnsMorePieces_WhenNumCreatedGreaterThanNumOriginal()
        {
            string numCreated = "15";
            string numOriginalAmount = "10";
            string curve = "NE";

            string status = _orderManager.DetermineOrderStatus(numCreated, numOriginalAmount, curve);

            Assert.That(status, Is.EqualTo("Více kusů"), "Stav 'Více kusů' kdyz vyrobenych ksuu je vic nez by melo.");
        }


        [Test]
        public void SetOrderCellColor_SetsRedForDefaultCase()
        {
            var cell = new DataGridViewTextBoxCell();
            string status = "Neexistující status";

            _orderManager.SetOrderCellColor(cell, status);

            Assert.That(cell.Style.BackColor, Is.EqualTo(Color.Red), "Defaultne cervena, ale stejne to je v designeru bily -> pro lepsi prhled, tadyby to aspon pak moho vyhazovat chybu nebo tak neco pri cervenym");
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
        public void GetColorForStatus_ReturnsCoral()
        {
            var status = "více kusů";
            var result = _orderManager.GetColorForStatus(status);
            Assert.That(result, Is.EqualTo(Color.Coral));
        }

        [Test]
        public void GetColorForStatus_Null_ReturnsWhite()
        {
            var status = "";
            var result = _orderManager.GetColorForStatus(status);
            Assert.That(result, Is.EqualTo(Color.White));
        }

        [Test]
        public void HighlightOverdueDates()
        {
            _testRow.Cells[_dateColumnIndex].Value = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
            _orderManager.HighlightOverdueDates(_testRow, _dateColumnIndex);
            var cellStyle = _testRow.Cells[_dateColumnIndex].Style.BackColor;
            Assert.That(cellStyle, Is.EqualTo(Color.Red));
        }

        [Test]
        public void CalculateOverallStatus_ReturnsMorePieces_WhenAllDetailsHaveMorePiecesStatus()
        {
            var detailsList = new List<string[]> { new[] { "", "", "", "", "", "", "", "", "", "Více kusů" } };
            var row = CreateTestRowWithData(detailsList);

            string status = _orderManager.CalculateOverallStatus(row, _dataGridView);

            Assert.That(status, Is.EqualTo("Více kusů"));
        }

        [Test]
        public void CalculateOverallStatus_ReturnsDone_WhenAllDetailsDone()
        {
            var detailsList = new List<string[]> { new[] { "", "", "", "", "", "", "", "", "", "Hotovo" } };
            var row = CreateTestRowWithData(detailsList);

            string status = _orderManager.CalculateOverallStatus(row, _dataGridView);

            Assert.That(status, Is.EqualTo("Hotovo"));
        }

        [Test]
        public void CalculateOverallStatus_ReturnsInProgress_WhenAnyDetailInProgress()
        {
            var detailsList = new List<string[]> { new[] { "", "", "", "", "", "", "", "", "", "Rozpracováno" } };
            var row = CreateTestRowWithData(detailsList);

            string status = _orderManager.CalculateOverallStatus(row, _dataGridView);

            Assert.That(status, Is.EqualTo("Rozpracováno"));
        }

        [Test]
        public void CalculateOverallStatus_ReturnsUnspecified_WhenNoDetailInProgressOrDone()
        {
            var detailsList = new List<string[]> { new[] { "", "", "", "", "", "", "", "", "", "Jiný stav" } };
            var row = CreateTestRowWithData(detailsList);

            string status = _orderManager.CalculateOverallStatus(row, _dataGridView);

            Assert.That(status, Is.EqualTo("Nezadáno"));
        }

        [Test]
        public void CalculateOverallStatus_ReturnsUnknown_WhenTagIsNotList()
        {
            DataGridViewRow row = new DataGridViewRow();
            row.Tag = null;

            string status = _orderManager.CalculateOverallStatus(row, _dataGridView);

            Assert.That(status, Is.EqualTo("Neznámý"));
        }

        [Test]
        public void DetermineOrderStatusList_ReturnsDone_WhenAllRowsDone()
        {
            var detailData = new List<string[]>
            {
                new[] {"", "", "", "", "", "", "", "", "", "Hotovo"},
                new[] {"", "", "", "", "", "", "", "", "", "Hotovo"}
            };

            string status = _orderManager.DetermineOrderStatusList(detailData);

            Assert.That(status, Is.EqualTo("Hotovo"));
        }

        [Test]
        public void DetermineOrderStatusList_ReturnsInProgress_WhenAnyRowInProgressOrDone()
        {
            var detailData = new List<string[]>
            {
                new[] {"", "", "", "", "", "", "", "", "", "Rozpracováno"},
                new[] {"", "", "", "", "", "", "", "", "", "Hotovo"}
            };

            string status = _orderManager.DetermineOrderStatusList(detailData);

            Assert.That(status, Is.EqualTo("Rozpracováno"));
        }

        [Test]
        public void DetermineOrderStatusList_ReturnsUnspecified_WhenNoRowInProgressOrDone()
        {
            var detailData = new List<string[]>
            {
                new[] {"", "", "", "", "", "", "", "", "", "Jiný stav"},
                new[] {"", "", "", "", "", "", "", "", "", "Jiný stav"}
            };

            string status = _orderManager.DetermineOrderStatusList(detailData);

            Assert.That(status, Is.EqualTo("Nezadáno"));
        }

        [Test]
        [TestCase("", "10", "NE", "Neznámý")]
        [TestCase("10", "", "NE", "Neznámý")]
        [TestCase("10", "10", "NE", "Hotovo")]
        [TestCase("10", "10", "ANO", "Rozpracováno")]
        [TestCase("5", "10", "NE", "Rozpracováno")]
        [TestCase("15", "10", "NE", "Více kusů")]
        [TestCase("deset", "10", "NE", "Neplatný status")]
        [TestCase("10", "deset", "NE", "Neplatný status")]
        public void DetermineTheNewStatus_ReturnsCorrectStatus(string created, string amount, string curve, string expectedStatus)
        {
            string status = _orderManager.DetermineTheNewStatus(created, amount, curve);

            Assert.That(status, Is.EqualTo(expectedStatus), $"Stav '{expectedStatus}' Vytvoreno '{created}', pocet'{amount}', ohyb '{curve}'.");
        }

        [Test]
        public void DetermineTheNewStatus_ReturnsInProgress_WhenCurveIsYes()
        {
            string created = "5";
            string amount = "10";
            string curve = "ANO";

            string status = _orderManager.DetermineTheNewStatus(created, amount, curve);

            Assert.That(status, Is.EqualTo("Rozpracováno"), "Stav 'Rozpracováno' s ohybem 'ANO'.");
        }

        [Test]
        public void DetermineTheNewStatus_ReturnsInProgress_WhenCreatedLessThanAmount()
        {
            string created = "5";
            string amount = "10";
            string curve = "NE";

            string status = _orderManager.DetermineTheNewStatus(created, amount, curve);

            Assert.That(status, Is.EqualTo("Rozpracováno"), "Stav 'Rozpracováno' kdyz je vyrobeno min kusu nez original kusy.");
        }

        [Test]
        public void DetermineTheNewStatus_ReturnsMorePieces_WhenCreatedMoreThanAmount()
        {
            string created = "15";
            string amount = "10";
            string curve = "NE";

            string status = _orderManager.DetermineTheNewStatus(created, amount, curve);

            Assert.That(status, Is.EqualTo("Více kusů"), "Stav 'Více kusů' kdyz je vyrobenych vice kusu nez original.");
        }
    }
}
