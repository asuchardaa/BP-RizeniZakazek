using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP_rizeni_zakazek.utils
{
    internal class DataGridViewHelper
    {
        private Dictionary<int, DataGridView> detailGrids = new Dictionary<int, DataGridView>();

        private OrderManager _orderManager = new OrderManager();
        /// <summary>
        /// Metoda pro odstranění specifického řádku z DataGridView
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="cisloObjednavky"></param>
        public void OdstranitSpecifickyRadek(DataGridView gridView, string cisloObjednavky)
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
        /// Metoda pro ověření existence řádku v masterGridu - zabraňuje duplicitám
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="orderNumber"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool RowExists(DataGridView gridView, string customer, string orderNumber, string date)
        {
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (row.IsNewRow) continue;

                var customerCell = row.Cells["Customer"].Value?.ToString() ?? "";
                var orderNumberCell = row.Cells["NumOfOrder"].Value?.ToString() ?? "";
                var dateCell = row.Cells["Date"].Value?.ToString() ?? "";

                if (customerCell == customer && orderNumberCell == orderNumber && dateCell == date)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Metoda pro přidání detailních informací do existujícího řádku
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="orderNumber"></param>
        /// <param name="date"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        public int AddDetailsToExistingRow(DataGridView gridView, string customer, string orderNumber, string date, string[] details)
        {
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (row.Cells["Customer"].Value.ToString() == customer &&
                    row.Cells["NumOfOrder"].Value.ToString() == orderNumber &&
                    row.Cells["Date"].Value.ToString() == date)
                {
                    var detailsList = (List<string[]>)row.Tag;
                    detailsList.Add(details);
                    return row.Index;
                }
            }

            return -1;
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


    }
}
