using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BP_rizeni_zakazek
{
    public partial class settingsForm : Form
    {

        public string SelectedPathOrdAndLog { get; private set; }
        public string SelectedPathCsvZak { get; private set; }
        public string SelectedPathCsvHot { get; private set; }
        public bool IsPathOrdAndLogChanged { get; private set; }
        public bool IsPathCsvZakChanged { get; private set; }
        public bool IsPathCsvHotChanged { get; private set; }
        private protected Color defaultBackColor = Color.FromArgb(46, 51, 73);


        public settingsForm()
        {
            InitializeComponent();
            this.BackColor = Properties.Settings.Default.BackColor;
            UpdatePathsLabel();
            IsPathOrdAndLogChanged = false;
            IsPathCsvZakChanged = false;
            IsPathCsvHotChanged = false;
        }

        private void btnBrowseOrdAndLog_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                folderBrowser.Description = "Vybrat složku pro ukládání zakázek a logů";
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    SelectedPathOrdAndLog = folderBrowser.SelectedPath;
                    IsPathOrdAndLogChanged = true;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void UpdatePathsLabel()
        {
            textBox_OrdersPath.Text = Properties.Settings.Default.JsonFilePath;
            textBox_LogPath.Text = Properties.Settings.Default.LogFilePath;
            textBox_CsvZakPath.Text = Properties.Settings.Default.CsvZakFilePath;
            textBox_CsvHotPath.Text = Properties.Settings.Default.CsvHotFilePath;
        }

        private void btnCsvZak_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                folderBrowser.Description = "Vybrat složku pro vstupní CSV";
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    SelectedPathCsvZak = folderBrowser.SelectedPath;
                    IsPathCsvZakChanged = true;
                }
            }
        }

        private void btnCsvHot_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                folderBrowser.Description = "Vybrat složku pro výstupní CSV";
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    SelectedPathCsvHot = folderBrowser.SelectedPath;
                    IsPathCsvHotChanged = true;
                }
            }
        }

        private void btnChooseColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.BackColor = colorDialog.Color;
                this.BackColor = colorDialog.Color;
            }
        }

        private void btnResetColor_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.BackColor = defaultBackColor;
            this.BackColor = defaultBackColor;
            Properties.Settings.Default.Save();
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Uložení cesty k obrázku
                    Properties.Settings.Default.ImagePath = openFileDialog.FileName;
                    Properties.Settings.Default.Save();
                }
            }
        }
    }
}
