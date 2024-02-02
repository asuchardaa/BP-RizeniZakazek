using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP_rizeni_zakazek.Interfaces
{
    /// <summary>
    /// Interface pro messageBox
    /// </summary>
    public interface IMessageDisplayer
    {
        void ShowMessage(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon);
    }
}
