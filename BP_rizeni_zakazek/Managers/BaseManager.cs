using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP_rizeni_zakazek.Managers
{
    /// <summary>
    /// Abstraktní třída pro metody spojené s prací s objednávkami
    /// </summary>
    public abstract class BaseManager
    {
        public abstract string CalculateOverallStatus(DataGridViewRow masterRow, DataGridView gridView);
        public abstract void SetOrderCellColor(DataGridViewCell cell, string status);
        public abstract void UpdateColorStatusOrders(DataGridViewRow masterRow, string status);
        public abstract void UpdateDateOfFinish(DataGridViewRow row, string status);
        public abstract void HighlightOverdueDates(DataGridViewRow row, int dateColumnIndex);
        public abstract string DetermineOrderStatus(string created, string originalAmount, string curve);
        public abstract Color GetColorForStatus(string status);
        public abstract string DetermineOrderStatusList(List<string[]> detailData);
        public abstract string DetermineTheNewStatus(string created, string amount, string curve);
    }
}
