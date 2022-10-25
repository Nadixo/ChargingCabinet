using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinet.Interfaces
{
    public enum doorState
    {
        Opened,
        Closed,
        Locked
    }
    public class DoorEventArgs : EventArgs
    {
        public doorState doorState { get; set; }
    }

    public interface IDoor
    {
        event EventHandler<DoorEventArgs> CurrentDoorEvent;

        void onDoorOpen();
        void onDoorClose();

    }
}
