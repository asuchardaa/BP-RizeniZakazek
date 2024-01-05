using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP_rizeni_zakazek
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
        private void SetOrderCellColor(DataGridViewCell cell, string status)
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
                default:
                    color = Color.Red;
                    break;
            }

            cell.Style.BackColor = color;
        }

        /// <summary>
        /// Metoda pro aktualizaci datumu dle dokončení zakázky
        /// </summary>
        /// <param name="row"></param>
        /// <param name="status"></param>
        private void UpdateDateOfFinish(DataGridViewRow row, string status)
        {
            if (status == "Hotovo")
            {
                string formattedDate = DateTime.Now.ToString("dd/MM/yyyy");
                row.Cells["dateOfFinish"].Value = formattedDate;
            }
        }

        /// <summary>
        /// Metoda pro barevných zvýraznění překročených termínů dle datumu v masterGridu
        /// </summary>
        /// <param name="row"></param>
        /// <param name="dateColumnIndex"></param>
        private void HighlightOverdueDates(DataGridViewRow row, int dateColumnIndex)
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
        /// <param name="vyrobeno"></param>
        /// <param name="originalPocet"></param>
        /// <param name="ohyb"></param>
        /// <returns></returns>
        public string DetermineOrderStatus(string vyrobeno, string originalPocet, String ohyb)
        {
            bool parseVyrobeno = int.TryParse(vyrobeno, out int numVyrobeno);
            bool parseOriginalPocet = int.TryParse(originalPocet, out int numOriginalPocet);


            if (!parseVyrobeno || !parseOriginalPocet)
            {
                return "chyba přev";
            }

            if (numVyrobeno == 0)
            {
                return "Nehotovo";
            }
            else if (numVyrobeno != numOriginalPocet || ohyb.Equals("ANO", StringComparison.OrdinalIgnoreCase))
            {
                return "Rozpracováno";
            }
            else if (numVyrobeno == numOriginalPocet && ohyb.Equals("NE", StringComparison.OrdinalIgnoreCase))
            {
                return "Hotovo";
            }

            return "Nehotovo";
        }

        /// <summary>
        /// Metoda pro získání barvy podle stavu
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public Color GetColorForStatus(string status)
        {
            switch (status.ToLower())
            {
                case "neznámý":
                    return Color.LightSlateGray;
                case "rozpracováno":
                    return Color.Yellow;
                case "hotovo":
                    return Color.Green;
                default:
                    return Color.White;
            }
        }
    }
}
