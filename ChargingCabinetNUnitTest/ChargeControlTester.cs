using ChargingCabinet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinetNUnitTest
{
    [TestFixture]
    public class ChargeControlTester
    {
        private ChargeControl? charger;
        private UsbChargerSimulator? chargerSimulator;
        private Display? display;

        [SetUp]
        public void Setup()
        {
            chargerSimulator = new UsbChargerSimulator();
            display = new Display();
            charger = new ChargeControl(chargerSimulator, display);
        }
    }
}
