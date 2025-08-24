using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace PRB99.ASN.AirportBS.Daniel_Stanciu
{
    public static class RoomBooking
    {
        /////////DIT IS DE MENU VAN HOTEL OM KAMERS TE BOEKEN EN WELKE TYPE KAMER/////////
        public static void BookRoom(string filePath)
        {
            Console.WriteLine("Welke type kamer wil je boeken?");
            Console.WriteLine("1 - Basic");
            Console.WriteLine("2 - Comfort");
            Console.WriteLine("3 - Deluxe");
            Console.WriteLine("4 - Exclusive");

            /////////DE KEUZE DIE JE MAAKT/////////
            int keuze = int.Parse(Console.ReadLine());

            string type = "";

            if (keuze == 1)
            {
                type = "Basic";
            }
            if (keuze == 2)
            {
                type = "Comfort";
            }

            if (keuze == 3)
            {
                type = "Deluxe";
            }

            if (keuze == 4)
            {
                type = "Exclusive";
            }



            ////Leest lijn per lijn in de file en zoekt of de kamer vrij is, en dan word die aangepast naar bezet////

            string[] lijnen = File.ReadAllLines(filePath);

            for (int i = 0; i < lijnen.Length; i++)
            {
                if (lijnen[i].Contains(type) && lijnen[i].Contains("VRIJ"))
                {
                    lijnen[i] = lijnen[i].Replace("VRIJ", "BEZET");
                    File.WriteAllLines(filePath, lijnen);
                    Console.WriteLine("Kamer is geboekt!");
                    break;

                }
            }
            Console.WriteLine();
            Console.Write("Druk op een toets om terug te keren...");
            Console.ReadKey();
        }


        /////////DIT IS OM JE UIT TE CHECKKEN ZODAT HET AANPAST IN DE TXT FILE VAN BEZET NAAR VRIJ/////////
        public static void CheckoutRoom(string filePath)
        {
            Console.WriteLine("Voer het kamernummer in dat je wilt uitchecken:");
            if (int.TryParse(Console.ReadLine(), out int kamernummer))
            {
                var lijnen = File.ReadAllLines(filePath);
                for (int i = 0; i < lijnen.Length; i++)
                {
                    var onderdelen = lijnen[i].Split(',');
                    if (int.Parse(onderdelen[0]) == kamernummer && onderdelen[2].Trim() == "BEZET")
                    {
                        lijnen[i] = $"{onderdelen[0]},{onderdelen[1]},VRIJ";
                        File.WriteAllLines(filePath, lijnen);
                        Console.WriteLine("Kamer " + kamernummer + " is succesvol uitgecheckt!");
                        Console.ReadKey();
                        return;
                    }
                }
                Console.WriteLine("Kamer " + kamernummer + " is niet bezet of bestaat niet!");
                Console.Write("Druk op een toets om terug te keren...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Ongeldige invoer. Voer een geldig kamernummer in.");
                Console.Write("Druk op een toets om terug te keren...");
                Console.ReadKey();
            }
        }
    }
}
