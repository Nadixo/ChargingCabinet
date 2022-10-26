﻿using System;
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
		public IUsbCharger.UsbChargerState State { get; set; }

		public ChargeControl(UsbChargerSimulator charger)
		{
			chargerSimulator = charger;
			charger.CurrentValueEvent += HandleCurrentEvent;
		}

		double IUsbCharger.CurrentValue => throw new NotImplementedException();

		bool IUsbCharger.Connected => throw new NotImplementedException();

		public void StartCharge()
		{
			chargerSimulator?.StartCharge();
		}

		public void StopCharge()
		{
			chargerSimulator?.StopCharge();
		}

		public void HandleCurrentEvent(object sender, CurrentEventArgs e)
		{
			double c = e.Current;
			switch (c)
			{
				case double n when (n < 0):
                    throw new ArgumentException("I don't know how this happened but " +
												"your phone is somehow losing power");
				case 0:
                    State = IUsbCharger.UsbChargerState.notCharging;
                    break;
				case double n when (n > 0 && n <= 5):
					State = IUsbCharger.UsbChargerState.fullyCharged;
					// Display show that charging is finished
					break;
                case double n when (n > 5 && n <= 500):
					State = IUsbCharger.UsbChargerState.charging;
					// Display show that charging is ongoing
                    break;
                case double n when (n > 500):
					StopCharge();
					State = IUsbCharger.UsbChargerState.stopCharging;
					// Display show an error message
                    break;
            }
		}

        private bool connected;
        public bool Connected
        {
            get
            {
                connected = chargerSimulator.Connected;
                return connected;
            }
            set { connected = chargerSimulator.Connected; }

        }


		public event EventHandler<CurrentEventArgs> CurrentValueEvent
		{
			add { throw new NotImplementedException(); }
			remove { throw new NotImplementedException(); }
		}
    }
}
