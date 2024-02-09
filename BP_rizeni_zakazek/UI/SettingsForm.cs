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
    /// <summary>
    /// Třída pro formulář s nastavením aplikace
    /// </summary>
    public partial class settingsForm : Form
    {

        public string SelectedPathOrdAndLog { get; private set; }
        public string SelectedPathCsvZak { get; private set; }
        public string SelectedPathCsvHot { get; private set; }
        public bool IsPathOrdAndLogChanged { get; private set; }
        public bool IsPathCsvZakChanged { get; private set; }
        public bool IsPathCsvHotChanged { get; private set; }
        public Color defaultBackColor = Color.FromArgb(46, 51, 73);

        /// <summary>
        /// Konstruktor třídy
        /// </summary>
        public settingsForm()
        {
            InitializeComponent();

            if (Properties.Settings.Default.BackColor == System.Drawing.Color.Empty)
            {
                Properties.Settings.Default.BackColor = defaultBackColor;
                Properties.Settings.Default.Save();
            }

            this.BackColor = Properties.Settings.Default.BackColor;
            UpdatePathsLabel();
            IsPathOrdAndLogChanged = false;
            IsPathCsvZakChanged = false;
            IsPathCsvHotChanged = false;
        }

        /// <summary>
        /// Metoda pro tlačítko pro výběr složky pro ukládání zakázek a logů
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Metoda pro tlačítko pro uložení změn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Metoda pro tlačítko pro aktualizaci změn
        /// </summary>
        private void UpdatePathsLabel()
        {
            textBox_OrdersPath.Text = Properties.Settings.Default.JsonFilePath;
            textBox_LogPath.Text = Properties.Settings.Default.LogFilePath;
            textBox_CsvZakPath.Text = Properties.Settings.Default.CsvZakFilePath;
            textBox_CsvHotPath.Text = Properties.Settings.Default.CsvHotFilePath;
        }

        /// <summary>
        /// Metoda pro tlačítko pro výběr složky pro vstupní CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Metoda pro tlačítko pro výběr složky pro výstupní CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Metoda pro tlačítko pro výběr barvy pozadí
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.BackColor = colorDialog.Color;
                this.BackColor = colorDialog.Color;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Metoda pro tlačítko pro reset barvy pozadí
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnResetColor_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.BackColor = defaultBackColor;
            this.BackColor = defaultBackColor;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Metoda pro tlačítko pro výběr obrázku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
