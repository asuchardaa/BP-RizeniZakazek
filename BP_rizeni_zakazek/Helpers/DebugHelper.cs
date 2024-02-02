using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP_rizeni_zakazek.Helpers
{
    /// <summary>
    /// Třída pro metodu pomáhající s debugem
    /// </summary>
    internal class DebugHelper
    {
        /// <summary>
        /// Pomocná metoda pro zjištění názvu metody,jež je v kódu volána
        /// </summary>
        /// <returns></returns>
        public string GetCallingMethodName()
        {
            var stackTrace = new StackTrace();
            var callingMethod = stackTrace.GetFrame(1).GetMethod();
            return callingMethod.Name;
        }
    }
}
