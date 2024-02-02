using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP_rizeni_zakazek.Interfaces;

namespace BP_rizeni_zakazek.Services
{
    /// <summary>
    /// Třída pro zobrazení messageBox
    /// </summary>
    internal class MessageBoxDisplayer : IMessageDisplayer
    {
        public void ShowMessage(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, buttons, icon);
        }
    }
}
