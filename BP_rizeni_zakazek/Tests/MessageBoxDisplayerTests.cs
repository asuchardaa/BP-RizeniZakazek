using BP_rizeni_zakazek.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP_rizeni_zakazek.Tests
{
    [TestFixture]
    internal class MessageBoxDisplayerTests
    {
        private MessageBoxDisplayer _messageBoxDisplayer;

        [SetUp]
        public void SetUp()
        {
            _messageBoxDisplayer = new MessageBoxDisplayer();
        }

        [Test]
        public void ShowMessage_InvokesMessageBoxShow_WithCorrectParameters()
        {
            string expectedMessage = "Ahoj to je moje chyba";
            string expectedTitle = "titulek";
            MessageBoxButtons expectedButtons = MessageBoxButtons.YesNo;
            MessageBoxIcon expectedIcon = MessageBoxIcon.Question;

            bool messageBoxShowInvoked = false;

            _messageBoxDisplayer.ShowMessage(expectedMessage, expectedTitle, expectedButtons, expectedIcon);

            Assert.That(expectedMessage, Is.EqualTo("Ahoj to je moje chyba"));
            Assert.That(expectedTitle, Is.EqualTo("titulek"));
            Assert.That(expectedButtons, Is.EqualTo(MessageBoxButtons.YesNo));
            Assert.That(expectedIcon, Is.EqualTo(MessageBoxIcon.Question));
        }
    }
}