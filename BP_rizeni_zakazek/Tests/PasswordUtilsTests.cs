using BP_rizeni_zakazek.Interfaces;
using BP_rizeni_zakazek.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP_rizeni_zakazek.Tests
{
    [TestFixture]
    internal class PasswordUtilsTests
    {
        private PasswordUtils _passwordUtils;
        private string _testDirectory;
        private string _oldFilePath;
        private string _newFilePath;

        [SetUp]
        public void Setup()
        {
            _passwordUtils = new PasswordUtils();
            _testDirectory = Path.Combine(Path.GetTempPath(), "PasswordUtilsTests");
            Directory.CreateDirectory(_testDirectory);

            _oldFilePath = Path.Combine(_testDirectory, "oldFile.txt");
            _newFilePath = Path.Combine(_testDirectory, "newFile.txt");

            File.WriteAllText(_oldFilePath, "Test content");
        }

        [Test]
        public void ComputeSha256Hash_ReturnsCorrectHash()
        {
            string testData = "testPassword";
            string expectedHash = "fd5cb51bafd60f6fdbedde6e62c473da6f247db271633e15919bab78a02ee9eb"; 

            string actualHash = _passwordUtils.ComputeSha256Hash(testData);

            Assert.That(actualHash, Is.EqualTo(expectedHash), "SHA256 by mel sedet.");
        }

        [Test]
        [TestCase("wrongPassword", false)]
        public void VerifyPassword_ReturnsCorrectVerification(string password, bool expectedResult)
        {
            bool result = _passwordUtils.VerifyPassword(password);

            Assert.That(result, Is.EqualTo(expectedResult), "heslo by melo sedet s predepsanym heslem");
        }

        [Test]
        public void MoveOrCreateFile_MovesFile_WhenOldFileExists()
        {
            _passwordUtils.MoveOrCreateFile(_oldFilePath, _newFilePath);

            Assert.That(File.Exists(_oldFilePath), Is.False, "Starej soubor neexistuje");
            Assert.That(File.Exists(_newFilePath), Is.True, "Novej soubor existuje a to hnedka po presunuti staryho filu");
        }

        [Test]
        public void MoveOrCreateFile_CreatesFile_WhenOldFileDoesNotExist()
        {
            File.Delete(_oldFilePath);
            _passwordUtils.MoveOrCreateFile(_oldFilePath, _newFilePath);

            Assert.That(File.Exists(_newFilePath), "Novej soubor vytvorenej");
        }

        [Test]
        public void MoveOrCreateFile_DeletesLogFile_WhenIsLogFile()
        {
            _passwordUtils.MoveOrCreateFile(_oldFilePath, _newFilePath, true);

            Assert.That(File.Exists(_oldFilePath), Is.False, "Log soubor smazanej");
        }

        [Test]
        public void MoveOrCreateFile_WhenException_ShowsMessageBox()
        {
            var messageDisplayerMock = new Mock<IMessageDisplayer>();
            var passwordUtils = new PasswordUtils(messageDisplayerMock.Object);

            string invalidOldPath = "neexistující/cesta/oldFile.txt";
            string invalidNewPath = "neexistující/cesta/newFile.txt";

            passwordUtils.MoveOrCreateFile(invalidOldPath, invalidNewPath);

            messageDisplayerMock.Verify(m => m.ShowMessage(It.IsAny<string>(), "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error), Times.Once());
        }

        // Skvela metoda, ktera maze soubor hned po testovani -> aspon nebude zadny bordel
        [TearDown]
        public void Teardown()
        {
            if (File.Exists(_oldFilePath))
            {
                File.Delete(_oldFilePath);
            }

            if (File.Exists(_newFilePath))
            {
                File.Delete(_newFilePath);
            }

            if (Directory.Exists(_testDirectory))
            {
                Directory.Delete(_testDirectory, true);
            }
        }


    }
}
