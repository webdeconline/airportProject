using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRB99.ASN.AirportBS.Daniel_Stanciu
{
    internal class Program
    {
        //Daniel Stanciu
        //EindOpdracht Airport
        //Programmeren Basis


        ///////DIT IS DE HOOFDMENU VAN DE PROGRAMMA////////
        static void Main(string[] args)
        {
            bool continueProgram = true;

            while (continueProgram)
            {
                Console.Clear();  // Maak de console leeg
                Console.WriteLine("Welkom bij de luchthaven!");
                Console.WriteLine("Maak je keuze tussen:");
                Console.WriteLine("1 - Vluchten");
                Console.WriteLine("2 - Hotel");
                Console.WriteLine("0 - Afsluiten");

                if (int.TryParse(Console.ReadLine(), out int keuze))
                {
                    switch (keuze)
                    {
                        case 1:
                            var vluchten = new LuchtvoertuigBeheer();
                            vluchten.Start();
                            break;
                        case 2:
                            var hotel = new HotelManagement();
                            hotel.Start();
                            break;
                        case 0:
                            Console.WriteLine("Fijne dag!");
                            continueProgram = false;
                            break;
                        default:
                            Console.WriteLine("Ongeldige keuze.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Ongeldige invoer.");
                }
            }
        }
    }
}
