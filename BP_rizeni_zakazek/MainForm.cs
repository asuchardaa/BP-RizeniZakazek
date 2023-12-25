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


        public MainForm()
        {
            InitializeComponent();
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            NavPnl.Height = BtnDashboard.Height;
            NavPnl.Top = BtnDashboard.Top;
            NavPnl.Left = BtnDashboard.Left;
            BtnDashboard.BackColor = Color.FromArgb(46, 51, 73);
            dataGridViewMaster.CellContentClick += new DataGridViewCellEventHandler(dataGridViewMaster_CellContentClick);

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
                    csvLines.Clear();
                    string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

                    if (lines.Length > 1)
                    {
                        foreach (string line in lines.Skip(1))
                        {
                            string[] fields = line.Split(',');
                            csvLines.Add(fields);

                            if (fields.Length > 9)
                            {
                                int rowIndex = dataGridViewMaster.Rows.Add();
                                dataGridViewMaster.Rows[rowIndex].Cells["Customer"].Value = fields[0].Trim();
                                dataGridViewMaster.Rows[rowIndex].Cells["NumOfOrder"].Value = fields[1].Trim();
                                dataGridViewMaster.Rows[rowIndex].Cells["Date"].Value = fields[8].Trim();

                                dataGridViewMaster.Rows[rowIndex].Tag = fields;
                            }
                        }
                    }
                }
            }
        }


        private void AddDetailsRow(int rowIndex, string[] details)
        {
            // Ensure that there are enough elements in the details array.
            if (details.Length >= 10) // Adjust this number based on the number of columns in your CSV file
            {
                // Select the specific fields to display
                var selectedDetails = new List<string>
                {
                    details[2], // nazev
                    details[3], // material
                    details[4], // tloustka
                    details[5], // pocet
                    details[7], // ohyb
                    details[9]  // cesta_k_souboru
                };

                // Create a new row for the details.
                DataGridViewRow detailsRow = new DataGridViewRow();
                detailsRow.CreateCells(dataGridViewMaster);

                // Combine the details into a single string for display.
                string detailsString = string.Join("; ", selectedDetails);
                detailsRow.Cells[1].Value = "Details: " + detailsString; // Display in the second cell

                // Set the style for the details row.
                detailsRow.DefaultCellStyle.BackColor = Color.LightGray;

                // Insert the detail row below the current row.
                dataGridViewMaster.Rows.Insert(rowIndex + 1, detailsRow);
            }
        }

        private void dataGridViewMaster_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridViewMaster.Columns[e.ColumnIndex].Name == "ShowOverview")
            {
                if (isDetailRowBeingToggled)
                {
                    return;
                }

                isDetailRowBeingToggled = true;
                DisposeDetailDataGridView();

                string[] details = csvLines[e.RowIndex];
                CreateAndShowDetailDataGridView(e.RowIndex, details);

                Timer timer = new Timer();
                timer.Interval = 100;
                timer.Tick += (s, args) =>
                {
                    isDetailRowBeingToggled = false;
                    timer.Stop();
                };
                timer.Start();
            }
        }

        private void CreateAndShowDetailDataGridView(int rowIndex, string[] details)
        {
            // Dispose of the existing detail DataGridView if it exists
            DisposeDetailDataGridView();

            // Create a new DataGridView for showing details
            dataGridViewDetail = new DataGridView
            {
                // Basic configuration - adjust as necessary
                Size = new Size(dataGridViewMaster.Width, 100), // Example size
                Location = new Point(dataGridViewMaster.Location.X,
                    dataGridViewMaster.Location.Y + (rowIndex + 1) * dataGridViewMaster.RowTemplate.Height),
                AllowUserToAddRows = false,
                ReadOnly = true
            };

            // Adding columns to the detail DataGridView
            dataGridViewDetail.Columns.Add("nazev", "N·zev");
            dataGridViewDetail.Columns.Add("material", "Materi·l");
            dataGridViewDetail.Columns.Add("tloustka", "Tlouöùka");
            dataGridViewDetail.Columns.Add("pocet", "PoËet");
            dataGridViewDetail.Columns.Add("ohyb", "Ohyb");
            dataGridViewDetail.Columns.Add("cestaKSouboru", "Cesta k souboru");

            // Adding a row with the details
            dataGridViewDetail.Rows.Add(details[2], details[3], details[4], details[5], details[7], details[9]);

            // Add the new DataGridView to the form (or a specific panel if you have one)
            this.Controls.Add(dataGridViewDetail);

            // Bring the new DataGridView to the front to ensure it's visible
            dataGridViewDetail.BringToFront();
        }


        private void DisposeDetailDataGridView()
        {
            if (dataGridViewDetail != null)
            {
                dataGridViewDetail.Dispose();
                dataGridViewDetail = null;
            }
        }



    }
}