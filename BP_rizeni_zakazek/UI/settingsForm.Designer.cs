namespace BP_rizeni_zakazek
{
    partial class settingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            btnBrowseOrdAndLog = new Button();
            btnSave = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            textBox_OrdersPath = new TextBox();
            textBox_LogPath = new TextBox();
            label7 = new Label();
            textBox_CsvZakPath = new TextBox();
            textBox_CsvHotPath = new TextBox();
            btnCsvZak = new Button();
            btnCsvHot = new Button();
            colorDialog = new ColorDialog();
            btnChooseColor = new Button();
            btnResetColor = new Button();
            btnChooseImage = new Button();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(46, 51, 73);
            label1.Font = new Font("Microsoft Sans Serif", 27.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.Highlight;
            label1.Location = new Point(535, 9);
            label1.Name = "label1";
            label1.Size = new Size(337, 42);
            label1.TabIndex = 0;
            label1.Text = "Obecné nastavení";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // btnBrowseOrdAndLog
            // 
            btnBrowseOrdAndLog.BackColor = Color.LightGray;
            btnBrowseOrdAndLog.Cursor = Cursors.Hand;
            btnBrowseOrdAndLog.FlatAppearance.BorderSize = 0;
            btnBrowseOrdAndLog.FlatStyle = FlatStyle.Flat;
            btnBrowseOrdAndLog.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnBrowseOrdAndLog.Location = new Point(947, 194);
            btnBrowseOrdAndLog.Name = "btnBrowseOrdAndLog";
            btnBrowseOrdAndLog.Size = new Size(100, 30);
            btnBrowseOrdAndLog.TabIndex = 1;
            btnBrowseOrdAndLog.Text = "Procházet...";
            btnBrowseOrdAndLog.UseVisualStyleBackColor = false;
            btnBrowseOrdAndLog.Click += btnBrowseOrdAndLog_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(0, 123, 255);
            btnSave.Cursor = Cursors.Hand;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnSave.ForeColor = SystemColors.Control;
            btnSave.Location = new Point(486, 620);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(417, 40);
            btnSave.TabIndex = 2;
            btnSave.Text = "Uložit";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.White;
            label2.Location = new Point(340, 108);
            label2.Name = "label2";
            label2.Size = new Size(79, 17);
            label2.TabIndex = 3;
            label2.Text = "Orders.json:";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.White;
            label3.Location = new Point(323, 65);
            label3.Name = "label3";
            label3.Size = new Size(734, 20);
            label3.TabIndex = 5;
            label3.Text = "-----------------------------------------------Nastavení adresářů------------------------------------------------";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = Color.White;
            label4.Location = new Point(340, 247);
            label4.Name = "label4";
            label4.Size = new Size(81, 17);
            label4.TabIndex = 6;
            label4.Text = "Vstupní CSV:";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = Color.White;
            label5.Location = new Point(340, 338);
            label5.Name = "label5";
            label5.Size = new Size(87, 17);
            label5.TabIndex = 7;
            label5.Text = "Výstupní CSV:";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.White;
            label6.Location = new Point(330, 414);
            label6.Name = "label6";
            label6.Size = new Size(727, 20);
            label6.TabIndex = 8;
            label6.Text = "--------------------------------------------------------Ostatní------------------------------------------------------";
            // 
            // textBox_OrdersPath
            // 
            textBox_OrdersPath.BackColor = Color.White;
            textBox_OrdersPath.BorderStyle = BorderStyle.FixedSingle;
            textBox_OrdersPath.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_OrdersPath.Location = new Point(460, 106);
            textBox_OrdersPath.Multiline = true;
            textBox_OrdersPath.Name = "textBox_OrdersPath";
            textBox_OrdersPath.ReadOnly = true;
            textBox_OrdersPath.Size = new Size(587, 27);
            textBox_OrdersPath.TabIndex = 9;
            // 
            // textBox_LogPath
            // 
            textBox_LogPath.BackColor = Color.White;
            textBox_LogPath.BorderStyle = BorderStyle.FixedSingle;
            textBox_LogPath.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_LogPath.Location = new Point(460, 150);
            textBox_LogPath.Multiline = true;
            textBox_LogPath.Name = "textBox_LogPath";
            textBox_LogPath.ReadOnly = true;
            textBox_LogPath.Size = new Size(587, 27);
            textBox_LogPath.TabIndex = 10;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label7.ForeColor = Color.White;
            label7.Location = new Point(340, 152);
            label7.Name = "label7";
            label7.Size = new Size(60, 17);
            label7.TabIndex = 11;
            label7.Text = "Log.json:";
            label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // textBox_CsvZakPath
            // 
            textBox_CsvZakPath.BackColor = Color.White;
            textBox_CsvZakPath.BorderStyle = BorderStyle.FixedSingle;
            textBox_CsvZakPath.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_CsvZakPath.Location = new Point(460, 245);
            textBox_CsvZakPath.Multiline = true;
            textBox_CsvZakPath.Name = "textBox_CsvZakPath";
            textBox_CsvZakPath.Size = new Size(587, 27);
            textBox_CsvZakPath.TabIndex = 12;
            // 
            // textBox_CsvHotPath
            // 
            textBox_CsvHotPath.BackColor = Color.White;
            textBox_CsvHotPath.BorderStyle = BorderStyle.FixedSingle;
            textBox_CsvHotPath.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox_CsvHotPath.Location = new Point(460, 336);
            textBox_CsvHotPath.Multiline = true;
            textBox_CsvHotPath.Name = "textBox_CsvHotPath";
            textBox_CsvHotPath.Size = new Size(587, 27);
            textBox_CsvHotPath.TabIndex = 13;
            // 
            // btnCsvZak
            // 
            btnCsvZak.BackColor = Color.LightGray;
            btnCsvZak.Cursor = Cursors.Hand;
            btnCsvZak.FlatAppearance.BorderSize = 0;
            btnCsvZak.FlatStyle = FlatStyle.Flat;
            btnCsvZak.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnCsvZak.Location = new Point(947, 287);
            btnCsvZak.Name = "btnCsvZak";
            btnCsvZak.Size = new Size(100, 30);
            btnCsvZak.TabIndex = 14;
            btnCsvZak.Text = "Procházet...";
            btnCsvZak.UseVisualStyleBackColor = false;
            btnCsvZak.Click += btnCsvZak_Click;
            // 
            // btnCsvHot
            // 
            btnCsvHot.BackColor = Color.LightGray;
            btnCsvHot.Cursor = Cursors.Hand;
            btnCsvHot.FlatAppearance.BorderSize = 0;
            btnCsvHot.FlatStyle = FlatStyle.Flat;
            btnCsvHot.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnCsvHot.Location = new Point(947, 381);
            btnCsvHot.Name = "btnCsvHot";
            btnCsvHot.Size = new Size(100, 30);
            btnCsvHot.TabIndex = 15;
            btnCsvHot.Text = "Procházet...";
            btnCsvHot.UseVisualStyleBackColor = false;
            btnCsvHot.Click += btnCsvHot_Click;
            // 
            // btnChooseColor
            // 
            btnChooseColor.BackColor = Color.LightGray;
            btnChooseColor.Cursor = Cursors.Hand;
            btnChooseColor.FlatAppearance.BorderSize = 0;
            btnChooseColor.FlatStyle = FlatStyle.Flat;
            btnChooseColor.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnChooseColor.Location = new Point(606, 452);
            btnChooseColor.Name = "btnChooseColor";
            btnChooseColor.Size = new Size(100, 30);
            btnChooseColor.TabIndex = 16;
            btnChooseColor.Text = "Paleta barev";
            btnChooseColor.UseVisualStyleBackColor = false;
            btnChooseColor.Click += btnChooseColor_Click;
            // 
            // btnResetColor
            // 
            btnResetColor.BackColor = Color.DarkGray;
            btnResetColor.Cursor = Cursors.Hand;
            btnResetColor.FlatAppearance.BorderSize = 0;
            btnResetColor.FlatStyle = FlatStyle.Flat;
            btnResetColor.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnResetColor.Location = new Point(748, 452);
            btnResetColor.Name = "btnResetColor";
            btnResetColor.Size = new Size(100, 30);
            btnResetColor.TabIndex = 17;
            btnResetColor.Text = "Reset";
            btnResetColor.UseVisualStyleBackColor = false;
            btnResetColor.Click += btnResetColor_Click;
            // 
            // btnChooseImage
            // 
            btnChooseImage.BackColor = Color.LightGray;
            btnChooseImage.Cursor = Cursors.Hand;
            btnChooseImage.FlatAppearance.BorderSize = 0;
            btnChooseImage.FlatStyle = FlatStyle.Flat;
            btnChooseImage.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnChooseImage.Location = new Point(606, 508);
            btnChooseImage.Name = "btnChooseImage";
            btnChooseImage.Size = new Size(100, 30);
            btnChooseImage.TabIndex = 18;
            btnChooseImage.Text = "Procházet...";
            btnChooseImage.UseVisualStyleBackColor = false;
            btnChooseImage.Click += btnChooseImage_Click;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label8.ForeColor = Color.White;
            label8.Location = new Point(340, 459);
            label8.Name = "label8";
            label8.Size = new Size(143, 17);
            label8.TabIndex = 19;
            label8.Text = "Výběr barvy na pozadí:";
            label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label9.ForeColor = Color.White;
            label9.Location = new Point(340, 515);
            label9.Name = "label9";
            label9.Size = new Size(142, 17);
            label9.TabIndex = 20;
            label9.Text = "Výběr obrázku v menu:";
            label9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label10.ForeColor = Color.Khaki;
            label10.Location = new Point(648, 557);
            label10.Name = "label10";
            label10.Size = new Size(100, 21);
            label10.TabIndex = 21;
            label10.Text = "Upozornění";
            label10.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label11.ForeColor = Color.Khaki;
            label11.Location = new Point(511, 591);
            label11.Name = "label11";
            label11.Size = new Size(372, 17);
            label11.TabIndex = 22;
            label11.Text = "Pro aplikování všech změn je NUTNÉ kliknout na tlačítko uložit!";
            label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // settingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(46, 51, 73);
            ClientSize = new Size(1544, 911);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(btnChooseImage);
            Controls.Add(btnResetColor);
            Controls.Add(btnChooseColor);
            Controls.Add(btnCsvHot);
            Controls.Add(btnCsvZak);
            Controls.Add(textBox_CsvHotPath);
            Controls.Add(textBox_CsvZakPath);
            Controls.Add(label7);
            Controls.Add(textBox_LogPath);
            Controls.Add(textBox_OrdersPath);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnSave);
            Controls.Add(btnBrowseOrdAndLog);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "settingsForm";
            Text = "settingsForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnBrowseOrdAndLog;
        private Button btnSave;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox textBox_OrdersPath;
        private TextBox textBox_LogPath;
        private Label label7;
        private TextBox textBox_CsvZakPath;
        private TextBox textBox_CsvHotPath;
        private Button btnCsvZak;
        private Button btnCsvHot;
        private ColorDialog colorDialog;
        private Button btnChooseColor;
        private Button btnResetColor;
        private Button btnChooseImage;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
    }
}