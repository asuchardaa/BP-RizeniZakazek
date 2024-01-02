using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Timer = System.Windows.Forms.Timer;

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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void ResetButtonColors()
        {
            BtnDashboard.BackColor = Color.FromArgb(24, 30, 54);
            BtnStatistics.BackColor = Color.FromArgb(24, 30, 54);
            BtnCalender.BackColor = Color.FromArgb(24, 30, 54);
            BtnArchive.BackColor = Color.FromArgb(24, 30, 54);
            BtnSettings.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            NavPnl.Height = BtnDashboard.Height;
            NavPnl.Top = BtnDashboard.Top;
            NavPnl.Left = BtnDashboard.Left;
            BtnDashboard.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void BtnStatistics_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            NavPnl.Height = BtnStatistics.Height;
            NavPnl.Top = BtnStatistics.Top;
            NavPnl.Left = BtnStatistics.Left;
            BtnStatistics.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void BtnCalender_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            NavPnl.Height = BtnCalender.Height;
            NavPnl.Top = BtnCalender.Top;
            NavPnl.Left = BtnCalender.Left;
            BtnCalender.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void BtnArchive_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            NavPnl.Height = BtnArchive.Height;
            NavPnl.Top = BtnArchive.Top;
            NavPnl.Left = BtnArchive.Left;
            BtnArchive.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            NavPnl.Height = BtnSettings.Height;
            NavPnl.Top = BtnSettings.Top;
            NavPnl.Left = BtnSettings.Left;
            BtnSettings.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void BtnDashboard_Leave(object sender, EventArgs e)
        {
            BtnDashboard.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void BtnStatistics_Leave(object sender, EventArgs e)
        {
            BtnStatistics.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void BtnCalender_Leave(object sender, EventArgs e)
        {
            BtnCalender.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void BtnArchive_Leave(object sender, EventArgs e)
        {
            BtnArchive.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void BtnSettings_Leave(object sender, EventArgs e)
        {
            BtnSettings.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void InitializeDataGridViewMaster()
        {
            if (!dataGridViewMaster.Columns.Contains("ExpandDetails"))
            {
                DataGridViewButtonColumn expandColumn = new DataGridViewButtonColumn();
                expandColumn.HeaderText = "";
                expandColumn.Name = "ExpandDetails";
                expandColumn.Text = "+";
                expandColumn.UseColumnTextForButtonValue = true;
                expandColumn.Width = 40;
                dataGridViewMaster.Columns.Insert(0, expandColumn);
            }
        }


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
                    string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

                    if (lines.Length > 1)
                    {
                        foreach (string line in lines.Skip(1)) // Skip the header
                        {
                            string[] fields = line.Split(';');
                            var filteredFields = fields.Where((field, index) => index != 6 && index != 8).ToArray();

                            if (!RowExists(fields[0], fields[1], fields[8]))
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
                                AddDetailsToExistingRow(fields[0], fields[1], fields[8], filteredFields);
                            }
                        }
                    }
                }
            }
            UpdateAllMasterGridRowStatuses();

        }

        private bool RowExists(string customer, string orderNumber, string date)
        {
            foreach (DataGridViewRow row in dataGridViewMaster.Rows)
            {
                if (row.IsNewRow) continue;

                var customerCell = row.Cells["Customer"].Value?.ToString() ?? "";
                var orderNumberCell = row.Cells["NumOfOrder"].Value?.ToString() ?? "";
                var dateCell = row.Cells["Date"].Value?.ToString() ?? "";

                if (customerCell == customer && orderNumberCell == orderNumber && dateCell == date)
                {
                    return true;
                }
            }

            return false;
        }


        private int AddDetailsToExistingRow(string customer, string orderNumber, string date, string[] details)
        {
            foreach (DataGridViewRow row in dataGridViewMaster.Rows)
            {
                if (row.Cells["Customer"].Value.ToString() == customer &&
                    row.Cells["NumOfOrder"].Value.ToString() == orderNumber &&
                    row.Cells["Date"].Value.ToString() == date)
                {
                    var detailsList = (List<string[]>)row.Tag;
                    detailsList.Add(details);
                    return row.Index;
                }
            }

            return -1;
        }


        private void dataGridViewMaster_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridViewMaster.Columns[e.ColumnIndex].Name == "ExpandDetails")
            {
                if (detailGrids.ContainsKey(e.RowIndex) && detailGrids[e.RowIndex].Visible)
                {
                    DisposeDetailDataGridView(e.RowIndex);
                }
                else
                {
                    var detailsList = dataGridViewMaster.Rows[e.RowIndex].Tag as List<string[]>;
                    if (detailsList != null)
                    {
                        CreateAndShowDetailDataGridView(e.RowIndex, detailsList, visible: true);
                    }
                }
            }
        }

        private void CreateAndShowDetailDataGridView(int rowIndex, List<string[]> detailsList, bool visible)
        {
            // Dispose of the existing detail DataGridView if it exists
            if (detailGrids.TryGetValue(rowIndex, out var existingDetailGrid))
            {
                existingDetailGrid.Dispose();
                detailGrids.Remove(rowIndex);
            }

            // Create a new DataGridView for showing details
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
                    BackColor = Color.WhiteSmoke
                },
                GridColor = Color.Gainsboro,
                RowHeadersVisible = false
            };

            // Match the width of the master DataGridView
            detailDataGridView.Width = dataGridViewMaster.Width;

            // Add columns to the detail DataGridView (adjust the column names as necessary)
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
                Debug.WriteLine(string.Join(", ", detail));

                int detailIndex = detailDataGridView.Rows.Add(detail.Skip(2).Take(detailDataGridView.Columns.Count).ToArray());
                string stavObjednavky = string.IsNullOrEmpty(detail[9]) ? "Neznámý" : detail[9];
                detailDataGridView.Rows[detailIndex].Cells["stavObjednavky"].Value = stavObjednavky;
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

            if (visible)
            {
                this.Controls.Add(detailDataGridView);
                detailDataGridView.BringToFront();
                dataGridViewMaster.Rows[rowIndex].Cells["ExpandDetails"].Value = "-";
            }
        }

        private void DisposeDetailDataGridView(int masterRowIndex)
        {
            // Find and dispose the detail Panel associated with the master row index
            if (detailGrids.TryGetValue(masterRowIndex, out var detailPanel))
            {
                Controls.Remove(detailPanel);
                detailPanel.Dispose();
                detailGrids.Remove(masterRowIndex);

                // Update the button text to indicate expansion
                dataGridViewMaster.Rows[masterRowIndex].Cells["ExpandDetails"].Value = "+";
            }
        }

        // V této metodě nahrajeme výstupní CSV a aktualizujeme stav zakázek
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

                                Debug.WriteLine($"Hledání: {cestaKSouboru}, Vyrobeno: {vyrobeno}");

                                bool foundMatch = false;

                                foreach (var detail in detailsList)
                                {
                                    Debug.WriteLine($"Detail: {string.Join(", ", detail.Select(d => d.Trim()))}");
                                    if (detail[7].Trim() == cestaKSouboru)
                                    {
                                        foundMatch = true;
                                        string originalPocet = detail[5]; // Původní počet kusů
                                        string novyStavObjednavky = DetermineOrderStatus(vyrobeno, originalPocet, ohyb);

                                        Debug.WriteLine(
                                            $"Nalezeno: {cestaKSouboru}, Original: {originalPocet}, Stav: {novyStavObjednavky}");


                                        // Aktualizujte detail
                                        detail[8] = vyrobeno; // Aktualizace vyrobeného množství
                                        detail[9] = novyStavObjednavky; // Aktualizace stavu objednávky

                                        // Aktualizujte příslušný řádek v DetailGrid
                                        UpdateDetailGridRow(masterRow.Index, detail);
                                    }
                                }

                                if (!foundMatch)
                                {
                                    Debug.WriteLine($"Nenalezena shoda pro: {cestaKSouboru}");
                                    VypisHodnotyCestaKSouboruZDetailGrid(masterRow);
                                }
                            }
                        }
                    }
                }
            }
            UpdateAllMasterGridRowStatuses();
        }

        private void UpdateAllMasterGridRowStatuses()
        {
            Debug.WriteLine("Aktualizace stavů pro všechny řádky v masterGridu");
            foreach (DataGridViewRow masterRow in dataGridViewMaster.Rows)
            {
                if (masterRow.IsNewRow) continue;

                string overallStatus = CalculateOverallStatus(masterRow);
                Debug.WriteLine($"Řádek {masterRow.Index}: Celkový stav = {overallStatus}");
                masterRow.Cells["stateOfOrder"].Value = overallStatus;
            }
        }

        private string CalculateOverallStatus(DataGridViewRow masterRow)
        {
            if (masterRow.Tag is List<string[]> detailsList)
            {
                bool anyInProgressOrComplete = false;
                bool allDone = true;

                foreach (var detail in detailsList)
                {
                    string status = detail[9];

                    if (status.Equals("Hotovo", StringComparison.OrdinalIgnoreCase))
                    {
                        anyInProgressOrComplete = true; // alespoň jeden stav "Hotovo"
                    }
                    else
                    {
                        allDone = false; 
                        if (status.Equals("Rozpracovano", StringComparison.OrdinalIgnoreCase) ||
                            status.Equals("Hotovo", StringComparison.OrdinalIgnoreCase))
                        {
                            anyInProgressOrComplete = true; // alespoň jeden "Rozpracovano" nebo "Hotovo"
                        }
                    }
                }

                string resultStatus;

                if (allDone)
                {
                    resultStatus = "Hotovo";
                }
                else if (anyInProgressOrComplete)
                {
                    resultStatus = "Rozpracovano";
                }
                else
                {
                    resultStatus = "Nezadáno";
                }

                DataGridViewCell statusCell = masterRow.Cells["StateOfOrder"];
                SetOrderCellColor(statusCell, resultStatus);
                UpdateDateOfFinish(masterRow, resultStatus);
                int dateColIndex = dataGridViewMaster.Columns["Date"].Index;
                HighlightOverdueDates(masterRow, dateColIndex);
                return resultStatus;

            }

            return "Neznámý";
        }

        private void SetOrderCellColor(DataGridViewCell cell, string status)
        {
            Color color;
            switch (status)
            {
                case "Hotovo":
                    color = Color.Green;
                    break;
                case "Rozpracovano":
                    color = Color.Yellow;
                    break;
                case "Nezadáno":
                    color = Color.LightBlue;
                    break;
                default:
                    color = Color.Red;
                    break;
            }

            cell.Style.BackColor = color;
        }

        private void UpdateDateOfFinish(DataGridViewRow row, string status)
        {
            if (status == "Hotovo")
            {
                string formattedDate = DateTime.Now.ToString("dd/MM/yyyy");
                row.Cells["dateOfFinish"].Value = formattedDate;
            }
        }

        private void HighlightOverdueDates(DataGridViewRow row, int dateColumnIndex)
        {
            if (DateTime.TryParse(row.Cells[dateColumnIndex].Value?.ToString(), out DateTime cellDate))
            {
                if (cellDate < DateTime.Now.Date)
                {
                    row.Cells[dateColumnIndex].Style.BackColor = Color.Red;
                }
            }
        }


        private void VypisHodnotyCestaKSouboruZDetailGrid(DataGridViewRow masterRow)
        {
            if (detailGrids.TryGetValue(masterRow.Index, out var detailGrid))
            {
                foreach (DataGridViewRow row in detailGrid.Rows)
                {
                    string cestaVDetailGrid = row.Cells["cestaKSouboru"].Value?.ToString() ?? "";
                    Debug.WriteLine($"Hodnota v DetailGrid 'cestaKSouboru': {cestaVDetailGrid}");
                }
            }
        }

        private string DetermineOrderStatus(string vyrobeno, string originalPocet, String ohyb)
        {
            bool parseVyrobeno = int.TryParse(vyrobeno, out int numVyrobeno);
            bool parseOriginalPocet = int.TryParse(originalPocet, out int numOriginalPocet);

            Debug.WriteLine($"\nDetermineOrderStatus - Vyrobeno: {vyrobeno}, OriginalPocet: {originalPocet}, Ohyb: {ohyb}\n");
            Debug.WriteLine($"\nParsed Vyrobeno: {numVyrobeno}, Parsed OriginalPocet: {numOriginalPocet}\n");

            if (!parseVyrobeno || !parseOriginalPocet)
            {
                Debug.WriteLine("Nepodařilo se zpracovat čísla pro vyrobeno nebo originalPocet.");

                return "chyba přev";
            }

            if (numVyrobeno == 0)
            {
                return "Nehotovo";
            }
            else if (numVyrobeno != numOriginalPocet || ohyb.Equals("ANO", StringComparison.OrdinalIgnoreCase))
            {
                return "Rozpracovano";
            }
            else if (numVyrobeno == numOriginalPocet && ohyb.Equals("NE", StringComparison.OrdinalIgnoreCase))
            {
                return "Hotovo";
            }

            return "Nehotovo";
        }

        private void UpdateDetailGridRow(int masterRowIndex, string[] detail)
        {
            if (detailGrids.TryGetValue(masterRowIndex, out var detailGrid))
            {
                Debug.WriteLine($"DetailGrid pro MasterGrid řádek {masterRowIndex} byl nalezen.");

                string cestaKSouboru = detail[7].Trim();

                foreach (DataGridViewRow row in detailGrid.Rows)
                {
                    //if (row.IsNewRow) continue;
                    string status = row.Cells["stavObjednavky"].Value?.ToString();
                    Debug.WriteLine($"Řádek {row.Index}, Stav: {status}");

                    if (row.Cells["cestaKSouboru"].Value?.ToString().Trim() == cestaKSouboru)
                    {
                        row.Cells["vyrobeno"].Value = detail[8];
                        row.Cells["stavObjednavky"].Value = detail[9];

                        Debug.WriteLine(
                            $"Aktualizace řádku {masterRowIndex} pro {cestaKSouboru}: Vyrobeno = {detail[8]}, Stav = {detail[9]}");

                        // Po aktualizaci jednoho řádku se nemusí pokračovat v iteraci, pokud je 'cestaKSouboru' unikátní pro každý řádek.
                        // break;
                    }
                }

                detailGrid.Refresh();
                Debug.WriteLine($"Vytvoření DetailGrid pro MasterGrid řádek {masterRowIndex}");
            }
            else
            {
                Debug.WriteLine($"DetailGrid pro MasterGrid řádek {masterRowIndex} nebyl nalezen.");

            }
        }


        private void DetailDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView detailGridView = sender as DataGridView;

            if (detailGridView.Columns["stavObjednavky"] != null &&
                e.ColumnIndex == detailGridView.Columns["stavObjednavky"].Index)
            {
                string stav = (string)detailGridView.Rows[e.RowIndex].Cells["stavObjednavky"].Value;
                e.CellStyle.BackColor = GetColorForStatus(stav);
            }
        }

        // Metoda pro získání barvy podle stavu
        private Color GetColorForStatus(string status)
        {
            switch (status.ToLower())
            {
                case "neznámý":
                    return Color.LightGray;
                case "rozpracovano":
                    return Color.Yellow;
                case "hotovo":
                    return Color.Green;
                default:
                    return Color.White;
            }
        }
    }
}