using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRB99.ASN.AirportBS.Daniel_Stanciu
{
    public static class RoomFileManager
    {
        /////////DIT IS DE INFO VOOR DE KAMERS TE MAKEN MANUEEL/////////
        public static void CreateRoomFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 1; i <= 10; i++) writer.WriteLine(i + ",Basic,VRIJ");
                for (int i = 11; i <= 15; i++) writer.WriteLine(i + ",Comfort,VRIJ");
                for (int i = 16; i <= 20; i++) writer.WriteLine(i + ",Deluxe,VRIJ");
                for (int i = 21; i <= 25; i++) writer.WriteLine(i + ",Exclusive,VRIJ");

            }
        }

        /////////DIT LAAT DE KAMERS ZIEN DIE WE HEBBEN AANGEMAAKT EN DE BESCHIKBAARHEID VAN DE KAMER/////////
        public static void ShowRooms(string filePath)
        {
            Console.Clear();
            Console.WriteLine("Overzicht Hotelkamers:");
            foreach (var line in File.ReadAllLines(filePath))
            {
                var onderdelen = line.Split(',');
                Console.WriteLine("Kamer " + onderdelen[0] + " || " + onderdelen[1] + " - Status: " + onderdelen[2]);

            }
            Console.WriteLine();
            Console.Write("Druk op een toets om terug te keren...");
            Console.ReadKey();
        }
    }
}
