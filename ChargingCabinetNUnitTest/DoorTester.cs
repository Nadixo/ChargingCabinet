using ChargingCabinet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinetNUnitTest
{
    [TestFixture]
    public class DoorTester
    {
        Door door;
        [SetUp]
        public void SetUp()
        {
            door = new Door();
        }

        [TestCase(doorState.Closed)]
        [TestCase(doorState.Opened)]
        public void SetDoorStateOpenedClosed(doorState testState)
        {
            doorState state = testState;
            door.CurrentDoorEvent += (noArg, arg) => state = arg.doorState;
            door.NewDoorState(state);

            Assert.That(state, Is.EqualTo(testState));
        }

        [Test]
        public void SetDoorStateLocked()
        {
            door.NewDoorState(doorState.Closed);
            doorState state = doorState.Locked;
            door.CurrentDoorEvent += (noArg, arg) => state = arg.doorState;
            door.NewDoorState(state);

            Assert.That(state, Is.EqualTo(doorState.Locked));
        }

        [Test]
        public void LockClosedDoor()
        {
            doorState state = doorState.Closed;
            door.NewDoorState(state);
            door.CurrentDoorEvent += (noArg, arg) => state = arg.doorState;
            door.lockDoor();

            Assert.That(state, Is.EqualTo(doorState.Locked));
        }

        [Test]
        public void LockOpenedDoor()
        {
            doorState state = doorState.Opened;
            door.NewDoorState(state);
            door.CurrentDoorEvent += (noArg, arg) => state = arg.doorState;

            Assert.Throws<ArgumentException>(() => door.lockDoor());
        }

        [TestCase(doorState.Opened)]
        [TestCase(doorState.Closed)]
        public void UnlockOpenedOrClosedDoor(doorState testState)
        {
            doorState state = testState;
            door.NewDoorState(state);
            door.CurrentDoorEvent += (noArg, arg) => state = arg.doorState;

            Assert.Throws<ArgumentException>(() => door.unlockDoor());
        }

        [Test]
        public void UnlockLockedDoor()
        {
            doorState state = doorState.Locked;
            door.NewDoorState(state);
            door.CurrentDoorEvent += (noArg, arg) => state = arg.doorState;
            door.unlockDoor();

            Assert.That(state, Is.EqualTo(doorState.Closed));
        }
    }
}
