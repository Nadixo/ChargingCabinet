using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingCabinet.Interfaces;
using ChargingCabinet.Simulators;

namespace ChargingCabinet.Models
{
    internal class ChargeControl : IUsbCharger
    {
        UsbChargerSimulator chargerSimulator;

        ChargeControl(UsbChargerSimulator charger)
        {
            chargerSimulator = charger;
        }

        double IUsbCharger.CurrentValue => throw new NotImplementedException();

        bool IUsbCharger.Connected => throw new NotImplementedException();

        public event EventHandler<CurrentEventArgs> CurrentValueEvent
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        public void StartCharge()
        {
            chargerSimulator.StartCharge();
        }

        public void StopCharge()
        {
            chargerSimulator?.StopCharge();
        }
    }
}
