using ChargingCabinet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinet.Models
{
    public class RFIDReader : IRFIDReader
    {

        public event EventHandler<RFIDReaderEventArgs>? RFIDReaderChangedEvent;

        public void setRFIDState(int nRFID)
        {
            RFIDReaderChangedEvent?
                .Invoke(this, new RFIDReaderEventArgs { RFIDReaderValue = nRFID } );
        }
    }
}
