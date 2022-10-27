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
    }
}
