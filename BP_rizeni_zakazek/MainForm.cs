using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using BP_rizeni_zakazek.utils;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BP_rizeni_zakazek

{
    public partial class MainForm : Form
    {
        [DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        private CSVManager _csvManager = new CSVManager();
        private DataGridViewHelper _dataGridViewHelper = new DataGridViewHelper();
        private OrderManager _orderManager = new OrderManager();

        private Dictionary<int, DataGridView> detailGrids = new Dictionary<int, DataGridView>();


        public MainForm()
        {
            InitializeComponent();
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            NavPnl.Height = BtnDashboard.Height;
            NavPnl.Top = BtnDashboard.Top;
            NavPnl.Left = BtnDashboard.Left;
            BtnDashboard.BackColor = System.Drawing.Color.FromArgb(46, 51, 73);
            dataGridViewMaster.CellContentClick +=
                new DataGridViewCellEventHandler(dataGridViewMaster_CellContentClick);
            InitializeDataGridViewMaster();
            //this.Load += new System.EventHandler(this.Form1_Load);
            //settingsPanel.Visible = false;
            dataGridViewMaster.CellEndEdit += new DataGridViewCellEventHandler(detailGrid_CellEndEdit);
        }

        /// <summary>
        /// Metoda pro defaultní nastavení Formuláře, tohle nastavuje parametry do filtru
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            OrderDoneOrNotDone.Items.Clear();
            OrderDoneOrNotDone.Items.Add("Vše");
            OrderDoneOrNotDone.Items.Add("Hotovo");
            OrderDoneOrNotDone.Items.Add("Rozpracováno");
            OrderDoneOrNotDone.SelectedIndex = 0;
            //LoadDataFromXlsx(ExcelFilePath);
        }

        /// <summary>
        /// Metoda pro reset barvy tlačítek v navigačním panelu
        /// </summary>
        private void ResetButtonColors()
        {
            BtnDashboard.BackColor = System.Drawing.Color.FromArgb(24, 30, 54);
            BtnStatistics.BackColor = System.Drawing.Color.FromArgb(24, 30, 54);
            BtnCalender.BackColor = System.Drawing.Color.FromArgb(24, 30, 54);
            BtnArchive.BackColor = System.Drawing.Color.FromArgb(24, 30, 54);
            BtnSettings.BackColor = System.Drawing.Color.FromArgb(24, 30, 54);
        }

        /// <summary>
        /// Metoda pro zobrazení efektu při najetí na tlačítko Dashboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            NavPnl.Height = BtnDashboard.Height;
            NavPnl.Top = BtnDashboard.Top;
            NavPnl.Left = BtnDashboard.Left;
            BtnDashboard.BackColor = System.Drawing.Color.FromArgb(46, 51, 73);
        }

        /// <summary>
        /// Metoda pro zobrazení efektu při najetí na tlačítko Statistics
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStatistics_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            NavPnl.Height = BtnStatistics.Height;
            NavPnl.Top = BtnStatistics.Top;
            NavPnl.Left = BtnStatistics.Left;
            BtnStatistics.BackColor = System.Drawing.Color.FromArgb(46, 51, 73);
        }

        /// <summary>
        /// Metoda pro zobrazení efektu při najetí na tlačítko Calender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCalender_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            NavPnl.Height = BtnCalender.Height;
            NavPnl.Top = BtnCalender.Top;
            NavPnl.Left = BtnCalender.Left;
            BtnCalender.BackColor = System.Drawing.Color.FromArgb(46, 51, 73);
        }

        /// <summary>
        /// Metoda pro zobrazení efektu při najetí na tlačítko Archive
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnArchive_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            NavPnl.Height = BtnArchive.Height;
            NavPnl.Top = BtnArchive.Top;
            NavPnl.Left = BtnArchive.Left;
            BtnArchive.BackColor = System.Drawing.Color.FromArgb(46, 51, 73);
        }

        /// <summary>
        /// Metoda pro nastavení efektu při najetí na tlačítko Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSettings_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            NavPnl.Height = BtnSettings.Height;
            NavPnl.Top = BtnSettings.Top;
            NavPnl.Left = BtnSettings.Left;
            BtnSettings.BackColor = System.Drawing.Color.FromArgb(46, 51, 73);
        }

        /// <summary>
        /// Metoda pro zobrazení efektu při najetí na tlačítko Dashboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDashboard_Leave(object sender, EventArgs e)
        {
            BtnDashboard.BackColor = System.Drawing.Color.FromArgb(24, 30, 54);
        }

        /// <summary>
        /// Metoda pro zobrazení efektu při odchodu z tlačítka Statistics
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStatistics_Leave(object sender, EventArgs e)
        {
            BtnStatistics.BackColor = System.Drawing.Color.FromArgb(24, 30, 54);
        }

        /// <summary>
        /// Metoda pro zobrazení efektu při odchodu z tlačítka Calender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCalender_Leave(object sender, EventArgs e)
        {
            BtnCalender.BackColor = System.Drawing.Color.FromArgb(24, 30, 54);
        }

        /// <summary>
        /// Metoda pro zobrazení efektu při odchodu z tlačítka Archive
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnArchive_Leave(object sender, EventArgs e)
        {
            BtnArchive.BackColor = System.Drawing.Color.FromArgb(24, 30, 54);
        }

        /// <summary>
        /// Metoda pro zobrazení efektu při odchodu z tlačítka Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSettings_Leave(object sender, EventArgs e)
        {
            BtnSettings.BackColor = System.Drawing.Color.FromArgb(24, 30, 54);
        }

        /// <summary>
        /// Metoda pro aplikaci filtru na textBox pro vyhledávání zákazníka a čísla zakázky
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterTextBox_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        /// <summary>
        /// Metoda pro aplikaci filtru na comboBox s hotovo/rozpracováno/vše
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_OrderDoneOrNotDone(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        /// <summary>
        /// Metoda pro aplikaci filtru na masterGrid
        /// </summary>
        private void ApplyFilter()
        {
            string searchText = FilterTextBox.Text.ToLower();
            string selectedStatus = OrderDoneOrNotDone.SelectedItem.ToString();

            foreach (DataGridViewRow row in dataGridViewMaster.Rows)
            {
                bool textMatches = row.Cells["Customer"].Value.ToString().ToLower().Contains(searchText) ||
                                   row.Cells["NumOfOrder"].Value.ToString().ToLower().Contains(searchText);
                bool statusMatches;

                if (selectedStatus == "Vše")
                {
                    statusMatches = true;
                }
                else
                {
                    statusMatches = row.Cells["stateOfOrder"].Value.ToString()
                        .Equals(selectedStatus, StringComparison.OrdinalIgnoreCase);
                }

                row.Visible = textMatches && statusMatches;
            }
        }

        /// <summary>
        /// Metoda pro inicializaci masterGridu
        /// </summary>
        private void InitializeDataGridViewMaster()
        {
            if (!dataGridViewMaster.Columns.Contains("ExpandDetails"))
            {
                DataGridViewButtonColumn expandColumn = new DataGridViewButtonColumn();
                expandColumn.HeaderText = "";
                expandColumn.Name = "ExpandDetails";
                // Removed the Text property assignment
                expandColumn.UseColumnTextForButtonValue = false;
                expandColumn.Width = 40;
                dataGridViewMaster.Columns.Insert(0, expandColumn);
            }

            // Přidání sloupce pro vymazání
            if (!dataGridViewMaster.Columns.Contains("DeleteColumn"))
            {
                var deleteColumn = new DataGridViewButtonColumn
                {
                    HeaderText = "Vymazat",
                    Name = "DeleteColumn",
                    Text = "Vymazat",
                    UseColumnTextForButtonValue = true
                };
                dataGridViewMaster.Columns.Add(deleteColumn);
            }


            dataGridViewMaster.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dataGridViewMaster.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewMaster.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dataGridViewMaster.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sans-Serif", 13);
            dataGridViewMaster.EnableHeadersVisualStyles = false;
            dataGridViewMaster.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 10);
            dataGridViewMaster.RowTemplate.Height = 30;
            dataGridViewMaster.ColumnHeadersHeight = 250;

            dataGridViewMaster.ScrollBars = ScrollBars.Both;
            dataGridViewMaster.GridColor = System.Drawing.Color.DarkGray;
            dataGridViewMaster.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewMaster.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewMaster.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewMaster.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        /// Metoda pro nahrání vstupního CSV a vytvoření řádků v masterGridu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\Users\\Adam\\Documents\\TUL\\SZZ\\BP\\importZak";
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;


                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var filePath = openFileDialog.FileName;

                    // Kontrola duplicity
                    if (_csvManager.JeSouborJizNacten(filePath))
                    {
                        DialogResult dialogResult = MessageBox.Show(
                            "Tento soubor již byl nahrán. Chcete jej nahrát znovu a přepsat existující data?",
                            "Duplicitní soubor", MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.Yes)
                        {
                            string cisloObjednavky = _csvManager.NajitCisloObjednavkyCSV(filePath);
                            if (cisloObjednavky != null)
                            {
                                _dataGridViewHelper.DeleteSpecifiedRow(dataGridViewMaster, cisloObjednavky);
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        _csvManager.PridatNactenySoubor(filePath);
                        //nacteneSoubory.Add(filePath);
                    }

                    string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

                    if (lines.Length > 1)
                    {
                        foreach (string line in lines.Skip(1)) // Skip the header
                        {
                            string[] fields = line.Split(';');
                            var filteredFields = fields.Where((field, index) => index != 6 && index != 8).ToArray();
                            int rowIndex;

                            if (!_dataGridViewHelper.RowExists(dataGridViewMaster, fields[0], fields[1], fields[8]))
                            {
                                rowIndex = dataGridViewMaster.Rows.Add();
                                dataGridViewMaster.Rows[rowIndex].Cells["Customer"].Value = fields[0].Trim();
                                dataGridViewMaster.Rows[rowIndex].Cells["NumOfOrder"].Value = fields[1].Trim();
                                dataGridViewMaster.Rows[rowIndex].Cells["Date"].Value = fields[8].Trim();
                                dataGridViewMaster.Rows[rowIndex].Tag = new List<string[]> { filteredFields };

                                // inicializace, ale zatím nechávám skrytý
                                CreateAndShowDetailDataGridView(rowIndex,
                                    (List<string[]>)dataGridViewMaster.Rows[rowIndex].Tag, false);
                            }
                            else
                            {
                                rowIndex = _dataGridViewHelper.AddDetailsToExistingRow(dataGridViewMaster, fields[0],
                                    fields[1], fields[8], filteredFields);
                                if (rowIndex >= 0 && detailGrids.ContainsKey(rowIndex))
                                {
                                    var detailsList = (List<string[]>)dataGridViewMaster.Rows[rowIndex].Tag;
                                    CreateAndShowDetailDataGridView(rowIndex, detailsList,
                                        detailGrids[rowIndex].Visible);
                                }
                            }
                        }
                    }

                    _dataGridViewHelper.UpdateAllMasterGridRowStatuses(dataGridViewMaster);
                    ExportDataToXlsx(dataGridViewMaster, detailGrids);
                }
            }
        }

        /// <summary>
        /// Metoda pro rozbalení DetailGridu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewMaster_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ochrana proti kliknutí na hlavičku -> kdyžtak smazat v případě potřeby organizace
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.RowIndex >= 0 && dataGridViewMaster.Columns[e.ColumnIndex].Name == "ExpandDetails")
            {
                var buttonCell = dataGridViewMaster.Rows[e.RowIndex].Cells["ExpandDetails"];
                bool isDetailVisible = detailGrids.ContainsKey(e.RowIndex) && detailGrids[e.RowIndex].Visible;

                if (isDetailVisible)
                {
                    DisposeDetailDataGridView(e.RowIndex);
                    buttonCell.Value = "+";
                }
                else
                {
                    var detailsList = dataGridViewMaster.Rows[e.RowIndex].Tag as List<string[]>;
                    if (detailsList != null)
                    {
                        CreateAndShowDetailDataGridView(e.RowIndex, detailsList, visible: true);
                        buttonCell.Value = "-";
                    }
                }

                dataGridViewMaster.InvalidateRow(e.RowIndex);
            }

            if (dataGridViewMaster.Columns[e.ColumnIndex].Name == "EditColumn")
            {
                BeginEditRow(e.RowIndex);
            }

            else if (dataGridViewMaster.Columns[e.ColumnIndex].Name == "DeleteColumn")
            {
                DialogResult result = MessageBox.Show("Opravdu chcete smazat tento řádek?", "Potvrzení smazání",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    dataGridViewMaster.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void BeginEditRow(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < dataGridViewMaster.Rows.Count)
            {
                DataGridViewRow row = dataGridViewMaster.Rows[rowIndex];

                // Povolit editaci pro všechny buňky v řádku
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.ReadOnly = false;
                }

                // Vyberte první buňku v řádku a začněte editaci
                dataGridViewMaster.CurrentCell = row.Cells[0];
                dataGridViewMaster.BeginEdit(true);
            }
        }

        /// <summary>
        /// Metoda pro vytvoření DetailGridu
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="detailsList"></param>
        /// <param name="visible"></param>
        private void CreateAndShowDetailDataGridView(int rowIndex, List<string[]> detailsList, bool visible)
        {
            if (detailGrids.TryGetValue(rowIndex, out var existingDetailGrid))
            {
                existingDetailGrid.Dispose();
                detailGrids.Remove(rowIndex);
            }

            var detailDataGridView = new DataGridView
            {
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = false,
                BackgroundColor = System.Drawing.Color.White,
                BorderStyle = BorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new System.Drawing.Font("Segoe UI", 9.75F),
                    BackColor = System.Drawing.Color.AliceBlue,
                    ForeColor = System.Drawing.Color.Black,
                    // V případě potřeby organizace, Selection BC a FC odkomentovat pro zobrazení barvy výběru
                    // SelectionBackColor = Color.AliceBlue,
                    // SelectionForeColor = Color.Black
                },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new System.Drawing.Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                    BackColor = System.Drawing.Color.CornflowerBlue,
                    ForeColor = System.Drawing.Color.White
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = System.Drawing.Color.LightGray
                },
                GridColor = System.Drawing.Color.DarkGray,
                RowHeadersVisible = false
            };

            detailDataGridView.RowTemplate.Height = 30;
            detailDataGridView.ScrollBars = ScrollBars.Both;
            detailDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            detailDataGridView.CellEndEdit += new DataGridViewCellEventHandler(detailGrid_CellEndEdit);
            detailDataGridView.Tag = rowIndex;

            detailDataGridView.Width = dataGridViewMaster.Width;

            detailDataGridView.Columns.Add("nazev", "Název");
            detailDataGridView.Columns.Add("material", "Materiál");
            detailDataGridView.Columns.Add("tloustka", "Tloušťka");
            detailDataGridView.Columns.Add("pocet", "Počet");
            detailDataGridView.Columns.Add("ohyb", "Ohyb");
            detailDataGridView.Columns.Add("cestaKSouboru", "Cesta");
            detailDataGridView.Columns.Add("vyrobeno", "Vyrobeno");
            detailDataGridView.Columns.Add("stavObjednavky", "Stav");

            detailDataGridView.Columns["stavObjednavky"].DefaultCellStyle.BackColor = System.Drawing.Color.White;

            detailDataGridView.CellFormatting +=
                new DataGridViewCellFormattingEventHandler(DetailDataGridView_CellFormatting);
            detailDataGridView.Visible = visible;
            detailGrids[rowIndex] = detailDataGridView;

            foreach (var detail in detailsList)
            {
                //detailDataGridView.Rows.Add(detail.Skip(2).Take(detailDataGridView.Columns.Count).ToArray());

                int detailIndex =
                    detailDataGridView.Rows.Add(detail.Skip(2).Take(detailDataGridView.Columns.Count).ToArray());
                string stavObjednavky = string.IsNullOrEmpty(detail[9]) ? "Neznámý" : detail[9];
                detailDataGridView.Rows[detailIndex].Cells["stavObjednavky"].Value = stavObjednavky;
                dataGridViewMaster.Rows[rowIndex].Cells["ExpandDetails"].Value = "+";
            }

            detailDataGridView.Height = (detailDataGridView.Rows.Count * detailDataGridView.RowTemplate.Height) +
                                        detailDataGridView.ColumnHeadersHeight;

            int currentY = dataGridViewMaster.Location.Y + dataGridViewMaster.ColumnHeadersHeight;
            for (int i = 0; i <= rowIndex; i++)
            {
                currentY += dataGridViewMaster.Rows[i].Height;
            }

            detailDataGridView.Location = new Point(dataGridViewMaster.Location.X, currentY);
            detailGrids[rowIndex] = detailDataGridView;
            this.Controls.Add(detailDataGridView);
            detailDataGridView.BringToFront();

            if (visible)
            {
                dataGridViewMaster.Rows[rowIndex].Cells["ExpandDetails"].Value = "-";
            }
        }

        /// <summary>
        /// Metoda pro zrušení DetailGridu
        /// </summary>
        /// <param name="masterRowIndex"></param>
        private void DisposeDetailDataGridView(int masterRowIndex)
        {
            if (detailGrids.TryGetValue(masterRowIndex, out var detailPanel))
            {
                Controls.Remove(detailPanel);
                detailPanel.Dispose();
                detailGrids.Remove(masterRowIndex);

                dataGridViewMaster.Rows[masterRowIndex].Cells["ExpandDetails"].Value = "+";
            }
        }

        /// <summary>
        /// Metoda pro nahrání výstupního CSV a aktualizaci stavu zakázek v detailGridu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUploadHot_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\Users\\Adam\\Documents\\TUL\\SZZ\\BP\\importHotov";
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var filePath = openFileDialog.FileName;
                    string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

                    foreach (DataGridViewRow masterRow in dataGridViewMaster.Rows)
                    {
                        if (masterRow.Tag is List<string[]> detailsList)
                        {
                            foreach (string line in lines.Skip(1)) // Skip the header
                            {
                                string[] fields = line.Split(';');
                                string cestaKSouboru = fields[9].Trim(); // 10. sloupec v CSV
                                string ohyb = fields[7].Trim(); // 7. sloupec v CSV
                                string vyrobeno = fields[5].Trim(); // 6. sloupec v CSV


                                bool foundMatch = false;

                                foreach (var detail in detailsList)
                                {
                                    if (detail[7].Trim() == cestaKSouboru)
                                    {
                                        foundMatch = true;
                                        string originalPocet = detail[5];
                                        string novyStavObjednavky =
                                            _orderManager.DetermineOrderStatus(vyrobeno, originalPocet, ohyb);

                                        // Aktualizace
                                        detail[8] = vyrobeno;
                                        detail[9] = novyStavObjednavky;

                                        UpdateDetailGridRow(masterRow.Index, detail);
                                    }
                                }

                                if (!foundMatch)
                                {
                                    if (detailGrids.TryGetValue(masterRow.Index, out var detailGrid))
                                    {
                                        foreach (DataGridViewRow row in detailGrid.Rows)
                                        {
                                            string cestaVDetailGrid =
                                                row.Cells["cestaKSouboru"].Value?.ToString() ?? "";
                                        }
                                    }
                                }
                            }
                        }
                    }

                    _dataGridViewHelper.UpdateAllMasterGridRowStatuses(dataGridViewMaster);
                    ExportDataToXlsx(dataGridViewMaster, detailGrids);
                }
            }
        }

        /// <summary>
        /// Metoda pro aktualizaci DetailGrid řádku
        /// </summary>
        /// <param name="masterRowIndex"></param>
        /// <param name="detail"></param>
        public void UpdateDetailGridRow(int masterRowIndex, string[] detail)
        {
            if (detailGrids.TryGetValue(masterRowIndex, out var detailGrid))
            {
                string cestaKSouboru = detail[7].Trim();

                foreach (DataGridViewRow row in detailGrid.Rows)
                {
                    //if (row.IsNewRow) continue;
                    string status = row.Cells["stavObjednavky"].Value?.ToString();

                    if (row.Cells["cestaKSouboru"].Value?.ToString().Trim() == cestaKSouboru)
                    {
                        row.Cells["vyrobeno"].Value = detail[8];
                        row.Cells["stavObjednavky"].Value = detail[9];

                        // Po aktualizaci jednoho řádku se nemusí pokračovat v iteraci, pokud je 'cestaKSouboru' unikátní pro každý řádek.
                        // break;
                    }
                }

                detailGrid.Refresh();
            }
        }

        /// <summary>
        /// Metoda pro zbarvení buňky dle stavu položky v zakázce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView detailGridView = sender as DataGridView;

            if (detailGridView.Columns["stavObjednavky"] != null &&
                e.ColumnIndex == detailGridView.Columns["stavObjednavky"].Index)
            {
                string stav = (string)detailGridView.Rows[e.RowIndex].Cells["stavObjednavky"].Value;
                e.CellStyle.BackColor = _orderManager.GetColorForStatus(stav);
            }
        }

        private const string ExcelFilePath = @"C:\Users\Adam\Documents\TUL\SZZ\BP\data\orders.xlsx";


        public void ExportDataToXlsx(DataGridView masterGrid, Dictionary<int, DataGridView> detailGrids)
        {
            Debug.WriteLine("Začínám export do Excelu.");

            using (var workbook = new XLWorkbook())
            {
                for (int i = 0; i < masterGrid.Rows.Count; i++)
                {
                    var masterRow = masterGrid.Rows[i];
                    if (!masterRow.IsNewRow)
                    {
                        var orderNumber = masterRow.Cells["NumOfOrder"].Value.ToString();
                        Debug.WriteLine($"Vytvářím list pro objednávku číslo: {orderNumber}");

                        var worksheet = workbook.Worksheets.Add("Order_" + orderNumber);

                        // Přidání hlaviček a dat masterGrid do prvního řádku
                        for (int colIndex = 0; colIndex < masterGrid.Columns.Count; colIndex++)
                        {
                            worksheet.Cell(1, colIndex + 1).Value = masterGrid.Columns[colIndex].HeaderText; // Hlavička
                            worksheet.Cell(2, colIndex + 1).Value =
                                masterRow.Cells[colIndex].Value?.ToString() ?? ""; // Data
                        }

                        if (detailGrids.ContainsKey(i))
                        {
                            var detailGrid = detailGrids[i];
                            Debug.WriteLine(
                                $"DetailGrid pro objednávku číslo: {orderNumber} má {detailGrid.Rows.Count} řádků.");
                            ExportGridToWorksheet(detailGrid, worksheet,
                                startRow: 3); // Přidání detailGrid od třetího řádku
                        }
                        else
                        {
                            Debug.WriteLine($"DetailGrid pro objednávku číslo: {orderNumber} nebyl nalezen.");
                        }
                    }
                }

                // Uložení do souboru
                var fileInfo = new FileInfo(ExcelFilePath);
                fileInfo.Directory.Create();
                workbook.SaveAs(ExcelFilePath);
                Debug.WriteLine($"Data byla uložena do souboru: {ExcelFilePath}");
            }
        }

        private void ExportGridToWorksheet(DataGridView grid, IXLWorksheet worksheet, int startRow = 1)
        {
            // Přidání hlaviček
            for (int colIndex = 0; colIndex < grid.Columns.Count; colIndex++)
            {
                worksheet.Cell(startRow, colIndex + 1).Value = grid.Columns[colIndex].HeaderText;
            }

            // Přidání dat
            for (int rowIndex = 0; rowIndex < grid.Rows.Count; rowIndex++)
            {
                var row = grid.Rows[rowIndex];
                if (!row.IsNewRow)
                {
                    for (int colIndex = 0; colIndex < grid.Columns.Count; colIndex++)
                    {
                        worksheet.Cell(rowIndex + startRow + 1, colIndex + 1).Value =
                            row.Cells[colIndex].Value?.ToString() ?? "";
                    }
                }
            }
        }


        /*private void LoadDataFromXlsx(string filePath)
        {
            using (var workbook = new XLWorkbook(filePath))
            {
                foreach (var worksheet in workbook.Worksheets)
                {
                    if (int.TryParse(worksheet.Name.Replace("Order_", ""), out int orderNumber))
                    {
                        DataGridView detailGrid;

                        // Kontrola, jestli už detailGrid existuje v slovníku
                        if (detailGrids.ContainsKey(orderNumber))
                        {
                            detailGrid = detailGrids[orderNumber];
                        }
                        else
                        {
                            detailGrid = new DataGridView();
                            detailGrids[orderNumber] = detailGrid;
                        }

                        var rows = worksheet.RowsUsed().ToList();
                        if (rows.Count < 3) continue; // Pokud nejsou dostatečná data, pokračujeme dalším listem.

                        // Vytvoření sloupců pro masterGrid podle druhého řádku v Excelu (hlavička detailGrid)
                        CreateColumnsForDataGridView(dataGridViewMaster, rows[2]);

                        // Vytvoření sloupců pro detailGrid podle třetího řádku v Excelu (hlavička detailGrid)
                        CreateColumnsForDataGridView(detailGrid, rows[2]);

                        // Přidání dat do masterGrid
                        int masterRowIndex = dataGridViewMaster.Rows.Add();
                        for (int i = 0; i < dataGridViewMaster.Columns.Count; i++)
                        {
                            dataGridViewMaster.Rows[masterRowIndex].Cells[i].Value = rows[1].Cell(i + 1).Value;
                        }

                        // Přidání dat do detailGrid
                        for (int rowIndex = 3; rowIndex < rows.Count; rowIndex++) // Začneme na čtvrtém řádku
                        {
                            int detailIndex = detailGrid.Rows.Add();
                            for (int colIndex = 0; colIndex < detailGrid.Columns.Count; colIndex++)
                            {
                                detailGrid.Rows[detailIndex].Cells[colIndex].Value =
                                    rows[rowIndex].Cell(colIndex + 1).Value;
                            }
                        }
                    }
                }
            }
        }*/


        private void detailGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var detailGrid = sender as DataGridView;
            int masterRowIndex = Convert.ToInt32(detailGrid.Tag);

            if (masterRowIndex < 0 || masterRowIndex >= dataGridViewMaster.Rows.Count)
                return;

            var masterRow = dataGridViewMaster.Rows[masterRowIndex];
            if (masterRow.Tag is List<string[]> detailsList && e.RowIndex < detailsList.Count)
            {
                var detailRow = detailGrid.Rows[e.RowIndex];
                var vyrobeno = detailRow.Cells[6].Value?.ToString();
                var pocet = detailRow.Cells[3].Value?.ToString();
                var ohyb = detailRow.Cells[4].Value?.ToString();

                string novyStav = UrčitNovýStav(vyrobeno, pocet, ohyb);

                // Aktualizujte rowData
                var rowData = detailsList[e.RowIndex];
                rowData[8] = vyrobeno; // Index pro 'vyrobeno'
                rowData[5] = pocet; // Index pro 'pocet'
                rowData[6] = ohyb; // Index pro 'ohyb'
                rowData[9] = novyStav; // Index pro 'stavObjednavky', předpokládá se, že je na indexu 7

                // Aktualizace stavu v detailGridu
                detailRow.Cells["stavObjednavky"].Value = novyStav;

                var celkovýStav = UrčitStavZakázky(detailsList);
                dataGridViewMaster.Rows[masterRowIndex].Cells["stateOfOrder"].Value = celkovýStav;

                // Aktualizujte barvu na základě celkového stavu
                AktualizovatBarvuStavuZakázky(dataGridViewMaster.Rows[masterRowIndex], celkovýStav);
            }

            // Volitelně: Uložte změny do CSV nebo jiného úložiště
        }

        private void AktualizovatBarvuStavuZakázky(DataGridViewRow masterRow, string stav)
        {
            if (stav == "Hotovo")
            {
                masterRow.Cells[5].Style.BackColor = System.Drawing.Color.Green; // nebo jiná barva pro Hotovo
            }
            else if (stav == "Rozpracováno")
            {
                masterRow.Cells[5].Style.BackColor = System.Drawing.Color.Yellow; // nebo jiná barva pro Rozpracováno
            }
            else
            {
                masterRow.Cells[5].Style.BackColor = System.Drawing.Color.LightGray; // nebo jiná barva pro Neznámý stav
            }
        }


        private string UrčitStavZakázky(List<string[]> detailData)
        {
            bool anyInProgressOrComplete = false;
            bool allDone = true;

            foreach (var řádek in detailData)
            {
                string stav = řádek[9]; // Předpokládá se, že index 7 obsahuje stav

                if (stav.Equals("Hotovo", StringComparison.OrdinalIgnoreCase))
                {
                    anyInProgressOrComplete = true; // alespoň jeden stav "Hotovo"
                }
                else
                {
                    allDone = false;
                    if (stav.Equals("Rozpracováno", StringComparison.OrdinalIgnoreCase) ||
                        stav.Equals("Hotovo", StringComparison.OrdinalIgnoreCase))
                    {
                        anyInProgressOrComplete = true; // alespoň jeden "Rozpracovano" nebo "Hotovo"
                    }
                }
            }

            if (allDone)
            {
                return "Hotovo";
            }
            else if (anyInProgressOrComplete)
            {
                return "Rozpracováno";
            }
            else
            {
                return "Nezadáno";
            }

            return "Nezadáno";
        }


        private string UrčitNovýStav(string vyrobeno, string pocet, string ohyb)
        {
            if (string.IsNullOrEmpty(vyrobeno))
            {
                return "Neznámý";
            }

            if (int.TryParse(vyrobeno, out int vyrobenoInt) && int.TryParse(pocet, out int pocetInt))
            {
                if (vyrobenoInt == pocetInt)
                {
                    if (ohyb.Equals("NE", StringComparison.OrdinalIgnoreCase))
                    {
                        return "Hotovo";
                    }
                    else if (ohyb.Equals("ANO", StringComparison.OrdinalIgnoreCase))
                    {
                        return "Rozpracováno";
                    }
                }
                else if (vyrobenoInt < pocetInt)
                {
                    return "Rozpracováno";
                }
                else if (vyrobenoInt > pocetInt)
                {
                    return "Více kusů";
                }
            }

            return "Neplatný stav";
        }


        private void UpdateXlsxFileWithNewData(string orderNumber, DataGridViewRow updatedMasterRow,
            DataGridView detailGrid)
        {
            using (var workbook = new XLWorkbook(ExcelFilePath))
            {
                var worksheet = workbook.Worksheets.FirstOrDefault(ws => ws.Name == "Order_" + orderNumber);
                if (worksheet != null)
                {
                    // Aktualizace masterGrid row
                    for (int i = 0; i < updatedMasterRow.Cells.Count; i++)
                    {
                        worksheet.Cell(2, i + 1).Value = updatedMasterRow.Cells[i].Value?.ToString() ?? "";
                    }

                    // Aktualizace detailGrid rows
                    if (detailGrid != null)
                    {
                        for (int rowIndex = 0; rowIndex < detailGrid.Rows.Count; rowIndex++)
                        {
                            var row = detailGrid.Rows[rowIndex];
                            for (int colIndex = 0; colIndex < row.Cells.Count; colIndex++)
                            {
                                worksheet.Cell(rowIndex + 3, colIndex + 1).Value =
                                    row.Cells[colIndex].Value?.ToString() ?? "";
                            }
                        }
                    }

                    workbook.Save();
                }
            }
        }
    }
}