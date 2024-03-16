using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Castle.Core.Resource;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Title = System.Windows.Forms.DataVisualization.Charting.Title;

namespace BP_rizeni_zakazek.UI
{
    /// <summary>
    /// Třída pro formulář statistik
    /// </summary>
    public partial class StatisticsForm : Form
    {
        private List<string> customers = new List<string>();
        private List<string> dates = new List<string>();
        private List<string> states = new List<string>();

        private protected List<string> delayedOrders;
        private protected List<string> currentOrders;

        public StatisticsForm()
        {
            InitializeComponent();
            LoadChartData();
            ProcessOrderData();
            CreatePieChart();
            CreateColumnChart();
            CreateHorizontalBar();
            this.BackColor = Properties.Settings.Default.BackColor;
        }

        /// <summary>
        /// Metoda pro načtení dat (stavů) pro graf 
        /// </summary>
        private void LoadChartData()
        {
            DataGridView masterGrid = (Application.OpenForms["MainForm"] as MainForm)?.dataGridViewMaster;

            if (masterGrid != null)
            {
                states = masterGrid.Rows.Cast<DataGridViewRow>()
                    .Select(row => row.Cells["stateOfOrder"].Value?.ToString())
                    .Where(state => !string.IsNullOrEmpty(state))
                    .ToList();

                dates = masterGrid.Rows.Cast<DataGridViewRow>()
                    .Select(row => row.Cells["Date"].Value?.ToString())
                    .Where(state => !string.IsNullOrEmpty(state))
                    .ToList();

                customers = masterGrid.Rows.Cast<DataGridViewRow>()
                    .Select(row => row.Cells["Customer"].Value?.ToString())
                    .Where(state => !string.IsNullOrEmpty(state))
                    .ToList();

                foreach (var customer in customers)
                {
                    Debug.WriteLine(customer);
                }
            }
        }

        /// <summary>
        /// Metoda pro tvorbu koláčového grafu
        /// </summary>
        private void CreatePieChart()
        {
            chart1.ChartAreas.Clear();
            chart1.Series.Clear();

            Title chartTitle = new Title("Stav jednotlivých zakázek", Docking.Top,
                new Font("Segoe UI", 20, FontStyle.Bold), Color.FromArgb(64, 64, 64));
            chart1.Titles.Add(chartTitle);

            ChartArea chartArea = new ChartArea();
            chartArea.BorderDashStyle = ChartDashStyle.Solid;
            chartArea.BorderWidth = 1;
            chartArea.BorderColor = Color.FromArgb(200, 200, 200);
            chartArea.BackColor = Color.WhiteSmoke;
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            chartArea.AxisY.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            chart1.ChartAreas.Add(chartArea);

            Series series = new Series("Orders");
            series.ChartType = SeriesChartType.Pie;
            series.ChartArea = chartArea.Name;
            series.BorderColor = Color.White;
            series.BorderWidth = 1;

            Dictionary<string, int> stateCounts = new Dictionary<string, int>();
            foreach (var state in states)
            {
                if (stateCounts.ContainsKey(state))
                {
                    stateCounts[state]++;
                }
                else
                {
                    stateCounts[state] = 1;
                }
            }

            double total = stateCounts.Values.Sum();

            foreach (var kvp in stateCounts)
            {
                double percentage = (100d * kvp.Value) / total;

                DataPoint dataPoint = new DataPoint();
                dataPoint.SetValueXY(kvp.Key, percentage);
                dataPoint.Label = $"{kvp.Key}\n{percentage.ToString("00.00")}%";
                dataPoint.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                if (kvp.Key == "Hotovo")
                {
                    dataPoint.Color = Color.FromArgb(92, 184, 92);
                }
                else if (kvp.Key == "Rozpracováno")
                {
                    dataPoint.Color = Color.FromArgb(255, 255, 92);
                }
                else if (kvp.Key == "Více kusů")
                {
                    dataPoint.Color = Color.FromArgb(255, 165, 0);
                }
                else
                {
                    dataPoint.Color = Color.FromArgb(173, 216, 230);
                }

                series.Points.Add(dataPoint);
            }

            chart1.Series.Add(series);
        }

        /// <summary>
        /// Metoda pro pročesání dat a rozdělení zakázek do dvou seznamů dle datumu
        /// </summary>
        private void ProcessOrderData()
        {
            DateTime currentDate = DateTime.Now;

            delayedOrders = new List<string>();
            currentOrders = new List<string>();

            foreach (var orderDate in dates)
            {
                DateTime parsedDate;

                if (DateTime.TryParse(orderDate, out parsedDate))
                {
                    if (parsedDate < currentDate)
                    {
                        delayedOrders.Add(orderDate);
                        Debug.WriteLine("Pridavam do delayed" + orderDate);
                    }
                    else
                    {
                        currentOrders.Add(orderDate);
                        Debug.WriteLine("Pridavam do current" + orderDate);
                    }
                }
            }
        }

        /// <summary>
        /// Metoda pro tvoirbu sloupcového grafu
        /// </summary>
        private void CreateColumnChart()
        {
            chart2.ChartAreas.Clear();
            chart2.Series.Clear();

            Title chartTitle = new Title("Zakázky dle zpoždění", Docking.Top, new Font("Segoe UI", 20, FontStyle.Bold),
                Color.FromArgb(64, 64, 64));
            chart2.Titles.Add(chartTitle);

            ChartArea chartArea = new ChartArea();
            chartArea.BorderDashStyle = ChartDashStyle.Solid;
            chartArea.BorderWidth = 1;
            chartArea.BorderColor = Color.FromArgb(200, 200, 200);
            chartArea.BackColor = Color.WhiteSmoke;
            chartArea.AxisX.Title = "Stavy";
            chartArea.AxisY.Title = "Počet zakázek";
            chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            chartArea.AxisY.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            chartArea.AxisX.TitleFont = new Font("Segoe UI", 12, FontStyle.Bold);
            chartArea.AxisY.TitleFont = new Font("Segoe UI", 12, FontStyle.Bold);
            chart2.ChartAreas.Add(chartArea);

            DataPoint delayedPoint = new DataPoint(0, delayedOrders.Count);
            delayedPoint.AxisLabel = "Zpožděné";
            delayedPoint["PointWidth"] = "0.6";
            delayedPoint["PointGap"] = "1.5";

            DataPoint currentPoint = new DataPoint(1, currentOrders.Count);
            currentPoint.AxisLabel = "V termínu";
            currentPoint["PointWidth"] = "0.6";
            currentPoint["PointGap"] = "1.5";

            Series delayedSeries = new Series("Zpožděné");
            delayedSeries.ChartType = SeriesChartType.Column;
            delayedSeries.ChartArea = chartArea.Name;
            delayedSeries.Color = Color.FromArgb(255, 92, 92);
            delayedSeries.Points.Add(delayedPoint);
            delayedSeries.IsVisibleInLegend = false;
            chart2.Series.Add(delayedSeries);

            Series currentSeries = new Series("V termínu");
            currentSeries.ChartType = SeriesChartType.Column;
            currentSeries.ChartArea = chartArea.Name;
            currentSeries.Color = Color.FromArgb(173, 216, 230);
            currentSeries.Points.Add(currentPoint);
            currentSeries.IsVisibleInLegend = false;
            chart2.Series.Add(currentSeries);

            chart2.Legends.Add("DateLegend");
            chart2.Legends["DateLegend"].CustomItems
                .Add(Color.FromArgb(255, 92, 92), $"Zpožděné: {delayedOrders.Count}");
            chart2.Legends["DateLegend"].CustomItems
                .Add(Color.FromArgb(173, 216, 230), $"V termínu: {currentOrders.Count}");

            foreach (DataPoint point in delayedSeries.Points)
            {
                point.Label = "";
            }

            foreach (DataPoint point in currentSeries.Points)
            {
                point.Label = "";
            }
        }

        /// <summary>
        /// Metoda pro horizontální graf
        /// </summary>
        private void CreateHorizontalBar()
        {
            chart3.ChartAreas.Clear();
            chart3.Series.Clear();

            Title chartTitle = new Title("Zákazníci x počet zakázek", Docking.Top,
                new Font("Segoe UI", 20, FontStyle.Bold),
                Color.FromArgb(64, 64, 64));
            chart3.Titles.Add(chartTitle);

            ChartArea chartArea = new ChartArea();
            chartArea.BorderDashStyle = ChartDashStyle.Solid;
            chartArea.BorderWidth = 1;
            chartArea.BorderColor = Color.FromArgb(200, 200, 200);
            chartArea.BackColor = Color.WhiteSmoke;
            chartArea.AxisX.Title = "Zákazníci";
            chartArea.AxisY.Title = "Počet zakázek";
            chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            chartArea.AxisY.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            chartArea.AxisX.TitleFont = new Font("Segoe UI", 12, FontStyle.Bold);
            chartArea.AxisY.TitleFont = new Font("Segoe UI", 12, FontStyle.Bold);

            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.IntervalOffset = 1;
            chart3.ChartAreas.Add(chartArea);

            Dictionary<string, int> customerCounts = new Dictionary<string, int>();

            foreach (string customer in customers)
            {
                if (customerCounts.ContainsKey(customer))
                {
                    customerCounts[customer]++;
                }
                else
                {
                    customerCounts[customer] = 1;
                }
            }

            double xValue = 0;

            foreach (var kvp in customerCounts)
            {
                Series series = new Series(kvp.Key);
                series.ChartType = SeriesChartType.RangeBar;
                series.ChartArea = chartArea.Name;
                series.Color = GetRandomColor();
                series.IsVisibleInLegend = false;

                DataPoint dataPoint = new DataPoint();
                dataPoint.SetValueXY(kvp.Key, kvp.Value);
                dataPoint["PointWidth"] = "0.5";
                dataPoint["PointGap"] = "1";
                series.Points.Add(dataPoint);

                Legend legend = new Legend(kvp.Key);
                legend.CustomItems.Add(series.Color, $"{kvp.Key}: {kvp.Value}");
                legend.Docking = Docking.Bottom;
                legend.IsDockedInsideChartArea = true;
                chart3.Legends.Add(legend);
                chart3.Series.Add(series);
            }
        }

        /// <summary>
        /// Metoda pro random barvu
        /// </summary>
        /// <returns></returns>
        private Color GetRandomColor()
        {
            Random rand = new Random();
            return Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
        }
    }
}