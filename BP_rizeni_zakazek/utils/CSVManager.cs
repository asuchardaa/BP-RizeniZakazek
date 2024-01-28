using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP_rizeni_zakazek.utils
{
    internal class CsvManager
    {
        private List<string> loadedFiles = new List<string>();

        public List<string> GetLoadedFiles()
        {
            return loadedFiles;
        }

        /// <summary>
        /// Metoda pro najití čísla objednávky ve vstupním CSV souboru
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string FindNumberOfOrder_CSV(string filePath)
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
        public bool isFileLoaded(string filePath)
        {
            return loadedFiles.Contains(filePath);
        }

        /// <summary>
        /// Metoda pro přidání nahrávaného souboru do seznamu nahrátých souborů
        /// </summary>
        /// <param name="filePath"></param>
        public void AddLoadedFile(string filePath)
        {
            if (!loadedFiles.Contains(filePath))
            {
                loadedFiles.Add(filePath);
            }
        }

        /// <summary>
        /// Metoda pro odebrání nahrávaného souboru ze seznamu nahrátých souborů
        /// </summary>
        /// <param name="filePath"></param>
        public void RemoveLoadedFile(string filePath)
        {
            loadedFiles.Remove(filePath);
        }
    }
}