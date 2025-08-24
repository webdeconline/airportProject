using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRB99.ASN.AirportBS.Daniel_Stanciu
{
    class LuchtvoertuigBeheer
    {
        //////////HIER ZORGEN WE ERVOOR DAT DE MAPPEN BESTAAN///////////
        string folderVliegtuigen = Path.Combine("Data", "Vliegtuigen");
        string folderHelikopters = Path.Combine("Data", "Helikopters");

        // MTR - List<>
        //////////HIER MAKEN WE EEN NIEUWE LIST VAN VOERTUIGEN//////////
        private List<Luchtvoertuig> voertuigen = new List<Luchtvoertuig>();


        //////////DIT ZIJN 2 VOERTUIGEN DIE IK MANUEEL HEB INGEVOERD ALS TEST//////////
        public LuchtvoertuigBeheer()
        {
            Directory.CreateDirectory(folderVliegtuigen);
            Directory.CreateDirectory(folderHelikopters);

            LaadVliegtuigen();
            LaadHelikopters();

            if (voertuigen.Count == 0)
            {
                var v = new Vliegtuig("V001", "Boeing 747", 7, 9);
                voertuigen.Add(v);
                SaveVliegtuig(v);


                var h = new Helikopter("H001", "Apache", 2, 4, true);
                voertuigen.Add(h);
                SaveHelikopter(h);
            }
        }


        //////////HIER START DE MENU VOOR VLUCHTEN//////////
        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Overzicht van luchtvoertuigen");
                Console.WriteLine("2. Luchtvoertuig vertrekt/aankomt");
                Console.WriteLine("3. Toon detailinformatie van een voertuig");
                Console.WriteLine("4. Boek 1 passagier");
                Console.WriteLine("0. Terug");

                if (!int.TryParse(Console.ReadLine(), out int keuze) || keuze < 0 || keuze > 4)
                {
                    Console.WriteLine("Ongeldige keuze.");
                    continue;
                }

                if (keuze == 0) break;

                switch (keuze)
                {
                    case 1:
                        ToonVoertuigen(); //deze keuze brengt je bij klasse ToonVoertuigen
                        break;
                    case 2:
                        VertrekAankomst(); //deze keuze brengt je bij klasse VertrekAankomst
                        break;
                    case 3:
                        ToonDetails(); //deze keuze brengt je bij klasse ToonDetails
                        break;
                    case 4:
                        BoekPassagier(); //Deze boekt een passagier
                        break;
                }
            }
        }

        //////////HIER BEGINNEN DE KLASSEN//////////

        //////////KLASSEN OM DE BESCHIKBARE VLIEGTUIGEN TE LATEN ZIEN//////////
        private void ToonVoertuigen()
        {
            Console.WriteLine("Overzicht van luchtvoertuigen:");
            PrintVoertuigenOverzicht();



            Console.WriteLine("Druk op een toets om terug te keren naar het menu.");
            Console.ReadKey();
        }

        //////laat de beschikbare vluchten zien//////
        private void PrintVoertuigenOverzicht()
        {
            foreach (var voertuig in voertuigen)
            {
                Console.WriteLine("" + voertuig.ID + " - " + voertuig.Naam + " - Status: " + (voertuig.Onderweg ? "Onderweg" : "Aan de gate"));
            }
        }

        //////////KLASSE OM TE LATEN ZIEN WAT DE VOERTUIG AAN HET DOEN IS//////////
        private void VertrekAankomst()
        {
            Console.Clear();
            Console.WriteLine("Beschikbare vluchten");
            PrintVoertuigenOverzicht();

            Console.Write("Geef het ID van het voertuig dat vertrekt of aankomt: ");
            string id = Console.ReadLine();
            var v = voertuigen.Find(x => x.ID == id);

            if (v == null)
            {
                Console.WriteLine("Voertuig niet gevonden.");

            }
            else
            {
                if (!v.Onderweg)
                {
                    if (v is Helikopter hk && hk.IsMilitair)
                    {
                        Console.WriteLine("Kan niet vertrekken: militaire helikopter.");
                    }
                    else if (!v.KanOpstijgen())
                    {
                        Console.WriteLine("Kan niet vertrekken: geen passagiers aan boord.");
                    }

                    else
                    {
                        v.Onderweg = false;
                        v.LeegmakenBijLanden();
                        Console.WriteLine(v.Naam + " is geland. Passagiers uitgestapt.");


                        if (v is Vliegtuig vv)
                        {
                            SaveVliegtuig(vv);

                        }
                        else if (v is Helikopter hh)
                        {
                            SaveHelikopter(hh);
                        }
                    }
                }
            }
            Console.WriteLine("Druk op een toets om terug te keren naar het menu.");
            Console.ReadKey();
        }

        //////////KLASSE OM DE DETAILS VAN HET VOERTUIG TE LATEN ZIEN//////////
        private void ToonDetails()
        {
            Console.Clear();
            Console.WriteLine("Beschikbare vluchten");
            PrintVoertuigenOverzicht();

            Console.Write("Geef voertuig ID: ");
            string id = Console.ReadLine();
            var v = voertuigen.Find(x => x.ID == id);
            if (v != null)
            {
                v.ToonExtraInfo();
            }

            else
            {
                Console.WriteLine("Voertuig niet gevonden.");
            }

            Console.WriteLine("Druk op een toets om terug te keren naar het menu.");
            Console.ReadKey();
        }

        /////////KLASSE OM EEN PASSAGIER TE BOEKEN////////
        private void BoekPassagier()
        {
            Console.Clear();
            Console.WriteLine("Beschikbare vluchten");
            PrintVoertuigenOverzicht();
            Console.Write("Kies een ID voor boeking: ");
            string id = (Console.ReadLine() ?? "").Trim();

            Luchtvoertuig v = null;
            foreach (var item in voertuigen)
            {
                if (item.ID == id)
                {
                    v = item;
                    break;
                }
            }
            if (v == null)
            {
                Console.WriteLine("Voertuig niet gevonden");
                Console.WriteLine();
                Console.Write("Druk op een toets om terug te keren...");
                Console.ReadKey();
                return;
            }
            if (v.Onderweg)
            {
                Console.WriteLine("Kan geen boeking maken: voertuig is onderweg.");
                Console.ResetColor();
                Console.WriteLine();
                Console.Write("Druk op een toets om terug te keren...");
                Console.ReadKey();
                return;
            }
            if (v is Helikopter && ((Helikopter)v).IsMilitair)
            {
                Console.WriteLine("Kan geen boeking maken: militaire helikopter.");
                Console.WriteLine();
                Console.Write("Druk op een toets om terug te keren...");
                Console.ReadKey();
                return;
            }
            if (v.IsVol())
            {
                Console.WriteLine("Kan geen boeking maken: vliegtuig is vol.");
                Console.WriteLine();
                Console.Write("Druk op een toets om terug te keren...");
                Console.ReadKey();
                return;
            }

            char rij;
            int kolom;
            bool gelukt = v.AutoBooking(out rij, out kolom);

            if (gelukt)
            {
                Console.WriteLine("Stoel " + rij + kolom.ToString("00") + " geboekt.");
                if (v is Vliegtuig vv) SaveVliegtuig(vv);
                else if (v is Helikopter hh) SaveHelikopter(hh);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Boeking mislukt: geen geschikte stoel gevonden.");
                Console.ResetColor();
            }
            Console.WriteLine();
            Console.Write("Druk op een toets om terug te keren...");
            Console.ReadKey();
        }


        ////////GEGEVENS OPSLAAN////////
        private void SaveVliegtuig(Vliegtuig v)
        {
            string path = Path.Combine(folderVliegtuigen, v.ID + ".map");
            var lines = new List<string> { v.Naam };


            for (int r = 0; r < v.Zitplaatsen.GetLength(0); r++)
            {
                string row = "";
                for (int c = 0; c < v.Zitplaatsen.GetLength(1); c++)
                {
                    row += v.Zitplaatsen[r, c];
                }
                lines.Add(row);
            }
            File.WriteAllLines(path, lines);
        }

        private void SaveHelikopter(Helikopter h)
        {
            string path = Path.Combine(folderHelikopters, h.ID + ".map");
            var lines = new List<string> { h.IsMilitair ? h.Naam + ",M" : h.Naam };
            for (int r = 0; r < h.Zitplaatsen.GetLength(0); r++)
            {
                string row = "";
                for (int c = 0; c < h.Zitplaatsen.GetLength(1); c++)
                {
                    row += h.Zitplaatsen[r, c];
                }
                lines.Add(row);
            }
            File.WriteAllLines(path, lines);
        }

        ////////LAAD GEGEVENS OP////////
        private void LaadVliegtuigen()
        {
            foreach (var file in Directory.GetFiles(folderVliegtuigen, "*.map"))
            {
                string id = Path.GetFileNameWithoutExtension(file);
                string[] lines = File.ReadAllLines(file);
                if (lines.Length < 2)
                {
                    continue;
                }

                string naam = lines[0];
                int rijen = lines.Length - 1;
                int kol = lines[1].Length;

                var v = new Vliegtuig(id, naam, rijen, kol);
                for (int r = 0; r < rijen; r++)
                    for (int c = 0; c < kol; c++)
                    {
                        v.Zitplaatsen[r, c] = lines[r + 1][c];
                    }
                voertuigen.Add(v);
            }
        }
        private void LaadHelikopters()
        {
            foreach (var file in Directory.GetFiles(folderHelikopters, "*.map"))
            {
                string id = Path.GetFileNameWithoutExtension(file);
                string[] lines = File.ReadAllLines(file);
                if (lines.Length < 2)
                {
                    continue;
                }

                string header = lines[0].Trim();
                bool militair = header.EndsWith(",M", StringComparison.OrdinalIgnoreCase);
                string naam = militair ? header.Substring(0, header.Length - 2) : header;

                int rijen = lines.Length - 1;
                int kol = lines[1].Length;

                var h = new Helikopter(id, naam, rijen, kol, militair);
                for (int r = 0; r < rijen; r++)
                {
                    for (int c = 0; c < kol; c++)
                    {
                        h.Zitplaatsen[r, c] = lines[r + 1][c];
                    }

                }
                voertuigen.Add(h);
            }
        }
    }
}
