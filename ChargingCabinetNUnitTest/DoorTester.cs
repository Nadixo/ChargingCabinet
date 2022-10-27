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
    }
}
