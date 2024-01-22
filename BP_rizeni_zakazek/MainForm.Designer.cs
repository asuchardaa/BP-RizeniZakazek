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
            OrderDoneOrNotDone = new ComboBox();
            FilterTextBox = new TextBox();
            BtnUploadHot = new Button();
            BtnUpload = new Button();
            NavPnl = new Panel();
            BtnSettings = new Button();
            BtnArchive = new Button();
            BtnCalender = new Button();
            BtnStatistics = new Button();
            BtnDashboard = new Button();
            panel2 = new Panel();
            PnlNav = new Panel();
            OrganizationName = new Label();
            userName = new Label();
            pictureBox1 = new PictureBox();
            dataGridViewMaster = new DataGridView();
            Customer = new DataGridViewTextBoxColumn();
            NumOfOrder = new DataGridViewTextBoxColumn();
            Date = new DataGridViewTextBoxColumn();
            dateOfFinish = new DataGridViewTextBoxColumn();
            stateOfOrder = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMaster).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(24, 30, 54);
            panel1.Controls.Add(OrderDoneOrNotDone);
            panel1.Controls.Add(FilterTextBox);
            panel1.Controls.Add(BtnUploadHot);
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
            panel1.Size = new Size(186, 633);
            panel1.TabIndex = 0;
            // 
            // OrderDoneOrNotDone
            // 
            OrderDoneOrNotDone.FormattingEnabled = true;
            OrderDoneOrNotDone.Location = new Point(12, 518);
            OrderDoneOrNotDone.Name = "OrderDoneOrNotDone";
            OrderDoneOrNotDone.Size = new Size(121, 23);
            OrderDoneOrNotDone.TabIndex = 9;
            OrderDoneOrNotDone.SelectedIndexChanged += comboBox1_OrderDoneOrNotDone;
            // 
            // FilterTextBox
            // 
            FilterTextBox.Location = new Point(12, 478);
            FilterTextBox.Name = "FilterTextBox";
            FilterTextBox.Size = new Size(121, 23);
            FilterTextBox.TabIndex = 8;
            FilterTextBox.TextChanged += FilterTextBox_TextChanged;
            // 
            // BtnUploadHot
            // 
            BtnUploadHot.Location = new Point(93, 562);
            BtnUploadHot.Name = "BtnUploadHot";
            BtnUploadHot.Size = new Size(75, 23);
            BtnUploadHot.TabIndex = 7;
            BtnUploadHot.Text = "UploadHot";
            BtnUploadHot.UseVisualStyleBackColor = true;
            BtnUploadHot.Click += BtnUploadHot_Click;
            // 
            // BtnUpload
            // 
            BtnUpload.Location = new Point(12, 562);
            BtnUpload.Name = "BtnUpload";
            BtnUpload.Size = new Size(75, 23);
            BtnUpload.TabIndex = 6;
            BtnUpload.Text = "UploadZak";
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
            BtnSettings.Location = new Point(0, 591);
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
            panel2.Controls.Add(OrganizationName);
            panel2.Controls.Add(userName);
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
            // OrganizationName
            // 
            OrganizationName.AutoSize = true;
            OrganizationName.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            OrganizationName.ForeColor = Color.FromArgb(158, 161, 178);
            OrganizationName.Location = new Point(37, 127);
            OrganizationName.Name = "OrganizationName";
            OrganizationName.Size = new Size(109, 13);
            OrganizationName.TabIndex = 2;
            OrganizationName.Text = "Jméno organizace";
            // 
            // userName
            // 
            userName.AutoSize = true;
            userName.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            userName.ForeColor = Color.FromArgb(0, 126, 249);
            userName.Location = new Point(45, 98);
            userName.Name = "userName";
            userName.Size = new Size(88, 16);
            userName.TabIndex = 1;
            userName.Text = "User/Admin";
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
            dataGridViewMaster.AllowUserToAddRows = false;
            dataGridViewMaster.BackgroundColor = SystemColors.ActiveCaption;
            dataGridViewMaster.BorderStyle = BorderStyle.None;
            dataGridViewMaster.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.ButtonShadow;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.ControlLightLight;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridViewMaster.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewMaster.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewMaster.Columns.AddRange(new DataGridViewColumn[] { Customer, NumOfOrder, Date, dateOfFinish, stateOfOrder });
            dataGridViewMaster.EnableHeadersVisualStyles = false;
            dataGridViewMaster.GridColor = SystemColors.ScrollBar;
            dataGridViewMaster.Location = new Point(224, 20);
            dataGridViewMaster.Name = "dataGridViewMaster";
            dataGridViewMaster.RowTemplate.Height = 25;
            dataGridViewMaster.Size = new Size(1112, 601);
            dataGridViewMaster.TabIndex = 1;
            // 
            // Customer
            // 
            Customer.HeaderText = "Zákazník";
            Customer.Name = "Customer";
            Customer.Width = 215;
            // 
            // NumOfOrder
            // 
            NumOfOrder.HeaderText = "Číslo objednávky";
            NumOfOrder.Name = "NumOfOrder";
            NumOfOrder.Width = 215;
            // 
            // Date
            // 
            Date.HeaderText = "Datum";
            Date.Name = "Date";
            Date.Width = 198;
            // 
            // dateOfFinish
            // 
            dateOfFinish.HeaderText = "Dokončeno";
            dateOfFinish.Name = "dateOfFinish";
            dateOfFinish.Width = 198;
            // 
            // stateOfOrder
            // 
            stateOfOrder.HeaderText = "Stav";
            stateOfOrder.Name = "stateOfOrder";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(46, 51, 73);
            ClientSize = new Size(1370, 633);
            Controls.Add(dataGridViewMaster);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DU-PE - Řízení zakázek";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
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
        private Label userName;
        private Button BtnDashboard;
        private Label OrganizationName;
        private Button BtnSettings;
        private Button BtnArchive;
        private Button BtnCalender;
        private Button BtnStatistics;
        private Panel PnlNav;
        private Panel NavPnl;
        private Button BtnUpload;
        private Button BtnUploadHot;
        private ComboBox OrderDoneOrNotDone;
        private TextBox FilterTextBox;
        private DataGridView dataGridViewMaster;
        private DataGridViewTextBoxColumn Customer;
        private DataGridViewTextBoxColumn NumOfOrder;
        private DataGridViewTextBoxColumn Date;
        private DataGridViewTextBoxColumn dateOfFinish;
        private DataGridViewTextBoxColumn stateOfOrder;
    }
}