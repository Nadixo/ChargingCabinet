using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinet.Interfaces
{
    public class RFIDReaderChangedEventArgs : EventArgs
    {
        public double RFIDReaderValue { get; set; }
    }
    public interface IRFIDReader
    {
        event EventHandler<RFIDReaderChangedEventArgs> RFIDReaderChangedEvent;
    }
}
