using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP_rizeni_zakazek.utils
{
    internal class OrderManager
    {
        /// <summary>
        /// Metoda pro určení celkového stavu zakázky
        /// </summary>
        /// <param name="masterRow"></param>
        /// <returns></returns>
        public string CalculateOverallStatus(DataGridViewRow masterRow, DataGridView gridView)
        {
            if (masterRow.Tag is List<string[]> detailsList)
            {
                bool anyInProgressOrComplete = false;
                bool allDone = true;

                foreach (var detail in detailsList)
                {
                    string status = detail[9];

                    if (status.Equals("Hotovo", StringComparison.OrdinalIgnoreCase))
                    {
                        anyInProgressOrComplete = true; // alespoň jeden stav "Hotovo"
                    }
                    else
                    {
                        allDone = false;
                        if (status.Equals("Rozpracováno", StringComparison.OrdinalIgnoreCase) ||
                            status.Equals("Hotovo", StringComparison.OrdinalIgnoreCase))
                        {
                            anyInProgressOrComplete = true; // alespoň jeden "Rozpracovano" nebo "Hotovo"
                        }
                    }
                }

                string resultStatus;

                if (allDone)
                {
                    resultStatus = "Hotovo";
                }
                else if (anyInProgressOrComplete)
                {
                    resultStatus = "Rozpracováno";
                }
                else
                {
                    resultStatus = "Nezadáno";
                }

                DataGridViewCell statusCell = masterRow.Cells["StateOfOrder"];
                SetOrderCellColor(statusCell, resultStatus);
                UpdateDateOfFinish(masterRow, resultStatus);
                int dateColIndex = gridView.Columns["Date"].Index;
                HighlightOverdueDates(masterRow, dateColIndex);
                return resultStatus;
            }

            return "Neznámý";
        }

        /// <summary>
        /// Metoda pro nastavení barvy buňky dle stavu objednávky
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="status"></param>
        public void SetOrderCellColor(DataGridViewCell cell, string status)
        {
            Color color;
            switch (status)
            {
                case "Hotovo":
                    color = Color.Green;
                    break;
                case "Rozpracováno":
                    color = Color.Yellow;
                    break;
                case "Nezadáno":
                    color = Color.LightBlue;
                    break;
                case "Více kusů":
                    color = Color.Coral;
                    break;
                default:
                    color = Color.Red;
                    break;
            }

            cell.Style.BackColor = color;
        }

        /// <summary>
        /// Metoda pro opětovnou aktualizaci barvy buňky dle stavu objednávky
        /// </summary>
        /// <param name="masterRow"></param>
        /// <param name="status"></param>
        public void UpdateColorStatusOrders(DataGridViewRow masterRow, string status)
        {
            if (status == "Hotovo")
            {
                masterRow.Cells[5].Style.BackColor = System.Drawing.Color.Green;
            }
            else if (status == "Rozpracováno")
            {
                masterRow.Cells[5].Style.BackColor = System.Drawing.Color.Yellow;
            }
            else if (status == "Nezadáno")
            {
                masterRow.Cells[5].Style.BackColor = System.Drawing.Color.LightBlue;
            }
            else
            {
                masterRow.Cells[5].Style.BackColor = System.Drawing.Color.LightGray;
            }
        }

        /// <summary>
        /// Metoda pro aktualizaci datumu dle dokončení zakázky
        /// </summary>
        /// <param name="row"></param>
        /// <param name="status"></param>
        public void UpdateDateOfFinish(DataGridViewRow row, string status)
        {
            if (status == "Hotovo")
            {
                string formattedDate = DateTime.Now.ToString("dd/MM/yyyy");
                row.Cells["dateOfFinish"].Value = formattedDate;
            }
            else
            {
                row.Cells["dateOfFinish"].Value = "";
            }
        }

        /// <summary>
        /// Metoda pro barevných zvýraznění překročených termínů dle datumu v masterGridu
        /// </summary>
        /// <param name="row"></param>
        /// <param name="dateColumnIndex"></param>
        public void HighlightOverdueDates(DataGridViewRow row, int dateColumnIndex)
        {
            if (DateTime.TryParse(row.Cells[dateColumnIndex].Value?.ToString(), out DateTime cellDate))
            {
                if (cellDate < DateTime.Now.Date)
                {
                    row.Cells[dateColumnIndex].Style.BackColor = Color.Red;
                }
            }
        }

        /// <summary>
        /// Metoda pro určení stavu objednávky
        /// </summary>
        /// <param name="created"></param>
        /// <param name="originalAmount"></param>
        /// <param name="curve"></param>
        /// <returns></returns>
        public string DetermineOrderStatus(string created, string originalAmount, string curve)
        {
            bool parseCreated = int.TryParse(created, out int numCreated);
            bool parseOriginalAmount = int.TryParse(originalAmount, out int numOriginalAmount);


            if (!parseCreated || !parseOriginalAmount)
            {
                return "chyba přev";
            }

            if (string.IsNullOrEmpty(curve))
            {
                return "Špatně";
            }

            if (numCreated == 0)
            {
                return "Nehotovo";
            }
            else if (numCreated != numOriginalAmount || curve.Equals("ANO", StringComparison.OrdinalIgnoreCase))
            {
                return "Rozpracováno";
            }
            else if (numCreated == numOriginalAmount && curve.Equals("NE", StringComparison.OrdinalIgnoreCase))
            {
                return "Hotovo";
            }
            else if (numCreated > numOriginalAmount)
            {
                return "Více kusů";
            }

            return "Špatně/Chyba";
        }

        /// <summary>
        /// Metoda pro získání barvy podle stavu
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public Color GetColorForStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                return Color.White;
            }

            switch (status.ToLower())
            {
                case "neznámý":
                    return Color.LightSlateGray;
                case "rozpracováno":
                    return Color.Yellow;
                case "hotovo":
                    return Color.Green;
                case "více kusů":
                    return Color.Coral;
                default:
                    return Color.White;
            }
        }

        /// <summary>
        /// Metoda pro určení stavu zakázky po opětovném nahrání json souboru nebo editaci
        /// </summary>
        /// <param name="detailData"></param>
        /// <returns></returns>
        public string DetermineOrderStatusList(List<string[]> detailData)
        {
            bool anyInProgressOrComplete = false;
            bool allDone = true;

            foreach (var row in detailData)
            {
                string status = row[9];

                if (status.Equals("Hotovo", StringComparison.OrdinalIgnoreCase))
                {
                    anyInProgressOrComplete = true; // alespoň jeden status "Hotovo"
                }
                else
                {
                    allDone = false;
                    if (status.Equals("Rozpracováno", StringComparison.OrdinalIgnoreCase) ||
                        status.Equals("Hotovo", StringComparison.OrdinalIgnoreCase))
                    {
                        anyInProgressOrComplete = true; // alespoň jeden "Rozpracovano" nebo "Hotovo"
                    }
                }
            }

            if (allDone)
            {
                return "Hotovo";
            }
            else if (anyInProgressOrComplete)
            {
                return "Rozpracováno";
            }
            else
            {
                return "Nezadáno";
            }
        }

        /// <summary>
        /// Metoda pro určení nového stavu objednávky po editu
        /// </summary>
        /// <param name="created"></param>
        /// <param name="amount"></param>
        /// <param name="curve"></param>
        /// <returns></returns>
        public string DetermineTheNewStatus(string created, string amount, string curve)
        {
            if (string.IsNullOrEmpty(created) || string.IsNullOrEmpty(amount))
            {
                return "Neznámý";
            }

            if (int.TryParse(created, out int createdInt) && int.TryParse(amount, out int amountInt))
            {
                if (createdInt == amountInt)
                {
                    if (curve.Equals("NE", StringComparison.OrdinalIgnoreCase))
                    {
                        return "Hotovo";
                    }
                    if (curve.Equals("ANO", StringComparison.OrdinalIgnoreCase))
                    {
                        return "Rozpracováno";
                    }
                }
                else if (createdInt < amountInt)
                {
                    return "Rozpracováno";
                }
                else if (createdInt > amountInt)
                {
                    return "Více kusů";
                }
            }

            return "Neplatný status";
        }
    }
}
