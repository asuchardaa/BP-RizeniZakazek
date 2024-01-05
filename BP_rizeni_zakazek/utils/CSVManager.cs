using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP_rizeni_zakazek.utils
{
    internal class CSVManager
    {

        private HashSet<string> nacteneSoubory = new HashSet<string>();

        /// <summary>
        /// Metoda pro najití čísla objednávky ve vstupním CSV souboru
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string NajitCisloObjednavkyCSV(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            if (lines.Length > 1)
            {
                string[] fields = lines[1].Split(';');
                return fields[1].Trim();
            }
            return null;
        }

        /// <summary>
        /// Metoda pro nalezení čísla objednávky v CSV pro kontrolu duplicity při nahrávání
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool JeSouborJizNacten(string filePath)
        {
            return nacteneSoubory.Contains(filePath);
        }

        /// <summary>
        /// Metoda pro přidání nahrávaného souboru do seznamu nahrátých souborů
        /// </summary>
        /// <param name="filePath"></param>
        public void PridatNactenySoubor(string filePath)
        {
            nacteneSoubory.Add(filePath);
        }

    }
}
