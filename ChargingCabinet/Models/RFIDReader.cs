using ChargingCabinet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinet.Models
{
    internal class RFIDReader : IRFIDReader
    {

        public event EventHandler<RFIDReaderChangedEventArgs>? RFIDReaderChangedEvent;

        public void setRFIDState(int nRFID)
        {
            RFIDReaderChangedEvent?
                .Invoke(this, new RFIDReaderChangedEventArgs { RFIDReaderValue = nRFID } );
        }
    }
}
