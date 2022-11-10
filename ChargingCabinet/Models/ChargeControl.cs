using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingCabinet.Interfaces;
using ChargingCabinet.Simulators;

namespace ChargingCabinet.Models
{
	public class ChargeControl : IChargeControl
	{
		UsbChargerSimulator chargerSimulator;
		Display? display;

		public IChargeControl.UsbChargerState State { get; set; }

		public ChargeControl(UsbChargerSimulator charger, Display display)
		{
			chargerSimulator = charger;
			this.display = display;
			charger.CurrentValueEvent += HandleCurrentEvent;
		}

		
		public void StartCharge()
		{
			chargerSimulator!.StartCharge();
		}

		public void StopCharge()
		{
			chargerSimulator!.StopCharge();
		}

		public void HandleCurrentEvent(object? sender, CurrentEventArgs e)
		{
			double c = e.Current;
			switch (c)
			{
				case double n when (n < 0):
                    throw new ArgumentException("I don't know how this happened but " +
												"your phone is somehow losing power");
				case 0:
                    State = IChargeControl.UsbChargerState.notCharging;
                    break;
				case double n when (n > 0 && n <= 5):
					State = IChargeControl.UsbChargerState.fullyCharged;
					display?.ShowDisplay("Phone is fully charged");
					break;
                case double n when (n > 5 && n <= 500):
					State = IChargeControl.UsbChargerState.charging;
					display?.ShowDisplay("Phone is charging");
                    break;
                case double n when (n > 500):
					StopCharge();
					State = IChargeControl.UsbChargerState.stopCharging;
					display?.ShowDisplay("Something went wrong, charging stopped");
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
    }
}
