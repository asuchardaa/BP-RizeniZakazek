using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP_rizeni_zakazek.Interfaces
{
    /// <summary>
    /// Interface pro práci se soubory
    /// </summary>
    public interface IDataManager
    {
        bool isFileLoaded(string filePath);
        void AddLoadedFile(string filePath);
        void RemoveLoadedFile(string filePath);
        List<string> GetLoadedFiles();
        string FindNumberOfOrder_CSV(string filePath);
    }
}
