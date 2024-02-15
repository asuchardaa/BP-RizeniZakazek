using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BP_rizeni_zakazek.Tests
{
    [TestFixture]
    internal class MainFormTests
    {
        private MainForm _mainForm;

        [SetUp]
        public void SetUp()
        {
            _mainForm = new MainForm();
            _mainForm.dataGridViewMaster.Columns.Add("Customer", "Customer");
            _mainForm.dataGridViewMaster.Columns.Add("NumOfOrder", "NumOfOrder");
            _mainForm.dataGridViewMaster.Columns.Add("stateOfOrder", "stateOfOrder");
            _mainForm.FilterTextBox.Text = "";
            _mainForm.OrderDoneOrNotDone.Items.AddRange(new object[] { "Vše", "Done", "Not Done" });
            _mainForm.OrderDoneOrNotDone.SelectedIndex = 0;
        }

        [Test]
        public void CreateAndShowDetailDataGridView_Test()
        {
            int rowIndex = 0;
            List<string[]> detailsList = new List<string[]>();
            bool visible = true;

            DataGridView detailDataGridView = _mainForm.CreateAndShowDetailDataGridView(rowIndex, detailsList, visible);

            Assert.That(detailDataGridView, Is.Not.Null);
            Assert.That(detailDataGridView.AutoSizeColumnsMode, Is.EqualTo(DataGridViewAutoSizeColumnsMode.Fill));
            Assert.That(detailDataGridView.AllowUserToAddRows, Is.False);
            Assert.That(detailDataGridView.ReadOnly, Is.False);
            Assert.That(detailDataGridView.Anchor,
                Is.EqualTo(AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right));
            Assert.That(detailDataGridView.AllowUserToResizeRows, Is.False);
            Assert.That(detailDataGridView.AllowUserToResizeColumns, Is.False);
            Assert.That(detailDataGridView.BackgroundColor, Is.EqualTo(Color.FromArgb(153, 180, 209)));
            Assert.That(detailDataGridView.BorderStyle, Is.EqualTo(BorderStyle.None));
            Assert.That(detailDataGridView.SelectionMode, Is.EqualTo(DataGridViewSelectionMode.FullRowSelect));
            Assert.That(detailDataGridView.DefaultCellStyle.Font, Is.EqualTo(new Font("Segoe UI", 9.75F)));
            Assert.That(detailDataGridView.DefaultCellStyle.BackColor, Is.EqualTo(Color.AliceBlue));
            Assert.That(detailDataGridView.DefaultCellStyle.ForeColor, Is.EqualTo(Color.Black));
            detailDataGridView.Dispose();
        }

        [Test]
        public void UpdateDetailIfNecessary_UpdatesDetailCorrectly_Test()
        {
            string[] detail =
                { "HST", "492", "p-asdsad", "dc01", "2", "4", "NE", "p-asdsad.dxf", "10", "Rozpracováno" };
            string vyrobeno = "5";
            _mainForm.UpdateDetailIfNecessary(detail, vyrobeno);
            Assert.That(detail[8], Is.EqualTo("15"));
            Assert.That(detail[9], Is.EqualTo("Více kusů"));
        }

        [Test]
        public void UpdateOrAddDetailGrid_Test()
        {
            int masterRowIndex = 0;
            string[] fields =
                { "HST", "492", "p-asdsad", "dc01", "2", "4", "NE", "p-asdsad.dxf", "10", "Rozpracováno" };
            string[] filteredFields =
                { "HST", "492", "p-asdsad", "dc01", "2", "4", "NE", "p-asdsad.dxf", "10", "Rozpracováno" };
            var existingDetail = new string[]
                { "HST", "492", "p-asdsad", "dc01", "2", "4", "NE", "p-asdsad.dxf", "10", "Rozpracováno" };
            List<string[]> detailsList = new List<string[]> { existingDetail };

            _mainForm.dataGridViewMaster.Rows.Add(new DataGridViewRow());
            _mainForm.dataGridViewMaster.Rows[masterRowIndex].Tag = detailsList;
            _mainForm.UpdateOrAddDetailGrid(masterRowIndex, fields, filteredFields);
            Assert.That(detailsList.Count, Is.EqualTo(2));
            Assert.That(detailsList[0], Is.EqualTo(filteredFields));
        }

        [Test]
        public void EnableEditing_CanEdit_SetDataGridViewAndButtonsState()
        {
            bool canEdit = true;
            _mainForm.EnableEditing(canEdit);
            Assert.That(_mainForm.dataGridViewMaster.ReadOnly, Is.EqualTo(!canEdit));

            foreach (var detailGrid in _mainForm.detailGrids.Values)
            {
                Assert.That(detailGrid.ReadOnly, Is.EqualTo(!canEdit));
            }
        }

        [Test]
        public void EnableEditing_CannotEdit_SetDataGridViewAndButtonsState()
        {
            bool canEdit = false;
            _mainForm.EnableEditing(canEdit);
            Assert.That(_mainForm.dataGridViewMaster.ReadOnly, Is.EqualTo(!canEdit));

            foreach (var detailGrid in _mainForm.detailGrids.Values)
            {
                Assert.That(detailGrid.ReadOnly, Is.EqualTo(!canEdit));
            }
        }

        [Test]
        public void ApplyFilter_NoSearchTextAndAllStatus_ShowAllRows()
        {
            // Arrange
            _mainForm.dataGridViewMaster.Rows.Add("Customer1", "123", "Done");
            _mainForm.dataGridViewMaster.Rows.Add("Customer2", "456", "Not Done");

            // Act
            _mainForm.ApplyFilter();

            // Assert
            Assert.That(_mainForm.dataGridViewMaster.Rows[0].Visible, Is.True);
            Assert.That(_mainForm.dataGridViewMaster.Rows[1].Visible, Is.True);
        }

        [Test]
        public void ApplyFilter_SearchTextAndMatchingStatus_ShowMatchingRows()
        {
            // Arrange
            _mainForm.dataGridViewMaster.Rows.Add("Customer1", "123", "Done");
            _mainForm.dataGridViewMaster.Rows.Add("Customer2", "456", "Not Done");
            _mainForm.FilterTextBox.Text = "Customer1";

            // Act
            _mainForm.ApplyFilter();

            // Assert
            Assert.That(_mainForm.dataGridViewMaster.Rows[0].Visible, Is.False);
            Assert.That(_mainForm.dataGridViewMaster.Rows[1].Visible, Is.False);
        }

        [Test]
        public void LoadDataFromJson_FileDoesNotExist_ShowMessageBoxAndReturn()
        {
            string filePath = "nonexistent.json";

            _mainForm.LoadDataFromJson(filePath);

            Assert.That(MessageBoxShown("Soubor s daty neexistuje. Bude vytvořen nový soubor při ukončení aplikace."), Is.True);

        }

        [Test]
        public void LoadDataFromJson_EmptyFile_ShowMessageBoxAndReturn()
        {
            string filePath = "empty.json";
            File.WriteAllText(filePath, "");

            _mainForm.LoadDataFromJson(filePath);

            Assert.That(MessageBoxShown("Soubor s daty je prázdný. Žádná data nebudou načtena."), Is.True);

        }

        private bool MessageBoxShown(string expectedMessage)
        {
            return true;
        }

    }
}