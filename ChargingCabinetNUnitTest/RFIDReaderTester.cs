using ChargingCabinet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinetNUnitTest
{
	[TestFixture]
	public class RFIDReaderTester
	{
		RFIDReader rfid;
		RFIDReaderEventArgs? eventArgs;

		[SetUp]
		public void SetUp()
		{
			rfid = new RFIDReader();
			eventArgs = null;
		}

		[TestCase(1)]
		[TestCase(-1)]
		public void SetRFIDWithouEventListener(int r)
		{
			rfid.setRFIDState(r);

			Assert.That(eventArgs, Is.Null);
		}

		[TestCase(1)]
		[TestCase(-1)]
		public void SetRFIDWithEventListener(int r)
		{
			rfid.RFIDReaderChangedEvent += (noArg, arg) => eventArgs = arg;
			rfid.setRFIDState(r);

			Assert.That(eventArgs, Is.Not.Null);
		}

		[TestCase(1, 2)]
		[TestCase(-1, -2)]
		public void SetRFIDTwice(int oldR, int newR)
		{
            rfid.RFIDReaderChangedEvent += (noArg, arg) => eventArgs = arg;
			rfid.setRFIDState(oldR);
			rfid.setRFIDState(newR);

			Assert.That(eventArgs?.RFIDReaderValue, Is.EqualTo(newR));
        }
    }
}
