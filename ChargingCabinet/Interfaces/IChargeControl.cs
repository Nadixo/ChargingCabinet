using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinet.Interfaces
{
    public class CurrentEventArgs : EventArgs
    {
        // Value in mA (milliAmpere)
        public double Current { set; get; }
    }
    public interface IChargeControl
    {
        public enum UsbChargerState
        {
            notCharging,
            charging,
            fullyCharged,
            stopCharging
        }
        void StartCharge() { }
        void StopCharge() { }
        public bool Connected { get; set; }
    }
}
