using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRB99.ASN.AirportBS.Daniel_Stanciu
{
    abstract class Luchtvoertuig
    {
        public string ID { get; set; }
        public string Naam { get; set; }
        public bool Onderweg { get; set; }

        // MTR - Multi-dimensional array
        public char[,] Zitplaatsen { get; set; }

        protected Luchtvoertuig(string id, string naam, int rijen, int kolommen)
        {
            ID = id;
            Naam = naam;
            Onderweg = false;

            // MTR - Multi-dimensional array
            Zitplaatsen = new char[rijen, kolommen];
            for (int r = 0; r < rijen; r++)
            {
                for (int c = 0; c < kolommen; c++)
                {
                    Zitplaatsen[r, c] = '-';
                }

            }

        }

        public void SeatMap()
        {
            Console.Write("  ");
            for (int c = 0; c < Zitplaatsen.GetLength(1); c++)
            {
                Console.Write((c + 1).ToString("00") + " ");
            }
               
            Console.WriteLine();


            for (int r = 0; r < Zitplaatsen.GetLength(0); r++)
            {
                char row = (char)('A' + r);
                Console.Write(row + " ");

                if (row == 'D') //gang
                {
                    for (int c = 0; c < Zitplaatsen.GetLength(1); c++)
                    {
                        Console.Write("   ");
                    } 
                }
                else
                {
                    for (int c = 0; c < Zitplaatsen.GetLength(1); c++)
                    {
                        Console.Write(Zitplaatsen[r, c] + "  ");
                    }
                        
                }
                Console.WriteLine();
            }
        }
        public void BezetInfo()
        {
            int vrij = 0;
            int bezet = 0;

            for (int r = 0; r < Zitplaatsen.GetLength(0); r++)
            {
                for (int c = 0; c < Zitplaatsen.GetLength(1); c++)
                {
                    char ch = Zitplaatsen[r, c];
                    if (ch == '-')
                    {
                        vrij++;

                    }
                    else if (ch == 'X')
                    {
                        bezet++;
                    } 
                }
            }

            int totaal = vrij + bezet;
            int bzt = (totaal == 0) ? 0 : (int)Math.Round((double)bezet * 100 / totaal);

            Console.WriteLine();
            Console.WriteLine("Aantal beschikbare stoelen: " + vrij);
            Console.WriteLine("Aantal gereserveerde stoelen: " + bezet);
            Console.WriteLine("Bezettingsgraad: " + bzt + "%");


        }

        //VLUCHTEN//
        public void LeegmakenBijLanden()
        {
            for (int r = 0; r < Zitplaatsen.GetLength(0); r++)
            {
                for (int c = 0; c < Zitplaatsen.GetLength(1); c++)
                {
                    if (Zitplaatsen[r, c] == 'X')
                    {
                        Zitplaatsen[r, c] = '-';
                    }
                }
            }


        }


        // MTR - Foreach
        public bool HeeftPassagiers()
        {
            foreach (var ch in Zitplaatsen)
            {
                if (ch == 'X')
                {
                    return true;
                } 
            }
                
            return false;
        }
        public bool IsVol()
        {
            for (int r = 0; r < Zitplaatsen.GetLength(0); r++)
            {
                for (int c = 0; c < Zitplaatsen.GetLength(1); c++)
                {
                    if (Zitplaatsen[r, c] == '-')
                    {
                        return false;
                    } 
                }
            }
            return true;
        }
        public bool KanOpstijgen()
        {
            foreach (var stoel in Zitplaatsen)
                if (stoel == 'X')
                {
                    return true;
                }    
            return false;
        }

        // MTR - Tuple or ref/out parameters
        public bool AutoBooking(out char rij, out int kolom)
        {
            rij = '?';
            kolom = 0;

            for (int r = 0; r < Zitplaatsen.GetLength(0); r++)
            {
                char rijLabel = (char)('A' + r);
                if (rijLabel == 'D') 
                {
                    continue;

                } 

                for (int c = 0; c < Zitplaatsen.GetLength(1); c++)
                {
                    if (Zitplaatsen[r, c] == '-')
                    {
                        Zitplaatsen[r, c] = 'X';
                        rij = rijLabel;
                        kolom = c + 1;
                        return true;
                    }
                }
            }
            return false;

        }

        //INFO//
        public abstract void ToonExtraInfo();
        protected void ToonFoutmelding(string bericht)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(bericht);
            Console.ResetColor();
        }
    }
}
