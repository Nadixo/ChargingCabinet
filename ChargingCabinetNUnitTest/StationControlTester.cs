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
        private IChargeControl charger;
        private IDisplay display;

        [SetUp]
        public void Setup()
        {
            charger = Substitute.For<IChargeControl>();
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
            charger.Connected = true;
            door.CurrentDoorEvent += Raise.EventWith(new DoorEventArgs { doorState = doorState.Closed });
            rfidReader.RFIDReaderChangedEvent += Raise.EventWith(new RFIDReaderEventArgs { RFIDReaderValue = id });

            door.Received().lockDoor();
        }

        [Test]
        public void LockDoorNothingHappens()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            door.CurrentDoorEvent += Raise.EventWith(new DoorEventArgs { doorState = doorState.Locked });

            Assert.That(stringWriter.ToString(), Is.EqualTo(""));
        }

        [TestCase(1)]
        public void LockClosedDoorUnconnectedChargerOnRFIDScan(int id)
        {
            charger.Connected = false;
            door.CurrentDoorEvent += Raise.EventWith(new DoorEventArgs { doorState = doorState.Closed });
            rfidReader.RFIDReaderChangedEvent += Raise.EventWith(new RFIDReaderEventArgs { RFIDReaderValue = id });

            door.DidNotReceive().lockDoor();
        }

        [TestCase(1, 1)]
        public void UnlockClosedDoorSetRFIDTwice(int oldId, int newId)
        {
            charger.Connected = true;
            door.CurrentDoorEvent += Raise.EventWith(new DoorEventArgs { doorState = doorState.Closed });
            rfidReader.RFIDReaderChangedEvent += Raise.EventWith(new RFIDReaderEventArgs { RFIDReaderValue = oldId });
            rfidReader.RFIDReaderChangedEvent += Raise.EventWith(new RFIDReaderEventArgs { RFIDReaderValue = newId });

            door.Received().unlockDoor();
        }

        [TestCase(1, 2)]
        public void UnlockClosedDoorSetRFIDToDifferentIds(int oldId, int newId)
        {
            charger.Connected = true;
            door.CurrentDoorEvent += Raise.EventWith(new DoorEventArgs { doorState = doorState.Closed });
            rfidReader.RFIDReaderChangedEvent += Raise.EventWith(new RFIDReaderEventArgs { RFIDReaderValue = oldId });
            rfidReader.RFIDReaderChangedEvent += Raise.EventWith(new RFIDReaderEventArgs { RFIDReaderValue = newId });

            door.DidNotReceive().unlockDoor();
        }
    }
}
