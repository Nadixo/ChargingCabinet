using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinetNUnitTest
{
    [TestFixture]
    public class DisplayTester
    {
        Display display;
        [SetUp]
        public void SetUp()
        {
            display = new Display();
        }

        [TestCase("A normal string")]
        [TestCase("A gr8 string with numbers i guess")]
        [TestCase("H¤w @b¤ut $¤m€ $p€ci@l ch@r@ct€rs")]
        [TestCase("")]
        public void TestIfOutputIsTheSameAsString(string s)
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            display.ShowDisplay(s);
            Assert.That(s + "\r\n", Is.EqualTo(stringWriter.ToString()));
        }
    }
}
