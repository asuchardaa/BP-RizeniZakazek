namespace BP_rizeni_zakazek
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            panel1 = new Panel();
            BtnUpload = new Button();
            NavPnl = new Panel();
            BtnSettings = new Button();
            BtnArchive = new Button();
            BtnCalender = new Button();
            BtnStatistics = new Button();
            BtnDashboard = new Button();
            panel2 = new Panel();
            PnlNav = new Panel();
            label2 = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            dataGridViewMaster = new DataGridView();
            Customer = new DataGridViewTextBoxColumn();
            NumOfOrder = new DataGridViewTextBoxColumn();
            Date = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMaster).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(24, 30, 54);
            panel1.Controls.Add(BtnUpload);
            panel1.Controls.Add(NavPnl);
            panel1.Controls.Add(BtnSettings);
            panel1.Controls.Add(BtnArchive);
            panel1.Controls.Add(BtnCalender);
            panel1.Controls.Add(BtnStatistics);
            panel1.Controls.Add(BtnDashboard);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Left;
            panel1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(186, 577);
            panel1.TabIndex = 0;
            // 
            // BtnUpload
            // 
            BtnUpload.Location = new Point(48, 506);
            BtnUpload.Name = "BtnUpload";
            BtnUpload.Size = new Size(75, 23);
            BtnUpload.TabIndex = 6;
            BtnUpload.Text = "Upload";
            BtnUpload.UseVisualStyleBackColor = true;
            BtnUpload.Click += BtnUpload_Click;
            // 
            // NavPnl
            // 
            NavPnl.BackColor = SystemColors.Highlight;
            NavPnl.Location = new Point(0, 207);
            NavPnl.Name = "NavPnl";
            NavPnl.Size = new Size(3, 100);
            NavPnl.TabIndex = 4;
            // 
            // BtnSettings
            // 
            BtnSettings.Dock = DockStyle.Bottom;
            BtnSettings.FlatAppearance.BorderSize = 0;
            BtnSettings.FlatStyle = FlatStyle.Flat;
            BtnSettings.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            BtnSettings.ForeColor = Color.FromArgb(0, 126, 249);
            BtnSettings.Image = (Image)resources.GetObject("BtnSettings.Image");
            BtnSettings.Location = new Point(0, 535);
            BtnSettings.Name = "BtnSettings";
            BtnSettings.Size = new Size(186, 42);
            BtnSettings.TabIndex = 5;
            BtnSettings.Text = "Settings";
            BtnSettings.TextImageRelation = TextImageRelation.TextBeforeImage;
            BtnSettings.UseVisualStyleBackColor = true;
            BtnSettings.Click += BtnSettings_Click;
            BtnSettings.Leave += BtnSettings_Leave;
            // 
            // BtnArchive
            // 
            BtnArchive.Dock = DockStyle.Top;
            BtnArchive.FlatAppearance.BorderSize = 0;
            BtnArchive.FlatStyle = FlatStyle.Flat;
            BtnArchive.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            BtnArchive.ForeColor = Color.FromArgb(0, 126, 249);
            BtnArchive.Image = (Image)resources.GetObject("BtnArchive.Image");
            BtnArchive.Location = new Point(0, 285);
            BtnArchive.Name = "BtnArchive";
            BtnArchive.Size = new Size(186, 42);
            BtnArchive.TabIndex = 4;
            BtnArchive.Text = "Archive";
            BtnArchive.TextImageRelation = TextImageRelation.TextBeforeImage;
            BtnArchive.UseVisualStyleBackColor = true;
            BtnArchive.Click += BtnArchive_Click;
            BtnArchive.Leave += BtnArchive_Leave;
            // 
            // BtnCalender
            // 
            BtnCalender.Dock = DockStyle.Top;
            BtnCalender.FlatAppearance.BorderSize = 0;
            BtnCalender.FlatStyle = FlatStyle.Flat;
            BtnCalender.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            BtnCalender.ForeColor = Color.FromArgb(0, 126, 249);
            BtnCalender.Image = (Image)resources.GetObject("BtnCalender.Image");
            BtnCalender.Location = new Point(0, 243);
            BtnCalender.Name = "BtnCalender";
            BtnCalender.Size = new Size(186, 42);
            BtnCalender.TabIndex = 3;
            BtnCalender.Text = "Calender";
            BtnCalender.TextImageRelation = TextImageRelation.TextBeforeImage;
            BtnCalender.UseVisualStyleBackColor = true;
            BtnCalender.Click += BtnCalender_Click;
            BtnCalender.Leave += BtnCalender_Leave;
            // 
            // BtnStatistics
            // 
            BtnStatistics.Dock = DockStyle.Top;
            BtnStatistics.FlatAppearance.BorderSize = 0;
            BtnStatistics.FlatStyle = FlatStyle.Flat;
            BtnStatistics.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            BtnStatistics.ForeColor = Color.FromArgb(0, 126, 249);
            BtnStatistics.Image = (Image)resources.GetObject("BtnStatistics.Image");
            BtnStatistics.Location = new Point(0, 201);
            BtnStatistics.Name = "BtnStatistics";
            BtnStatistics.Size = new Size(186, 42);
            BtnStatistics.TabIndex = 2;
            BtnStatistics.Text = "Statistics";
            BtnStatistics.TextImageRelation = TextImageRelation.TextBeforeImage;
            BtnStatistics.UseVisualStyleBackColor = true;
            BtnStatistics.Click += BtnStatistics_Click;
            BtnStatistics.Leave += BtnStatistics_Leave;
            // 
            // BtnDashboard
            // 
            BtnDashboard.Dock = DockStyle.Top;
            BtnDashboard.FlatAppearance.BorderSize = 0;
            BtnDashboard.FlatStyle = FlatStyle.Flat;
            BtnDashboard.Font = new Font("Nirmala UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            BtnDashboard.ForeColor = Color.FromArgb(0, 126, 249);
            BtnDashboard.Image = (Image)resources.GetObject("BtnDashboard.Image");
            BtnDashboard.Location = new Point(0, 159);
            BtnDashboard.Name = "BtnDashboard";
            BtnDashboard.Size = new Size(186, 42);
            BtnDashboard.TabIndex = 1;
            BtnDashboard.Text = "Dashboard";
            BtnDashboard.TextImageRelation = TextImageRelation.TextBeforeImage;
            BtnDashboard.UseVisualStyleBackColor = true;
            BtnDashboard.Click += BtnDashboard_Click;
            BtnDashboard.Leave += BtnDashboard_Leave;
            // 
            // panel2
            // 
            panel2.Controls.Add(PnlNav);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(pictureBox1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(186, 159);
            panel2.TabIndex = 0;
            // 
            // PnlNav
            // 
            PnlNav.BackColor = Color.FromArgb(0, 126, 249);
            PnlNav.Location = new Point(0, 193);
            PnlNav.Name = "PnlNav";
            PnlNav.Size = new Size(3, 100);
            PnlNav.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.FromArgb(158, 161, 178);
            label2.Location = new Point(30, 128);
            label2.Name = "label2";
            label2.Size = new Size(126, 13);
            label2.TabIndex = 2;
            label2.Text = "Some User Text here";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(0, 126, 249);
            label1.Location = new Point(48, 100);
            label1.Name = "label1";
            label1.Size = new Size(85, 16);
            label1.TabIndex = 1;
            label1.Text = "User Name";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.user_icon;
            pictureBox1.Location = new Point(60, 22);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(63, 63);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // dataGridViewMaster
            // 
            dataGridViewMaster.BackgroundColor = SystemColors.ActiveCaption;
            dataGridViewMaster.BorderStyle = BorderStyle.None;
            dataGridViewMaster.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.ButtonShadow;
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.ControlLightLight;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridViewMaster.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewMaster.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewMaster.Columns.AddRange(new DataGridViewColumn[] { Customer, NumOfOrder, Date });
            dataGridViewMaster.EnableHeadersVisualStyles = false;
            dataGridViewMaster.GridColor = SystemColors.ScrollBar;
            dataGridViewMaster.Location = new Point(209, 38);
            dataGridViewMaster.Name = "dataGridViewMaster";
            dataGridViewMaster.RowTemplate.Height = 25;
            dataGridViewMaster.Size = new Size(929, 427);
            dataGridViewMaster.TabIndex = 1;
            // 
            // Customer
            // 
            Customer.HeaderText = "Zákazník";
            Customer.Name = "Customer";
            Customer.Width = 222;
            // 
            // NumOfOrder
            // 
            NumOfOrder.HeaderText = "Číslo objednávky";
            NumOfOrder.Name = "NumOfOrder";
            NumOfOrder.Width = 222;
            // 
            // Date
            // 
            Date.HeaderText = "Datum";
            Date.Name = "Date";
            Date.Width = 222;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(46, 51, 73);
            ClientSize = new Size(1159, 577);
            Controls.Add(dataGridViewMaster);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DU-PE - Řízení zakázek";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMaster).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private PictureBox pictureBox1;
        private Label label1;
        private Button BtnDashboard;
        private Label label2;
        private Button BtnSettings;
        private Button BtnArchive;
        private Button BtnCalender;
        private Button BtnStatistics;
        private Panel PnlNav;
        private Panel NavPnl;
        private Button BtnUpload;
        private DataGridView dataGridViewMaster;
        private DataGridViewTextBoxColumn Customer;
        private DataGridViewTextBoxColumn NumOfOrder;
        private DataGridViewTextBoxColumn Date;
    }
}