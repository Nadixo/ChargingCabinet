using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingCabinet.Interfaces;

namespace ChargingCabinet.Models
{
    public class Display
    {
        public void ShowDisplay(string displayTekst)
        {
            Console.WriteLine(displayTekst);
        }
    }
}
