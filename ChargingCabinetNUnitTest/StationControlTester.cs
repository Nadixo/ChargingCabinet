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
            charger = Substitute.For<UsbChargerSimulator>();
            display = Substitute.For<IDisplay>();
            door = Substitute.For<IDoor>();
            rfidReader = Substitute.For<IRFIDReader>();

            stationControl = new StationControl(door, rfidReader, charger, display);
        }

        [TestCase(1)]
        public void LockOpenDoorOnRFIDScan(int id)
        {
            door.CurrentDoorEvent += Raise.EventWith(new DoorEventArgs { doorState = doorState.Opened });
            rfidReader.RFIDReaderChangedEvent += Raise.EventWith(new RFIDReaderEventArgs { RFIDReaderValue = id });

            door.DidNotReceive().lockDoor();
        }

        [TestCase(1)]
        public void LockClosedDoorOnRFIDScan(int id)
        {
            door.CurrentDoorEvent += Raise.EventWith(new DoorEventArgs { doorState = doorState.Closed });
            rfidReader.RFIDReaderChangedEvent += Raise.EventWith(new RFIDReaderEventArgs { RFIDReaderValue = id });

            door.Received().lockDoor();
        }
    }
}
