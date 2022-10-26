using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingCabinet.Interfaces;

namespace ChargingCabinet.Models
{
    public class StationControl
    {
        private enum ChargingCabinetState
        {
            Available,
            Locked,
            DoorOpen
        };

        private ChargingCabinetState _state;
        private IUsbCharger _charger;
        private int _oldId;
        private IDoor _door;
        private IDisplay _display;

        private string logFile = "logfile.txt";

        public StationControl(IDoor door, IRFIDReader rfidReader, IUsbCharger chargeControl, IDisplay display)
        {
            door.CurrentDoorEvent += HandleDoorEvent;
            rfidReader.RFIDReaderChangedEvent += HandleRFIDEvent;
            _display = display;
            _charger = chargeControl;
            _door = door;
        }

        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case ChargingCabinetState.Available:
                    if (_charger.Connected)
                    {
                        _door.lockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        _display.ShowDisplay("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = ChargingCabinetState.Locked;
                    }
                    else
                    {
                        _display.ShowDisplay("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case ChargingCabinetState.DoorOpen:
                    // Ignore
                    break;

                case ChargingCabinetState.Locked:
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.unlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        _display.ShowDisplay("Tag din telefon ud af skabet og luk døren");
                        _state = ChargingCabinetState.Available;
                    }
                    else
                    {
                        _display.ShowDisplay("Forkert RFID tag");
                    }

                    break;
            }
        }

        private void HandleDoorEvent(object? sender, DoorEventArgs e)
        {
            switch (e.doorState)
            {
                case doorState.Closed:
                    _display.ShowDisplay("Load RFID");
                    _state = ChargingCabinetState.Available;
                    break;
                case doorState.Opened:
                    _display.ShowDisplay("Connect phone");
                    _state = ChargingCabinetState.DoorOpen;
                    break;
                case doorState.Locked:
                    // Nothing is to happen here
                    break;
            }
        }

        private void HandleRFIDEvent(object? sender, RFIDReaderEventArgs e)
        {
            RfidDetected(e.RFIDReaderValue);
        }
    }
}
