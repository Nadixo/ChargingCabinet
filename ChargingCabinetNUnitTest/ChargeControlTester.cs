using ChargingCabinet.Interfaces;
using ChargingCabinet.Models;
using ChargingCabinet.Simulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChargingCabinet.Interfaces.IUsbCharger;
using static NUnit.Framework.Constraints.Tolerance;

namespace ChargingCabinetNUnitTest
{
    [TestFixture]
    public class ChargeControlTester
    {
        private ChargeControl? charger;
        private UsbChargerSimulator chargerSimulator;
        private Display? display;

        [SetUp]
        public void Setup()
        {
            chargerSimulator = new UsbChargerSimulator();
            display = new Display();
            charger = new ChargeControl(chargerSimulator, display);
        }

        [TestCase(int.MaxValue, UsbChargerState.stopCharging)]
        [TestCase(500.1, UsbChargerState.stopCharging)]
        [TestCase(500, UsbChargerState.charging)]
        [TestCase(499.9, UsbChargerState.charging)]
        [TestCase(5.1, UsbChargerState.charging)]
        [TestCase(5, UsbChargerState.fullyCharged)]
        [TestCase(4.9, UsbChargerState.fullyCharged)]
        [TestCase(0.1, UsbChargerState.fullyCharged)]
        [TestCase(0, UsbChargerState.notCharging)]
        public void CurrentEventCheckState(double current, UsbChargerState state)
        {
            charger?.HandleCurrentEvent(chargerSimulator, new CurrentEventArgs { Current = current});

            Assert.That(state, Is.EqualTo(charger?.State));
        }

        [TestCase(int.MaxValue, false)]
        [TestCase(0.1, false)]
        [TestCase(0, false)]
        [TestCase(-0.1, true)]
        [TestCase(-int.MaxValue, true)]
        public void CurrentChangeCheckForException(double current, bool Throw)
        {
            if (Throw)
                Assert.Throws<ArgumentException>(() =>
                    charger?.HandleCurrentEvent(chargerSimulator, 
                    new CurrentEventArgs { Current = current }));
            else
                Assert.DoesNotThrow(() =>
                    charger?.HandleCurrentEvent(chargerSimulator, 
                    new CurrentEventArgs { Current = current }));
        }
    }
}