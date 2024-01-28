using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP_rizeni_zakazek.utils
{
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


    }
}
