using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
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

        private bool isDetailRowBeingToggled = false;
        private DataGridView dataGridViewDetail;
        private List<string[]> csvLines = new List<string[]>();
        private Dictionary<int, DataGridView> detailGrids = new Dictionary<int, DataGridView>();


        public MainForm()
        {
            InitializeComponent();
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            NavPnl.Height = BtnDashboard.Height;
            NavPnl.Top = BtnDashboard.Top;
            NavPnl.Left = BtnDashboard.Left;
            BtnDashboard.BackColor = Color.FromArgb(46, 51, 73);
            dataGridViewMaster.CellContentClick += new DataGridViewCellEventHandler(dataGridViewMaster_CellContentClick);
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
            // P¯id·nÌ sloupce pro rozbalenÌ detail˘, pokud jeötÏ neexistuje
            if (!dataGridViewMaster.Columns.Contains("ExpandDetails"))
            {
                DataGridViewButtonColumn expandColumn = new DataGridViewButtonColumn();
                expandColumn.HeaderText = ""; // nebo "Rozbalit"
                expandColumn.Name = "ExpandDetails";
                expandColumn.Text = "+"; // m˘ûete pouûÌt i ikonu
                expandColumn.UseColumnTextForButtonValue = true;
                dataGridViewMaster.Columns.Insert(0, expandColumn);
            }
        }


        private void BtnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "d:\\Downloads";
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

                            // Filter out the unnecessary columns (for example, 7th and 8th columns)
                            var filteredFields = fields.Where((field, index) => index != 6 && index != 8).ToArray();

                            // Check if row already exists
                            if (!RowExists(fields[0], fields[1], fields[8]))
                            {
                                int rowIndex = dataGridViewMaster.Rows.Add();
                                dataGridViewMaster.Rows[rowIndex].Cells["Customer"].Value = fields[0].Trim();
                                dataGridViewMaster.Rows[rowIndex].Cells["NumOfOrder"].Value = fields[1].Trim();
                                dataGridViewMaster.Rows[rowIndex].Cells["Date"].Value = fields[8].Trim();

                                dataGridViewMaster.Rows[rowIndex].Tag = new List<string[]> { filteredFields };
                            }
                            else
                            {
                                AddDetailsToExistingRow(fields[0], fields[1], fields[8], filteredFields);
                            }
                        }
                    }
                }
            }
        }

        private bool RowExists(string customer, string orderNumber, string date)
        {
            foreach (DataGridViewRow row in dataGridViewMaster.Rows)
            {
                if (row.IsNewRow) continue; // P¯eskoËÌ nov˝ ¯·dek, kter˝ je vûdy p¯Ìtomen na konci DataGridView

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


        private void AddDetailsToExistingRow(string customer, string orderNumber, string date, string[] details)
            {
                foreach (DataGridViewRow row in dataGridViewMaster.Rows)
                {
                    if (row.Cells["Customer"].Value.ToString() == customer &&
                        row.Cells["NumOfOrder"].Value.ToString() == orderNumber &&
                        row.Cells["Date"].Value.ToString() == date)
                    {
                        var detailsList = (List<string[]>)row.Tag;
                        detailsList.Add(details);
                        break;
                    }
                }
            }




            // In your CellContentClick event, toggle the detail grid view like this:
            private void dataGridViewMaster_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridViewMaster.Columns[e.ColumnIndex].Name == "ExpandDetails")
            {
                if (detailGrids.ContainsKey(e.RowIndex) && detailGrids[e.RowIndex].Visible)
                {
                    // Hide the detail DataGridView
                    DisposeDetailDataGridView(e.RowIndex);
                }
                else
                {
                    // Show a new detail DataGridView
                    var detailsList = dataGridViewMaster.Rows[e.RowIndex].Tag as List<string[]>;
                    if (detailsList != null)
                    {
                        CreateAndShowDetailDataGridView(e.RowIndex, detailsList);
                    }
                }
            }
        }




        private void CreateAndShowDetailDataGridView(int rowIndex, List<string[]> detailsList)
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
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 9.75F),
                    BackColor = Color.AliceBlue,
                    ForeColor = Color.Black
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
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            };

            // Match the width of the master DataGridView
            detailDataGridView.Width = dataGridViewMaster.Width;

            // Add columns to the detail DataGridView (adjust the column names as necessary)
            detailDataGridView.Columns.Add("nazev", "N·zev");
            detailDataGridView.Columns.Add("material", "Materi·l");
            detailDataGridView.Columns.Add("tloustka", "Tlouöùka");
            detailDataGridView.Columns.Add("pocet", "PoËet");
            detailDataGridView.Columns.Add("ohyb", "Ohyb");
            detailDataGridView.Columns.Add("cestaKSouboru", "Cesta k souboru");

            // Add a single row with the details for now
            foreach (var detail in detailsList)
            {
                detailDataGridView.Rows.Add(detail.Skip(2).Take(detailDataGridView.Columns.Count).ToArray());
            }
            // Adjust the height of the DataGridView to fit the number of rows
            detailDataGridView.Height = (detailDataGridView.Rows.Count * detailDataGridView.RowTemplate.Height) + detailDataGridView.ColumnHeadersHeight;

            // Calculate the location to place the DataGridView below the selected master row
            int currentY = dataGridViewMaster.Location.Y + dataGridViewMaster.ColumnHeadersHeight;
            for (int i = 0; i <= rowIndex; i++)
            {
                currentY += dataGridViewMaster.Rows[i].Height;
            }
            detailDataGridView.Location = new Point(dataGridViewMaster.Location.X, currentY);

            // Store the DataGridView in the dictionary
            detailGrids[rowIndex] = detailDataGridView;

            // Add the DataGridView to the form's controls
            Controls.Add(detailDataGridView);
            detailDataGridView.BringToFront();

            // Update the button text to indicate collapse
            dataGridViewMaster.Rows[rowIndex].Cells["ExpandDetails"].Value = "-";
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
    }
}