using BP_rizeni_zakazek.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP_rizeni_zakazek.Tests
{
    [TestFixture]
    internal class DataGridHelperTests
    {
        private DataGridHelper _dataGridHelper;
        private DataGridView _dataGridView;
        private DataGridView _detailGrid;


        [SetUp]
        public void Setup()
        {
            _dataGridHelper = new DataGridHelper();
            _dataGridView = new DataGridView();

            _dataGridView.Columns.Add("Customer", "Customer");
            _dataGridView.Columns.Add("NumOfOrder", "NumOfOrder");
            _dataGridView.Columns.Add("stateOfOrder", "stateOfOrder");
            _dataGridView.Columns.Add("Date", "Date");

            _dataGridView.Rows.Add("Customer1", "123", "Neznámý", "01/01/2023");
            _dataGridView.Rows.Add("Customer2", "456", "Neznámý", "01/02/2023");

            _detailGrid = new DataGridView();
            _detailGrid.Columns.Add("DetailColumn1", "DetailColumn1");
            _detailGrid.Columns.Add("DetailColumn2", "DetailColumn2");
        }

        [Test]
        public void DeleteSpecifiedRow_RemovesCorrectRow()
        {
            string orderNumber = "456";

            _dataGridHelper.DeleteSpecifiedRow(_dataGridView, orderNumber);

            bool rowExists = _dataGridView.Rows
                .Cast<DataGridViewRow>()
                .Any(row => !row.IsNewRow && row.Cells["NumOfOrder"].Value.ToString() == orderNumber);

            Assert.That(rowExists, Is.False, "Radek s timto cislem zakazky by mel byt odstranen.");
        }

        [Test]
        public void UpdateAllMasterGridRowStatuses_UpdatesStatusForAllRows()
        {
            foreach (DataGridViewRow row in _dataGridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.Cells["stateOfOrder"].Value = "Neznámý";
                }
            }

            _dataGridHelper.UpdateAllMasterGridRowStatuses(_dataGridView);

            foreach (DataGridViewRow row in _dataGridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    Assert.That(row.Cells["stateOfOrder"].Value.ToString(), Is.EqualTo("Neznámý"), "Status by se mel zmenit na dany ExpectedStatus.");
                }
            }
        }

        [Test]
        public void FindOrAddMasterRow_FindsExistingRow()
        {
            string[] fields = new string[] { "Customer1", "123", "", "", "", "", "", "", "01/01/2023" };
            int rowIndex = _dataGridHelper.FindOrAddMasterRow(_dataGridView, fields);

            Assert.That(rowIndex, Is.EqualTo(0), "Melo by to najit rade na indexu 0.");
        }

        [Test]
        public void FindOrAddMasterRow_AddsNewRowWhenNotExisting()
        {
            string[] fields = new string[] { "Customer3", "789", "", "", "", "", "", "", "01/03/2023" };
            int expectedIndex = _dataGridView.Rows.Count;
            int rowIndex = _dataGridHelper.FindOrAddMasterRow(_dataGridView, fields);

            Assert.That(rowIndex, Is.EqualTo(expectedIndex - 1), "Melo by pridat novej radek na konec (posledni index).");
        }

        [Test]
        public void FindOrAddMasterRow_NewRowHasCorrectData()
        {
            string[] fields = new string[] { "Customer3", "789", "", "", "", "", "", "", "01/03/2023" };
            int rowIndex = _dataGridHelper.FindOrAddMasterRow(_dataGridView, fields);

            Assert.That(_dataGridView.Rows[rowIndex].Cells["Customer"].Value?.ToString(), Is.EqualTo(fields[0].Trim()), "CustomerCol");
            Assert.That(_dataGridView.Rows[rowIndex].Cells["NumOfOrder"].Value?.ToString(), Is.EqualTo(fields[1].Trim()), "NumOfOrderCol");
            Assert.That(_dataGridView.Rows[rowIndex].Cells["Date"].Value?.ToString(), Is.EqualTo(fields[8].Trim()), "DateCol");
        }

        [Test]
        public void UpdateDetailGrid_ClearsExistingRows()
        {
            _detailGrid.Rows.Add("ExistingData1", "ExistingData2");
            var detailsList = new List<string[]> { new string[] { "1", "2", "DetailData1", "DetailData2" } };

            _dataGridHelper.UpdateDetailGrid(_detailGrid, detailsList);

            Assert.That(_detailGrid.Rows.Count, Is.EqualTo(2), "DetailGrid radky jen podle tyhle metody");
        }

        [Test]
        public void UpdateDetailGrid_AddsNewRows()
        {
            var detailsList = new List<string[]> {
                new string[] { "1", "2", "DetailData1", "DetailData2" },
                new string[] { "3", "4", "DetailData3", "DetailData4" }
            };

            _dataGridHelper.UpdateDetailGrid(_detailGrid, detailsList);

            Assert.That(_detailGrid.Rows.Count, Is.EqualTo(3), "DetailGrid");
        }

        [Test]
        public void UpdateDetailGrid_MapsDataCorrectly()
        {
            var detailsList = new List<string[]> {
                new string[] { "1", "2", "DetailData1", "DetailData2" }
            };

            _dataGridHelper.UpdateDetailGrid(_detailGrid, detailsList);

            for (int i = 0; i < _detailGrid.Columns.Count; i++)
            {
                Assert.That(_detailGrid.Rows[0].Cells[i].Value?.ToString(), Is.EqualTo(detailsList[0][i + 2]), $"Data ve sloupci {i} bxy mely sedet s listem");
            }
        }
    }
}

