using ChargingCabinet.Interfaces;
using ChargingCabinet.Models;
using ChargingCabinet.Simulators;

class Program
{
	static void Main(string[] args)
	{
		Door door = new Door();
		RFIDReader rfidReader = new RFIDReader();
		Display display = new Display();
		UsbChargerSimulator charger = new UsbChargerSimulator();
		ChargeControl chargeControl = new ChargeControl(charger, display);
		StationControl stationControl = new StationControl(door, rfidReader, chargeControl, display);

		bool finish = false;
		do
		{
			display.ShowDisplay("Indtast E, O, C, R: ");
			string ?input = Console.ReadLine();
			if (string.IsNullOrEmpty(input)) continue;

			switch (input[0])
			{
				case 'E':
					finish = true;
					break;

				case 'O':
					door.onDoorOpen();
					break;

				case 'C':
					door.onDoorClose();
					break;

				case 'R':
					display.ShowDisplay("Indtast RFID id: ");
					string ?idString = Console.ReadLine();

					int id = Convert.ToInt32(idString);
					rfidReader.setRFIDState(id);
					break;

				default:
					break;
			}

		} while (!finish);
	}
}
