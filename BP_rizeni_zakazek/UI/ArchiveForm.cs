using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BP_rizeni_zakazek.UI
{
    /// <summary>
    /// Třída pro formulář archivu
    /// </summary>
    public partial class ArchiveForm : Form
    {
        public ArchiveForm()
        {
            InitializeComponent();
            InitializeDataGridView();
            this.BackColor = Properties.Settings.Default.BackColor;
        }

        /// <summary>
        /// Metoda pro inicializaci DataGridView archivu
        /// </summary>
        public void InitializeDataGridView()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.BackgroundColor = SystemColors.ActiveCaption;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.ButtonShadow;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.ControlLightLight;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = SystemColors.ScrollBar;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 30;
            dataGridView1.ColumnHeadersHeight = 50;
            dataGridView1.ScrollBars = ScrollBars.Both;
            dataGridView1.GridColor = System.Drawing.Color.DarkGray;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridView1.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.RoyalBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sans-Serif", 13);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 10);

            if (!dataGridView1.Columns.Contains("DateOfFinishColumn"))
            {
                var dateOfFinishColumn = new DataGridViewTextBoxColumn
                {
                    Name = "DateOfFinishColumn",
                    HeaderText = "Datum Dokončení",
                    Width = 50
                };
                dataGridView1.Columns.Add(dateOfFinishColumn);
            }

            var customerColumn = new DataGridViewTextBoxColumn
            {
                Name = "Customer",
                HeaderText = "Zákazník",
                Width = 50
            };
            dataGridView1.Columns.Add(customerColumn);

            var numOfOrderColumn = new DataGridViewTextBoxColumn
            {
                Name = "NumOfOrder",
                HeaderText = "Číslo objednávky",
                Width = 50
            };
            dataGridView1.Columns.Add(numOfOrderColumn);

            var dateColumn = new DataGridViewTextBoxColumn
            {
                Name = "Date",
                HeaderText = "Datum",
                Width = 50
            };
            dataGridView1.Columns.Add(dateColumn);

            var stateOfOrderColumn = new DataGridViewTextBoxColumn
            {
                Name = "StateOfOrder",
                HeaderText = "Stav",
                Width = 50
            };
            dataGridView1.Columns.Add(stateOfOrderColumn);
        }
    }
}
