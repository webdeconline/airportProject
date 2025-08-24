using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRB99.ASN.AirportBS.Daniel_Stanciu
{
    //////////HIER NEMEN WE WAT WE NODIG HEBBEN VAN DE ANDERE KLASSEN OM HET HIER TE LATEN WERKEN EN OMGEKEERD//////////
    class Helikopter : Luchtvoertuig
    {
        public bool IsMilitair { get; set; }

        public Helikopter(string id, string naam, int rijen, int kolommen, bool isMilitair)
            : base(id, naam, rijen, kolommen)
        {
            IsMilitair = isMilitair;
        }

        //////////DIT IS EEN MENU VAN VLUCHTEN HELIKOPTER//////////
        public override void ToonExtraInfo()
        {
            if (IsMilitair || Onderweg || !KanOpstijgen())
            {
                Console.ForegroundColor = ConsoleColor.Red;

                if (IsMilitair)
                    Console.WriteLine("Informatie over deze militaire helikopter is niet beschikbaar.");
                else if (Onderweg)
                    Console.WriteLine("Deze helikopter is vertrokken.");
                else if (!KanOpstijgen())
                    Console.WriteLine("Deze helikopter heeft geen vrije zitplaatsen.");

                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Type: Helikopter, Militair: " + (IsMilitair ? "Ja" : "Nee") + ", Status: " + (Onderweg ? "Onderweg" : "Aan de gate"));

            }
        }
    }
}
