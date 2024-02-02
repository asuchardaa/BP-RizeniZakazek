using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP_rizeni_zakazek.Managers;


namespace BP_rizeni_zakazek.Helpers
{
    /// <summary>
    /// Třída pro pomocné metody spojené s DataGridy
    /// </summary>
    internal class DataGridHelper
    {
        private OrderManager _orderManager = new OrderManager();

        /// <summary>
        /// Metoda pro odstranění specifického řádku z DataGridView
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="cisloObjednavky"></param>
        public void DeleteSpecifiedRow(DataGridView gridView, string cisloObjednavky)
        {
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (!row.IsNewRow && row.Cells["NumOfOrder"].Value.ToString() == cisloObjednavky)
                {
                    gridView.Rows.Remove(row);
                    break;
                }
            }
        }

        /// <summary>
        /// Metoda pro aktualizaci celkového stavu zakázky
        /// </summary>
        public void UpdateAllMasterGridRowStatuses(DataGridView gridView)
        {
            foreach (DataGridViewRow masterRow in gridView.Rows)
            {
                if (masterRow.IsNewRow) continue;

                string overallStatus = _orderManager.CalculateOverallStatus(masterRow, gridView);
                masterRow.Cells["stateOfOrder"].Value = overallStatus;
            }
        }

        /// <summary>
        /// Metoda pro najití nebo přidání master řádku
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public int FindOrAddMasterRow(DataGridView gridView, string[] fields)
        {
            string customer = fields[0].Trim();
            string orderNumber = fields[1].Trim();
            string date = fields[8].Trim();

            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                var customerCell = gridView.Rows[i].Cells["Customer"].Value;
                var orderNumberCell = gridView.Rows[i].Cells["NumOfOrder"].Value;

                if (customerCell != null && orderNumberCell != null &&
                    customerCell.ToString() == customer &&
                    orderNumberCell.ToString() == orderNumber)
                {
                    return i;
                }
            }

            int rowIndex = gridView.Rows.Add();
            gridView.Rows[rowIndex].Cells["Customer"].Value = customer;
            gridView.Rows[rowIndex].Cells["NumOfOrder"].Value = orderNumber;
            gridView.Rows[rowIndex].Cells["Date"].Value = date;
            gridView.Rows[rowIndex].Tag = new List<string[]>();

            return rowIndex;
        }

        /// <summary>
        /// Metoda pro aktualizaci detailGridu
        /// </summary>
        /// <param name="detailGrid"></param>
        /// <param name="detailsList"></param>
        public void UpdateDetailGrid(DataGridView detailGrid, List<string[]> detailsList)
        {
            detailGrid.Rows.Clear();
            foreach (var detail in detailsList)
            {
                detailGrid.Rows.Add(detail.Skip(2).Take(detailGrid.Columns.Count).ToArray());
            }
        }
    }
}
