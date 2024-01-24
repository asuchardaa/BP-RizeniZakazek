using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;
using BP_rizeni_zakazek.utils;
using Newtonsoft.Json;

namespace BP_rizeni_zakazek

{
    /// <summary>
    /// Třída pro hlavní formulář s inicializací tlačítek a funkcemi
    /// </summary>
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

        private Dictionary<string, string> vstupniMaterialy = new Dictionary<string, string>();
        private Dictionary<string, string> vystupniMaterialy = new Dictionary<string, string>();

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
            dataGridViewMaster.CellEndEdit += new DataGridViewCellEventHandler(detailGrid_CellEndEdit);

            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
            this.Load += new EventHandler(MainForm_Load);
        }

        /// <summary>
        /// Metoda pro okamžitý save dat při zavření aplikace
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string dataFilePath = "C:\\Users\\Adam\\Documents\\TUL\\SZZ\\BP\\data\\orders.json";

            SaveDataToJson(dataFilePath);
        }

        /// <summary>
        /// Metoda pro okamžitý load dat po startu aplikace
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            string dataFilePath = "C:\\Users\\Adam\\Documents\\TUL\\SZZ\\BP\\data\\orders.json";

            if (File.Exists(dataFilePath))
            {
                LoadDataFromJson(dataFilePath);
            }
            else
            {
                File.Create(dataFilePath).Close();
            }
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
        /// Metoda pro aplikaci filtru na masterGrid (zákazník a číslo zakázky) a detailGrid (název položky)
        /// </summary>
        private void ApplyFilter()
        {
            string searchText = FilterTextBox.Text.ToLower();
            string selectedStatus = OrderDoneOrNotDone.SelectedItem.ToString();

            foreach (DataGridViewRow masterRow in dataGridViewMaster.Rows)
            {
                bool textMatches = masterRow.Cells["Customer"].Value.ToString().ToLower().Contains(searchText) ||
                                   masterRow.Cells["NumOfOrder"].Value.ToString().ToLower().Contains(searchText);
                bool statusMatches = selectedStatus == "Vše" ||
                                     masterRow.Cells["stateOfOrder"].Value.ToString().Equals(selectedStatus,
                                         StringComparison.OrdinalIgnoreCase);

                if (!textMatches && masterRow.Tag is List<string[]> detailsList)
                {
                    foreach (var detail in detailsList)
                    {
                        if (detail[2].ToLower().Contains(searchText))
                        {
                            textMatches = true;
                            break;
                        }
                    }
                }

                masterRow.Visible = textMatches && statusMatches;
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
                expandColumn.UseColumnTextForButtonValue = false;
                expandColumn.Width = 40;
                dataGridViewMaster.Columns.Insert(0, expandColumn);
            }

            // Přidání sloupce pro vymazání
            if (!dataGridViewMaster.Columns.Contains("DeleteColumn"))
            {
                var deleteColumn = new DataGridViewButtonColumn
                {
                    HeaderText = "Smazat",
                    Name = "DeleteColumn",
                    Text = "Smazat",
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
                    string cisloObjednavky = _csvManager.FindNumberOfOrder_CSV(filePath);

                    // Kontrola duplicity
                    if (_csvManager.isFileLoaded(filePath))
                    {
                        DialogResult dialogResult = MessageBox.Show(
                            "Tato zakázka již byla nahrán. Chcete jej nahrát znovu a přepsat existující data?",
                            "Duplicitní zakázka", MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.Yes)
                        {
                            if (cisloObjednavky != null)
                            {
                                _dataGridViewHelper.DeleteSpecifiedRow(dataGridViewMaster, cisloObjednavky);
                            }
                        }
                        else
                        {
                            return;
                        }

                        _csvManager.AddLoadedFile(filePath);
                    }
                    else
                    {
                        // Kontrola, zda číslo objednávky již existuje v dataGridViewMaster
                        foreach (DataGridViewRow row in dataGridViewMaster.Rows)
                        {
                            if (row.Cells["NumOfOrder"].Value.ToString().Equals(cisloObjednavky))
                            {
                                DialogResult dialogResult = MessageBox.Show(
                                    "Data pro toto číslo zakázky již existují. Chcete je přepsat?",
                                    "Duplicitní zakázka", MessageBoxButtons.YesNo);

                                if (dialogResult == DialogResult.Yes)
                                {
                                    _dataGridViewHelper.DeleteSpecifiedRow(dataGridViewMaster, cisloObjednavky);
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }

                        _csvManager.AddLoadedFile(filePath);
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
                            string cestaKSouboru = fields[9].Trim();
                            string material = fields[3].Trim();

                            vstupniMaterialy[cestaKSouboru] = material;
                            rowIndex = FindOrAddMasterRow(fields);
                            dataGridViewMaster.Rows[rowIndex].Cells["ExpandDetails"].Value = "+";

                            UpdateOrAddDetailGrid(rowIndex, fields, filteredFields);
                        }
                    }

                    _dataGridViewHelper.UpdateAllMasterGridRowStatuses(dataGridViewMaster);
                    string dataFilePath = "C:\\Users\\Adam\\Documents\\TUL\\SZZ\\BP\\data\\orders.json";

                    SaveDataToJson(dataFilePath);
                }
            }
        }

        /// <summary>
        /// Metoda pro najití nebo přidání master řádku
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        private int FindOrAddMasterRow(string[] fields)
        {
            string customer = fields[0].Trim();
            string orderNumber = fields[1].Trim();
            string date = fields[8].Trim();

            for (int i = 0; i < dataGridViewMaster.Rows.Count; i++)
            {
                if (dataGridViewMaster.Rows[i].Cells["Customer"].Value.ToString() == customer &&
                    dataGridViewMaster.Rows[i].Cells["NumOfOrder"].Value.ToString() == orderNumber)
                {
                    return i;
                }
            }

            int rowIndex = dataGridViewMaster.Rows.Add();
            dataGridViewMaster.Rows[rowIndex].Cells["Customer"].Value = customer;
            dataGridViewMaster.Rows[rowIndex].Cells["NumOfOrder"].Value = orderNumber;
            dataGridViewMaster.Rows[rowIndex].Cells["Date"].Value = date;
            dataGridViewMaster.Rows[rowIndex].Tag = new List<string[]>();

            return rowIndex;
        }

        /// <summary>
        /// Metoda pro update nebo přidání detailGridu
        /// </summary>
        /// <param name="masterRowIndex"></param>
        /// <param name="fields"></param>
        /// <param name="filteredFields"></param>
        private void UpdateOrAddDetailGrid(int masterRowIndex, string[] fields, string[] filteredFields)
        {
            List<string[]> detailsList = (List<string[]>)dataGridViewMaster.Rows[masterRowIndex].Tag;

            string filePath = fields[9].Trim();
            bool detailExists = false;

            foreach (var detail in detailsList)
            {
                if (detail.Length > 7 && detail[7] == filePath) // cesta k souboru -> i = 7
                {
                    detailExists = true;

                    // stejna delka
                    int length = Math.Min(detail.Length, filteredFields.Length);
                    for (int i = 0; i < length; i++)
                    {
                        detail[i] = filteredFields[i];
                    }

                    break;
                }
            }

            if (!detailExists)
            {
                detailsList.Add(filteredFields);
            }

            if (!detailGrids.ContainsKey(masterRowIndex))
            {
                CreateAndShowDetailDataGridView(masterRowIndex, detailsList, false);
            }
            else
            {
                var detailGrid = detailGrids[masterRowIndex];
                UpdateDetailGrid(detailGrid, detailsList);
            }
        }

        /// <summary>
        /// Metoda pro aktualizaci detailGridu
        /// </summary>
        /// <param name="detailGrid"></param>
        /// <param name="detailsList"></param>
        private void UpdateDetailGrid(DataGridView detailGrid, List<string[]> detailsList)
        {
            detailGrid.Rows.Clear();
            foreach (var detail in detailsList)
            {
                detailGrid.Rows.Add(detail.Skip(2).Take(detailGrid.Columns.Count).ToArray());
            }
        }


        /// <summary>
        /// Metoda pro rozbalení DetailGridu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewMaster_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ochrana proti hlavičce
            if (e.RowIndex < 0)
            {
                return; 
            }

            if (dataGridViewMaster.Columns[e.ColumnIndex].Name == "ExpandDetails")
            {
                var buttonCell = dataGridViewMaster.Rows[e.RowIndex].Cells["ExpandDetails"];
                bool isDetailVisible = detailGrids.ContainsKey(e.RowIndex) && detailGrids[e.RowIndex].Visible;

                if (isDetailVisible)
                {
                    // jenom skryju detailGrid
                    detailGrids[e.RowIndex].Visible = false;
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
            else if (dataGridViewMaster.Columns[e.ColumnIndex].Name == "EditColumn")
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
                    
                    // odstraňuju rovnou i celej jeho příapdnej detailGrid
                    if (detailGrids.ContainsKey(e.RowIndex))
                    {
                        detailGrids[e.RowIndex].Dispose();
                        detailGrids.Remove(e.RowIndex);
                    }
                }
            }
        }

        /// <summary>
        /// Metoda pro začátek editace řádku
        /// </summary>
        /// <param name="rowIndex"></param>
        private void BeginEditRow(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < dataGridViewMaster.Rows.Count)
            {
                DataGridViewRow row = dataGridViewMaster.Rows[rowIndex];

                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.ReadOnly = false;
                }
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
        private DataGridView CreateAndShowDetailDataGridView(int rowIndex, List<string[]> detailsList, bool visible)
        {
            DataGridView detailDataGridView;

            if (!detailGrids.TryGetValue(rowIndex, out detailDataGridView))
            {
                detailDataGridView = new DataGridView
                {
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    AllowUserToAddRows = false,
                    ReadOnly = false,
                    AllowUserToResizeRows = false,
                    AllowUserToResizeColumns = false,
                    BackgroundColor = System.Drawing.Color.FromArgb(153, 180, 209),
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

                var deleteButtonColumn = new DataGridViewButtonColumn
                {
                    HeaderText = "Smazat",
                    Name = "DeleteColumn",
                    Text = "Smazat",
                    UseColumnTextForButtonValue = true
                };

                detailDataGridView.RowTemplate.Height = 30;
                detailDataGridView.ScrollBars = ScrollBars.Both;
                detailDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                detailDataGridView.Tag = rowIndex;
                detailDataGridView.CellEndEdit += new DataGridViewCellEventHandler(detailGrid_CellEndEdit);

                detailDataGridView.Width = dataGridViewMaster.Width;

                detailDataGridView.Columns.Add("nazev", "Název");
                detailDataGridView.Columns.Add("material", "Materiál");
                detailDataGridView.Columns.Add("tloustka", "Tloušťka");
                detailDataGridView.Columns.Add("amount", "Počet");
                detailDataGridView.Columns.Add("curve", "Ohyb");
                detailDataGridView.Columns.Add("cestaKSouboru", "Cesta");
                detailDataGridView.Columns.Add("created", "Vyrobeno");
                detailDataGridView.Columns.Add("stavObjednavky", "Stav");

                detailDataGridView.Columns.Add(deleteButtonColumn);

                detailDataGridView.Columns["stavObjednavky"].DefaultCellStyle.BackColor = System.Drawing.Color.White;

                detailDataGridView.CellFormatting +=
                    new DataGridViewCellFormattingEventHandler(DetailDataGridView_CellFormatting);
                detailDataGridView.Visible = visible;
                detailDataGridView.CellClick += new DataGridViewCellEventHandler(DetailGrid_CellClick);

                this.Controls.Add(detailDataGridView);

                detailGrids[rowIndex] = detailDataGridView;
            }

            detailDataGridView.Rows.Clear();

            foreach (var detail in detailsList)
            {
                Debug.WriteLine($"Detail: {String.Join(", ", detail)}");

                if (detail.Length < 8)
                {
                    Debug.WriteLine("Chyba: Pole detail nemá dostatečný počet prvků.");
                    continue;
                }

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
            detailDataGridView.Visible = visible;

            detailDataGridView.BringToFront();

            if (visible)
            {
                dataGridViewMaster.Rows[rowIndex].Cells["ExpandDetails"].Value = "-";
            }
            else
            {
                dataGridViewMaster.Rows[rowIndex].Cells["ExpandDetails"].Value = "+";
            }
            //SaveDataToJson("C:\\Users\\Adam\\Documents\\TUL\\SZZ\\BP\\data\\orders.json");

            return detailDataGridView;
        }

        /// <summary>
        /// Metoda pro smazání řádku v detailGridu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid == null || e.RowIndex < 0) return;

            int masterRowIndex = Convert.ToInt32(grid.Tag);
            var masterRow = dataGridViewMaster.Rows[masterRowIndex];

            if (e.ColumnIndex == grid.Columns["DeleteColumn"].Index)
            {
                var nazevPoložky = grid.Rows[e.RowIndex].Cells["nazev"].Value?.ToString() ?? "neznámá položka";
                var result = MessageBox.Show($"Opravdu chcete smazat položku '{nazevPoložky}'?", "Potvrzení smazání",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    var detailsList = (List<string[]>)masterRow.Tag;

                    if (e.RowIndex < detailsList.Count)
                    {
                        detailsList.RemoveAt(e.RowIndex);
                        grid.Rows.RemoveAt(e.RowIndex);

                        var celkovýStav = DetermineOrderStatus(detailsList);
                        masterRow.Cells["stateOfOrder"].Value = celkovýStav;
                        UpdateColorStatusOrders(masterRow, celkovýStav);
                    }
                }
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
                                string material = fields[3].Trim();

                                vystupniMaterialy[cestaKSouboru] = material;

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

                    //string dataFilePath = "C:\\Users\\Adam\\Documents\\TUL\\SZZ\\BP\\data\\orders.json";
                    //SaveDataToJson(dataFilePath);
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
                    if (row.Cells["cestaKSouboru"].Value?.ToString().Trim() == cestaKSouboru)
                    {
                        row.Cells["created"].Value = detail[8];
                        row.Cells["stavObjednavky"].Value = detail[9];

                        // Aktualizace detailList v masterRow
                        var detailsList = dataGridViewMaster.Rows[masterRowIndex].Tag as List<string[]>;
                        var existingDetail = detailsList.FirstOrDefault(d => d[7].Trim() == cestaKSouboru);
                        if (existingDetail != null)
                        {
                            existingDetail[8] = detail[8];
                            existingDetail[9] = detail[9];
                        }

                        break;
                    }
                }

                detailGrid.Refresh();
                SaveDataToJson("C:\\Users\\Adam\\Documents\\TUL\\SZZ\\BP\\data\\orders.json");
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

            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            DataGridViewRow row = detailGridView.Rows[e.RowIndex];
            DataGridViewCell cell = row.Cells[e.ColumnIndex];

            if (cell.OwningColumn.Name == "material")
            {
                string cestaKSouboru = row.Cells["cestaKSouboru"].Value?.ToString();
                if (vstupniMaterialy.TryGetValue(cestaKSouboru, out string vstupniMaterial) &&
                    vystupniMaterialy.TryGetValue(cestaKSouboru, out string vystupniMaterial) &&
                    vstupniMaterial != vystupniMaterial)
                {
                    e.CellStyle.BackColor = System.Drawing.Color.DeepPink;
                }
            }

            if (detailGridView.Columns["stavObjednavky"] != null &&
                e.ColumnIndex == detailGridView.Columns["stavObjednavky"].Index)
            {
                string stav = (string)detailGridView.Rows[e.RowIndex].Cells["stavObjednavky"].Value;
                e.CellStyle.BackColor = _orderManager.GetColorForStatus(stav);
            }
        }

        /// <summary>
        /// Metoda pro uložení upravení a uložení dat v detailGridu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void detailGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var detailGrid = sender as DataGridView;
            int masterRowIndex = Convert.ToInt32(detailGrid.Tag);

            Debug.WriteLine($"CellEndEdit: Řádek {e.RowIndex} v detailGridu byl upraven.");

            if (masterRowIndex < 0 || masterRowIndex >= dataGridViewMaster.Rows.Count)
            {
                Debug.WriteLine("Chyba: Index řádku masterGrid je mimo rozsah.");
                return;
            }

            var masterRow = dataGridViewMaster.Rows[masterRowIndex];
            if (masterRow.Tag is List<string[]> detailsList && e.RowIndex < detailsList.Count)
            {
                var detailRow = detailGrid.Rows[e.RowIndex];
                var vyrobeno = detailRow.Cells[6].Value?.ToString();
                var pocet = detailRow.Cells[3].Value?.ToString();
                var ohyb = detailRow.Cells[4].Value?.ToString();
                Debug.WriteLine($"Aktuální data: Vyrobeno = {vyrobeno}, Počet = {pocet}, Ohyb = {ohyb}");


                string novyStav = DetermineTheNewStatus(vyrobeno, pocet, ohyb);
                Debug.WriteLine($"Nový status objednávky: {novyStav}");

                var rowData = detailsList[e.RowIndex];
                rowData[8] = vyrobeno; 
                rowData[5] = pocet;
                rowData[6] = ohyb; 
                rowData[9] = novyStav; 

                detailRow.Cells["stavObjednavky"].Value = novyStav;

                var celkovýStav = DetermineOrderStatus(detailsList);
                dataGridViewMaster.Rows[masterRowIndex].Cells["stateOfOrder"].Value = celkovýStav;
                Debug.WriteLine($"Celkový status zakázky: {celkovýStav}");

                UpdateColorStatusOrders(dataGridViewMaster.Rows[masterRowIndex], celkovýStav);
                SaveDataToJson("C:\\Users\\Adam\\Documents\\TUL\\SZZ\\BP\\data\\orders.json");
            }
            else
            {
                Debug.WriteLine("Chyba: Řádek detailGrid nebyl nalezen v seznamu detailů.");
            }
        }


        private void UpdateColorStatusOrders(DataGridViewRow masterRow, string status)
        {
            if (status == "Hotovo")
            {
                masterRow.Cells[5].Style.BackColor = System.Drawing.Color.Green;
            }
            else if (status == "Rozpracováno")
            {
                masterRow.Cells[5].Style.BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                masterRow.Cells[5].Style.BackColor = System.Drawing.Color.LightGray;
            }
        }


        /// <summary>
        /// Metoda pro určení stavu zakázky po opětovnéím nahrání json souboru nebo editaci
        /// </summary>
        /// <param name="detailData"></param>
        /// <returns></returns>
        private string DetermineOrderStatus(List<string[]> detailData)
        {
            bool anyInProgressOrComplete = false;
            bool allDone = true;

            foreach (var row in detailData)
            {
                string status = row[9];

                if (status.Equals("Hotovo", StringComparison.OrdinalIgnoreCase))
                {
                    anyInProgressOrComplete = true; // alespoň jeden status "Hotovo"
                }
                else
                {
                    allDone = false;
                    if (status.Equals("Rozpracováno", StringComparison.OrdinalIgnoreCase) ||
                        status.Equals("Hotovo", StringComparison.OrdinalIgnoreCase))
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

        /// <summary>
        /// Metoda pro určení nového stavu objednávky po editu
        /// </summary>
        /// <param name="created"></param>
        /// <param name="amount"></param>
        /// <param name="curve"></param>
        /// <returns></returns>
        private string DetermineTheNewStatus(string created, string amount, string curve)
        {
            if (string.IsNullOrEmpty(created))
            {
                return "Neznámý";
            }

            if (int.TryParse(created, out int createdInt) && int.TryParse(amount, out int amountInt))
            {
                if (createdInt == amountInt)
                {
                    if (curve.Equals("NE", StringComparison.OrdinalIgnoreCase))
                    {
                        return "Hotovo";
                    }
                    else if (curve.Equals("ANO", StringComparison.OrdinalIgnoreCase))
                    {
                        return "Rozpracováno";
                    }
                }
                else if (createdInt < amountInt)
                {
                    return "Rozpracováno";
                }
                else if (createdInt > amountInt)
                {
                    return "Více kusů";
                }
            }

            return "Neplatný status";
        }

        /// <summary>
        /// Metoda pro uložení dat do JSON souboru
        /// </summary>
        /// <param name="filePath"></param>
        private void SaveDataToJson(string filePath)
        {
            var masterGridData = dataGridViewMaster.Rows
                .Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow)
                .Select(r => r.Cells.Cast<DataGridViewCell>().Select(c => c.Value?.ToString() ?? "").ToArray())
                .ToList();

            Debug.WriteLine("Získána data z hlavního DataGridView");


            var detailGridsData = new Dictionary<int, List<string[]>>();

            foreach (var kvp in detailGrids)
            {
                var rowIndex = kvp.Key;
                var detailGrid = kvp.Value;

                var masterRowData = dataGridViewMaster.Rows[rowIndex].Cells.Cast<DataGridViewCell>()
                    .Select(c => c.Value?.ToString() ?? "").ToArray();
                var customer = masterRowData[1]; // Zákazník
                var orderNumber = masterRowData[2]; // Číslo objednávky

                Debug.WriteLine($"Získána data pro detailní DataGridView na řádku {rowIndex}");


                var detailRows = detailGrid.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => !r.IsNewRow)
                    .Select(r =>
                        new[] { customer, orderNumber }
                            .Concat(r.Cells.Cast<DataGridViewCell>().Select(c => c.Value?.ToString() ?? "")).ToArray())
                    .ToList();

                Debug.WriteLine($"Získána data pro detailní DataGridView na řádku {rowIndex}");


                detailGridsData.Add(rowIndex, detailRows);
            }

            var data = new
            {
                MasterGridData = masterGridData,
                DetailGridsData = detailGridsData
            };

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, json);

            Debug.WriteLine("Data byla úspěšně uložena do JSON souboru");

            Debug.WriteLine("Zapsaná data:");
            Debug.WriteLine(json);
        }


        /// <summary>
        /// Metoda pro načtení dat z JSON souboru
        /// </summary>
        /// <param name="filePath"></param>
        private void LoadDataFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Soubor s daty neexistuje. Bude vytvořen nový soubor při ukončení aplikace.",
                    "Informace", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string json = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                MessageBox.Show("Soubor s daty je prázdný. Žádná data nebudou načtena.", "Informace",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var jsonData = JsonConvert.DeserializeObject<dynamic>(json);
                var masterGridData = jsonData.MasterGridData.ToObject<List<string[]>>();
                var detailGridsData = jsonData.DetailGridsData.ToObject<Dictionary<int, List<string[]>>>();

                // Načtení dat do dataGridViewMaster
                dataGridViewMaster.Rows.Clear();
                foreach (var rowArray in masterGridData)
                {
                    int rowIndex = dataGridViewMaster.Rows.Add(rowArray);
                    var masterRow = dataGridViewMaster.Rows[rowIndex];
                    masterRow.Tag = new List<string[]>();
                    dataGridViewMaster.Rows[rowIndex].Tag = new List<string[]>();
                    string orderStatus = rowArray[5];
                    _orderManager.SetOrderCellColor(dataGridViewMaster.Rows[rowIndex].Cells[5], orderStatus);
                    int dateColumnIndex = 3; 
                    _orderManager.HighlightOverdueDates(masterRow, dateColumnIndex);
                }

                foreach (var kvp in detailGridsData)
                {
                    int masterRowIndex = kvp.Key;
                    var detailsList = kvp.Value;

                    if (masterRowIndex < dataGridViewMaster.Rows.Count)
                    {
                        dataGridViewMaster.Rows[masterRowIndex].Tag = detailsList;
                        CreateAndShowDetailDataGridView(masterRowIndex, detailsList, false);

                        if (detailGrids.TryGetValue(masterRowIndex, out var existingDetailGrid))
                        {
                            UpdateDetailGrid(existingDetailGrid, detailsList);
                        }
                        else
                        {
                            var newDetailGrid = CreateAndShowDetailDataGridView(masterRowIndex, detailsList, false);
                            detailGrids[masterRowIndex] = newDetailGrid;
                        }
                    }
                }
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Chyba při načítání dat: {ex.Message}", "Chyba", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}