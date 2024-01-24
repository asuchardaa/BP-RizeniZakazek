using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP_rizeni_zakazek.utils;
using NUnit.Framework;

namespace BP_rizeni_zakazek.Tests
{
    [TestFixture]
    internal class CSVManagerTests
    {
        private CSVManager _csvManager;
        private string testFilePath;


        [SetUp]
        public void Setup()
        {
            _csvManager = new CSVManager();
            testFilePath = Path.GetTempFileName();
            //File.WriteAllLines(testFilePath, new[] { "Header", "Zakazka;12345;Ostatni" });

        }

        [Test]
        public void UspesnePridatNactenySoubor()
        {
            var testFilePath = "test.csv";
            _csvManager.AddLoadedFile(testFilePath);

            Assert.That(_csvManager.isFileLoaded(testFilePath), Is.True);
        }

        [Test]
        public void VraciFalseProNeexistujiciSoubor()
        {
            var testFilePath = "neexistujici.csv";

            Assert.That(_csvManager.isFileLoaded(testFilePath), Is.False);
        }

        // Mazu soubor hned po testovani
        [TearDown]
        public void Teardown()
        {
            if (File.Exists(testFilePath))
            {
                File.Delete(testFilePath);
            }
        }

        [Test]
        public void NajitCisloObjednavkyCSV_VraciSpravneCisloObjednavky()
        {
            File.WriteAllLines(testFilePath, new[] { "Header", "Zakazka;12345;DalsiData" });
            var vysledek = _csvManager.FindNumberOfOrder_CSV(testFilePath);
            Assert.That(vysledek, Is.EqualTo("12345"));
        }

        [Test]
        public void NajitCisloObjednavky_NespravnyFormat_NULL()
        {
            File.WriteAllLines(testFilePath, new[] { "Header" });
            var vysledek = _csvManager.FindNumberOfOrder_CSV(testFilePath);
            Assert.That(vysledek, Is.Null);
        }

        [Test]
        public void NajitCisloObjednavky_PrazdnySoubor_NULL()
        {
            var vysledek = _csvManager.FindNumberOfOrder_CSV(testFilePath); // testFilePath je prázdný soubor
            Assert.That(vysledek, Is.Null);
        }
    }
}
