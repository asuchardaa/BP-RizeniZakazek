using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

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
            BtnDashboard.BackColor = Color.FromArgb(46, 51, 73);
            dataGridViewMaster.CellContentClick +=
                new DataGridViewCellEventHandler(dataGridViewMaster_CellContentClick);
            InitializeDataGridViewMaster();
            this.Load += new System.EventHandler(this.Form1_Load);
            //settingsPanel.Visible = false;
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
        }

        /// <summary>
        /// Metoda pro reset barvy tlačítek v navigačním panelu
        /// </summary>
        private void ResetButtonColors()
        {
            BtnDashboard.BackColor = Color.FromArgb(24, 30, 54);
            BtnStatistics.BackColor = Color.FromArgb(24, 30, 54);
            BtnCalender.BackColor = Color.FromArgb(24, 30, 54);
            BtnArchive.BackColor = Color.FromArgb(24, 30, 54);
            BtnSettings.BackColor = Color.FromArgb(24, 30, 54);
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
            BtnDashboard.BackColor = Color.FromArgb(46, 51, 73);
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
            BtnStatistics.BackColor = Color.FromArgb(46, 51, 73);
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
            BtnCalender.BackColor = Color.FromArgb(46, 51, 73);
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
            BtnArchive.BackColor = Color.FromArgb(46, 51, 73);
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
            BtnSettings.BackColor = Color.FromArgb(46, 51, 73);
        }

        /// <summary>
        /// Metoda pro zobrazení efektu při najetí na tlačítko Dashboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDashboard_Leave(object sender, EventArgs e)
        {
            BtnDashboard.BackColor = Color.FromArgb(24, 30, 54);
        }

        /// <summary>
        /// Metoda pro zobrazení efektu při odchodu z tlačítka Statistics
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStatistics_Leave(object sender, EventArgs e)
        {
            BtnStatistics.BackColor = Color.FromArgb(24, 30, 54);
        }

        /// <summary>
        /// Metoda pro zobrazení efektu při odchodu z tlačítka Calender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCalender_Leave(object sender, EventArgs e)
        {
            BtnCalender.BackColor = Color.FromArgb(24, 30, 54);
        }

        /// <summary>
        /// Metoda pro zobrazení efektu při odchodu z tlačítka Archive
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnArchive_Leave(object sender, EventArgs e)
        {
            BtnArchive.BackColor = Color.FromArgb(24, 30, 54);
        }

        /// <summary>
        /// Metoda pro zobrazení efektu při odchodu z tlačítka Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSettings_Leave(object sender, EventArgs e)
        {
            BtnSettings.BackColor = Color.FromArgb(24, 30, 54);
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
                    statusMatches = row.Cells["StateOfOrder"].Value.ToString()
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

            dataGridViewMaster.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewMaster.ColumnHeadersDefaultCellStyle.BackColor = Color.RoyalBlue;
            dataGridViewMaster.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewMaster.ColumnHeadersDefaultCellStyle.Font = new Font("Sans-Serif", 13);
            dataGridViewMaster.EnableHeadersVisualStyles = false;
            dataGridViewMaster.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridViewMaster.RowTemplate.Height = 30;
            dataGridViewMaster.ColumnHeadersHeight = 250;

            dataGridViewMaster.ScrollBars = ScrollBars.Both;
            dataGridViewMaster.GridColor = Color.DarkGray;
            dataGridViewMaster.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewMaster.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dataGridViewMaster.DefaultCellStyle.SelectionForeColor = Color.White;
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
                                _dataGridViewHelper.OdstranitSpecifickyRadek(dataGridViewMaster, cisloObjednavky);
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

                            if (!_dataGridViewHelper.RowExists(dataGridViewMaster, fields[0], fields[1], fields[8]))
                            {
                                int rowIndex = dataGridViewMaster.Rows.Add();
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
                                _dataGridViewHelper.AddDetailsToExistingRow(dataGridViewMaster, fields[0], fields[1],
                                    fields[8], filteredFields);
                            }
                        }
                    }
                }
            }

            _dataGridViewHelper.UpdateAllMasterGridRowStatuses(dataGridViewMaster);
        }

        /// <summary>
        /// Metoda pro rozbalení DetailGridu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewMaster_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
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
                ReadOnly = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 9.75F),
                    BackColor = Color.AliceBlue,
                    ForeColor = Color.Black,
                    // V případě potřeby organizace, Selection BC a FC odkomentovat pro zobrazení barvy výběru
                    // SelectionBackColor = Color.AliceBlue,
                    // SelectionForeColor = Color.Black
                },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                    BackColor = Color.CornflowerBlue,
                    ForeColor = Color.White
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.LightGray
                },
                GridColor = Color.DarkGray,
                RowHeadersVisible = false
            };
            detailDataGridView.RowTemplate.Height = 30;
            detailDataGridView.ScrollBars = ScrollBars.Both;
            detailDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            detailDataGridView.Width = dataGridViewMaster.Width;

            detailDataGridView.Columns.Add("nazev", "Název");
            detailDataGridView.Columns.Add("material", "Materiál");
            detailDataGridView.Columns.Add("tloustka", "Tloušťka");
            detailDataGridView.Columns.Add("pocet", "Počet");
            detailDataGridView.Columns.Add("ohyb", "Ohyb");
            detailDataGridView.Columns.Add("cestaKSouboru", "Cesta");
            detailDataGridView.Columns.Add("vyrobeno", "Vyrobeno");
            detailDataGridView.Columns.Add("stavObjednavky", "Stav");

            detailDataGridView.Columns["stavObjednavky"].DefaultCellStyle.BackColor = Color.White;

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
                                        string novyStavObjednavky = _orderManager.DetermineOrderStatus(vyrobeno, originalPocet, ohyb);

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
                                            string cestaVDetailGrid = row.Cells["cestaKSouboru"].Value?.ToString() ?? "";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            _dataGridViewHelper.UpdateAllMasterGridRowStatuses(dataGridViewMaster);
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
    }
}