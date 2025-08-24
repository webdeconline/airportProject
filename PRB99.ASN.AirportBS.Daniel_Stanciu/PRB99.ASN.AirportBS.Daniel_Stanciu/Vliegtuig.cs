using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRB99.ASN.AirportBS.Daniel_Stanciu
{
    class Vliegtuig : Luchtvoertuig
    {
        /////////DIT IS WAT WE NODIG HEBBEN OM TE GEBRUIKEN IN ONS PROGRAMMA EN ANDERE KLASSES/////////
        public Vliegtuig(string id, string naam, int rijen, int kolommen) : base(id, naam, rijen, kolommen) { }

        public override void ToonExtraInfo()
        {
            Console.WriteLine("Type: Vliegtuig");
            Console.WriteLine("Naam: " + Naam);
            Console.WriteLine("Status: " + (Onderweg ? "Onderweg" : "Aan de gate"));
            SeatMap();
            BezetInfo();


        }
    }
}
