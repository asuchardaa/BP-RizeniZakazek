using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP_rizeni_zakazek.Services;
using NUnit.Framework;

namespace BP_rizeni_zakazek.Tests
{
    [TestFixture]
    internal class FileManagerTests
    {
        private FileManager _fileManager;
        private string testFilePath;


        [SetUp]
        public void Setup()
        {
            _fileManager = new FileManager();
            testFilePath = Path.GetTempFileName();
            //File.WriteAllLines(testFilePath, new[] { "Header", "Zakazka;12345;Ostatni" });

        }

        [Test]
        public void UspesnePridatNactenySoubor()
        {
            var testFilePath = "test.csv";
            _fileManager.AddLoadedFile(testFilePath);

            Assert.That(_fileManager.isFileLoaded(testFilePath), Is.True);
        }

        [Test]
        public void VraciFalseProNeexistujiciSoubor()
        {
            var testFilePath = "neexistujici.csv";

            Assert.That(_fileManager.isFileLoaded(testFilePath), Is.False);
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
            var vysledek = _fileManager.FindNumberOfOrder_CSV(testFilePath);
            Assert.That(vysledek, Is.EqualTo("12345"));
        }

        [Test]
        public void NajitCisloObjednavky_NespravnyFormat_NULL()
        {
            File.WriteAllLines(testFilePath, new[] { "Header" });
            var vysledek = _fileManager.FindNumberOfOrder_CSV(testFilePath);
            Assert.That(vysledek, Is.Null);
        }

        [Test]
        public void NajitCisloObjednavky_PrazdnySoubor_NULL()
        {
            var vysledek = _fileManager.FindNumberOfOrder_CSV(testFilePath);
            Assert.That(vysledek, Is.Null);
        }

        [Test]
        public void GetLoadedFiles_ReturnsAllLoadedFiles()
        {
            var testFilePath1 = "test1.csv";
            var testFilePath2 = "test2.csv";
            _fileManager.AddLoadedFile(testFilePath1);
            _fileManager.AddLoadedFile(testFilePath2);

            var loadedFiles = _fileManager.GetLoadedFiles();

            Assert.That(loadedFiles, Contains.Item(testFilePath1), "The list should contain the first test file path.");
            Assert.That(loadedFiles, Contains.Item(testFilePath2), "The list should contain the second test file path.");
            Assert.That(loadedFiles.Count, Is.EqualTo(2), "The list should contain exactly two files.");
        }

        [Test]
        public void RemoveLoadedFile_RemovesFileFromLoadedFiles()
        {
            var testFilePath = "test.csv";
            _fileManager.AddLoadedFile(testFilePath);

            _fileManager.RemoveLoadedFile(testFilePath);
            var loadedFiles = _fileManager.GetLoadedFiles();

            Assert.That(loadedFiles, Does.Not.Contain(testFilePath), "The list should not contain the removed file path.");
        }
    }
}
