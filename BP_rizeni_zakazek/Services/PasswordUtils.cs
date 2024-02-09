using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BP_rizeni_zakazek.Interfaces;

namespace BP_rizeni_zakazek.Services
{
    /// <summary>
    /// Třída pro metody spojené s heslem
    /// </summary>
    internal class PasswordUtils
    {
        private const string hashedPassword = "94e3de58eaa0bfb3222034aa4c8f4d992abd110da905466884ac983c4dcd423f";
        private readonly IMessageDisplayer _messageDisplayer;

        /// <summary>
        /// Konstruktor pro třídu PasswordUtils
        /// </summary>
        /// <param name="messageDisplayer"></param>
        public PasswordUtils(IMessageDisplayer messageDisplayer)
        {
            _messageDisplayer = messageDisplayer;
        }

        /// <summary>
        /// Prázdný konstruktor pro třídu PasswordUtils
        /// </summary>
        public PasswordUtils()
        {
        }

        /// <summary>
        /// Pomocná metoda pro přesunutí popř. vytvoření souboru do nového adresáře
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="newPath"></param>
        /// <param name="isLogFile"></param>
        public void MoveOrCreateFile(string oldPath, string newPath, bool isLogFile = false)
        {
            try
            {
                if (isLogFile)
                {
                    if (File.Exists(oldPath))
                    {
                        File.Delete(oldPath);
                    }
                }
                else
                {
                    if (File.Exists(oldPath))
                    {
                        File.Move(oldPath, newPath, true);
                    }
                    else if (!File.Exists(newPath))
                    {
                        File.Create(newPath).Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _messageDisplayer.ShowMessage($"Chyba při manipulaci se souborem: {ex.Message}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        /// <summary>
        /// Metoda pro ověření hesla
        /// </summary>
        /// <param name="inputPassword"></param>
        /// <returns></returns>
        public bool VerifyPassword(string inputPassword)
        {
            return ComputeSha256Hash(inputPassword) == hashedPassword;
        }

        /// <summary>
        /// Metoda pro výpočet SHA256 hashe
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
