using ChargingCabinet.Interfaces;
using ChargingCabinet.Models;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NUnit.Framework.Constraints.Tolerance;

namespace ChargingCabinetNUnitTest
{
    [TestFixture]
    internal class StationControlTester
    {
        private StationControl stationControl;
        private IDoor door;
        private IRFIDReader rfidReader;
        private IUsbCharger charger;
        private IDisplay display;

        [SetUp]
        public void Setup()
        {
            charger = Substitute.For<IUsbCharger>();
            display = Substitute.For<IDisplay>();
            door = Substitute.For<IDoor>();
            rfidReader = Substitute.For<IRFIDReader>();

            stationControl = new StationControl(door, rfidReader, charger, display);
        }

        [TestCase(1)]
        public void OpenDoorDoesNotLockOnRFIDScan(int id)
        {
            rfidReader.RFIDReaderChangedEvent += Raise.EventWith(new RFIDReaderEventArgs { RFIDReaderValue = id });

            door.DidNotReceive().lockDoor();
        }
    }
}
