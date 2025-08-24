using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRB99.ASN.AirportBS.Daniel_Stanciu
{
    public class HotelManagement
    {
        private readonly string filePath = "hotelkamers.txt";

        public void Start()
        {
            if (!File.Exists(filePath))
            {
                RoomFileManager.CreateRoomFile(filePath);
            }

            int keuze;
            do
            {
                /////////MENU VOOR HOTEL/////////
                Console.Clear();  // Maak de console leeg
                Console.WriteLine("Welkom bij Hotel!");
                Console.WriteLine("Maak je keuze uit:");
                Console.WriteLine("1 - Toon overzicht van alle kamers");
                Console.WriteLine("2 - Boek een kamer");
                Console.WriteLine("3 - Uitchecken");
                Console.WriteLine("0 - Hoofdmenu");

                if (int.TryParse(Console.ReadLine(), out keuze))
                {
                    switch (keuze)
                    {
                        case 1:
                            RoomFileManager.ShowRooms(filePath);
                            break;
                        case 2:
                            RoomBooking.BookRoom(filePath);
                            break;
                        case 3:
                            RoomBooking.CheckoutRoom(filePath);
                            break;
                        case 0:
                            Console.WriteLine("Terug naar het hoofdmenu!");
                            return;  // Terug naar het hoofdmenu
                        default:
                            Console.WriteLine("Ongeldige keuze.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Ongeldige keuze. Voer een geldig nummer in.");
                }

            } while (true); // Blijft doorgaan tot je op 0 drukt
        }
    }
}
