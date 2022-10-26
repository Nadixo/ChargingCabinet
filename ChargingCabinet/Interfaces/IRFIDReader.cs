using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinet.Interfaces
{
    public class RFIDReaderEventArgs : EventArgs
    {
        public int RFIDReaderValue { get; set; }
    }
    public interface IRFIDReader
    {
        event EventHandler<RFIDReaderEventArgs> RFIDReaderChangedEvent;
    }
}
