using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Array_Oef6_2lagen
{
    internal class Program
    {

        // Tom Adriaens 
        // 15/40/2024
        // Oef 6 twee lagen

        // Velden
        static String[] _leerlingen = new string[0];
        static bool[] _AanBeurtGeweest = new bool[0];
        static bool _herhalen = true;



        // GUI
        static void Main(string[] args)
        {
            do
            {
                // Scherm leegmaken 
                Console.Clear();

                try
                {

                    // Toon menu
                    Console.WriteLine("Maak uw keuze uit onderstaand menu");
                    Console.WriteLine("   1) Maak array aan");

                    //Toon deel van het menu pas nadat array werd aangamakt 
                    if (ArrayAangemaakt())
                    {
                        Console.WriteLine("   2) Leerling ingeven\n   3) leerling aanpassen\n   4) leerling verwijderen\n   5) Leerlingen tonen \n   6) Afsluiten");
                    }

                    // Vraag keuze + opslaan 
                    Console.Write("\n\nuw keuze: ");
                    byte keuze = byte.Parse(Console.ReadLine());

                    // Scherm leegmaken 
                    Console.Clear();


                    // Als 1: maak array's 
                    if (keuze == 1)
                    {
                        bool herhalen1 = true;
                        do
                        {
                            try
                            {
                                // vraag de grootte van de klas 
                                Console.Write("\nhoe groot is uw klas: ");
                                int grootte = int.Parse(Console.ReadLine());

                                // stuur de grootte door
                                GrootteArrayAanpassen(grootte);


                                // Scherm leegmaken 
                                Console.Clear();

                                // bevestig
                                Console.WriteLine(StandaardAntwoorden(10));
                                Console.WriteLine(StandaardAntwoorden(0));
                                Console.ReadKey();

                                herhalen1 = false;
                            }
                            catch
                            {
                                Console.WriteLine(StandaardAntwoorden(1));
                                Console.WriteLine(StandaardAntwoorden(0));
                                Console.ReadKey();
                            }
                            
                        }
                        while (herhalen1);
                        

                    }

                    // Als 2: Leerling ingeven
                    else if (keuze == 2 && ArrayAangemaakt())
                    {
                        // Zoek een lege plaats
                        int plaats = ZoekLegePlaats();

                        if (plaats != -1)
                        {
                            // Vraag de gegevens van het monster
                            Console.Write("Geef de naam van de leerling in:");
                            string naam = Console.ReadLine();

                            LeerlingOpslaan(naam, plaats);

                            // Scherm leegmaken 
                            Console.Clear();

                            // fout code 
                            Console.WriteLine(StandaardAntwoorden(6));
                            Console.WriteLine(StandaardAntwoorden(0));
                            Console.ReadKey();
                        }
                        else
                        {
                            // fout code 
                            Console.WriteLine(StandaardAntwoorden(5));
                            Console.WriteLine(StandaardAntwoorden(0));
                            Console.ReadKey();
                        }
                    }

                    // Als 3: leerling aanpassen
                    else if (keuze == 3)
                    {
                        Console.WriteLine(ToonLeerlingen());
                        Console.WriteLine();

                        // Vraag het nummer van het mosnter
                        Console.Write("Geef het nummer van de leerling die u wilt aanpassen: ");
                        int nummer = (int.Parse(Console.ReadLine()) - 1);

                        // Vraag de gegevens van het monster
                        Console.Write("Geef de naam van de leerling in:");
                        string naam = Console.ReadLine();

                        LeerlingOpslaan(naam, nummer);

                        // Scherm leegmaken 
                        Console.Clear();

                        // begeleiden gebruiker  
                        Console.WriteLine(StandaardAntwoorden(8));
                        Console.WriteLine(StandaardAntwoorden(0));
                        Console.ReadKey();

                    }

                    // Als 4: Leerlingen verwijderen
                    else if (keuze == 4)
                    {
                        Console.WriteLine(ToonLeerlingen());
                        Console.WriteLine();

                        // Vraag het nummer van de leerling
                        Console.Write("Geef het nummer van de leerlingen die u wilt verwijderen: ");
                        int nummer = (int.Parse(Console.ReadLine()) - 1);

                        LeerlingOpslaan(null, nummer);

                        // Scherm leegmaken 
                        Console.Clear();

                        // fout code 
                        Console.WriteLine(StandaardAntwoorden(9));
                        Console.WriteLine(StandaardAntwoorden(0));
                        Console.ReadKey();
                    }

                    // Als 5: Tonen
                    else if (keuze == 5)
                    {
                        // Toon menu
                        Console.WriteLine("Maak uw keuze uit onderstaand menu");
                        Console.WriteLine("   1) Willekeurige leerling tonen\n   2) Alle leerlingen tonen in wel of niet geweest");

                        try
                        {

                            // Vraag keuze + opslaan 
                            Console.Write("\n\nuw keuze: ");
                            keuze = byte.Parse(Console.ReadLine());

                            // Scherm leegmaken 
                            Console.Clear();

                            //willekeurige lln tonen
                            if (keuze == 1)
                            {
                                int willekeurigeLln = KiesWillekeurigeLeerling();
                                
                                // Als de waarde van willekeurige leerling niet -1is, dan moet ik zijn naam tonen 
                                if(willekeurigeLln != -1)
                                {
                                    // Roep de naam op van de leerling en toon deze op het scherm
                                    Console.WriteLine($"Volgende leerling moet naar voor komen: {NaamLeerling(willekeurigeLln)}");

                                    Console.WriteLine(StandaardAntwoorden(0));
                                    Console.ReadKey();

                                    // Scherm leegmaken 
                                    Console.Clear();

                                    //stel vraag of de leerling effectief naar voren is gekomen.
                                    Console.WriteLine("Is de leerling aanbod gekomen? (J/N)");
                                    string antwoord = Console.ReadLine();


                                    if (antwoord.Substring(0, 1).ToUpper() == "J")
                                    {
                                        LeerlingAanbodGekomen(willekeurigeLln);
                                    }

                                }
                                else
                                {
                                    // fout code 
                                    Console.WriteLine(StandaardAntwoorden(11));
                                    Console.WriteLine(StandaardAntwoorden(0));

                                    Console.ReadKey();
                                }
                            }

                            // alle leerlingen tonen in 2 groepen
                            else if (keuze == 2)
                            {
                                Console.WriteLine(ToonleerlingInGroep());
                            }
                            else
                            {
                                // fout code 
                                Console.WriteLine(StandaardAntwoorden(3));
                                Console.WriteLine(StandaardAntwoorden(0));

                            }

                            Console.ReadKey();
                        }
                        catch
                        {
                            // Scherm leegmaken 
                            Console.Clear();

                            // fout code 
                            Console.WriteLine(StandaardAntwoorden(1));
                            Console.WriteLine(StandaardAntwoorden(0));
                            Console.ReadKey();
                        }
                    }

                    // Als Afsluiten 
                    else if (keuze == 6)
                    {
                        // fout code 
                        Console.WriteLine(StandaardAntwoorden(4));
                        Console.WriteLine(StandaardAntwoorden(2));
                        Console.ReadKey();
                    }

                    // in elk ander geval 
                    else
                    {
                        // fout code 
                        Console.WriteLine(StandaardAntwoorden(3));
                        Console.WriteLine(StandaardAntwoorden(0));
                        Console.ReadKey();
                    }
                }
                catch
                {
                    // Scherm leegmaken 
                    Console.Clear();

                    // fout code 
                    Console.WriteLine(StandaardAntwoorden(1));
                    Console.WriteLine(StandaardAntwoorden(0));
                    Console.ReadKey();
                }
            } while (_herhalen);

        }

        //Functies
        /// <summary>
        /// checks if the array already has some room in it
        /// </summary>
        /// <returns> false: array has not been altered , True: Array has been altered </returns>
        static public bool ArrayAangemaakt()
        {
            bool antwoord = false;

            if (_leerlingen.Count() != 0)
            {
                antwoord = true;
            }

            return antwoord;
        }

        /// <summary>
        /// update the number of spaces in the array 
        /// </summary>
        /// <param name="ontvAantal"></param>
        static public void GrootteArrayAanpassen(int ontvAantal)
        {
            _leerlingen = new string[ontvAantal];
            _AanBeurtGeweest = new bool[ontvAantal];
        }

        static public int ZoekLegePlaats()
        {
            int antwoord = -1;

            for (int i = 0; i < _leerlingen.Count(); i++)
            {
                if (_leerlingen[i] == null)
                {
                    antwoord = i;
                    break;
                }
            }

            return antwoord;
        }

        /// <summary>
        /// Add a studentsname to the array
        /// </summary>
        /// <param name="naam"></param>
        /// <param name="nrIndex"></param>
        static public void LeerlingOpslaan(String naam, int nrIndex)
        {
            _leerlingen[nrIndex] = naam;

            // Wanneer de naam null is, wil ik de leerling verwijderen en 
            // moet ik dus ook zijn gegevens uit de andere array verwijderen.
            if(naam == null)
            {
                _AanBeurtGeweest[nrIndex] = false;
            }
        }

        // Toon alle leerlingen
        static public String ToonLeerlingen()
        {
            string antwoord = null;

            for (int i = 0; i < _leerlingen.Count(); i++)
            {
                //Vergeet het + teken niet !!! anders maar 1 naam, vergeet ook de nieuwe lijn niet.
                antwoord += $"{(i + 1).ToString()})  {_leerlingen[i]} \n";

            }

            return antwoord;
        }

        /// <summary>
        /// choose a random student
        /// </summary>
        /// <returns></returns>
        static public int KiesWillekeurigeLeerling()
        {
            int antwoord = 0;
            Random rdm = new Random();

            int index = -1;

            // kijk of er nog iemand aan beurt moet komen
            for (int i = 0; i < _leerlingen.Count(); i++)
            {
                if (_AanBeurtGeweest[i] == false && _leerlingen[i] != null)
                {
                    index = i;
                    break;
                }

            }

            // als nog iemand aan de beurd moet komen, neem dan een willekeurig leerling 
            if (index != -1)
            {
                bool herhalen = true;

                do
                {
                    index = rdm.Next(_leerlingen.Count());

                    // Kijk of de leerling aan alle voorwaarden voldoet
                    // 1: hij moet nog aan beurt komen (dus _aanbeurtGeweest op false)
                    // 2: er moet een naam zijn ingevuld, anders is het een lege plek
                    if (_AanBeurtGeweest[index] == false && _leerlingen[index] != null)
                    {
                        herhalen = false;
                    }
                }
                while (herhalen);

            }


            // stuur de naam door van de willekeurig leeringen
            antwoord = index;

            return antwoord;
        }

        // toon naam leerling
        static public string NaamLeerling(int ontvIndex)
        {
            String antwoord = null;
                
            antwoord= _leerlingen[ontvIndex];

            return antwoord;
        }

        // Zet leerling als aanbod gekomen
        static void LeerlingAanbodGekomen(int ontvIndex)
        {
            _AanBeurtGeweest[ontvIndex] = true;
        }


        // toon alle leerlingen in 2 groepen 
        static public String ToonleerlingInGroep()
        {
            string antwoord = null;

            antwoord = "Volgende mensen kwamen reeds aanbod: \n";

            for(int i = 0; i < _leerlingen.Count(); i++)
            {
                if (_AanBeurtGeweest[i] == true)
                {
                    antwoord += $"   {(i + 1).ToString()})  {_leerlingen[i]} \n";
                }
                    
            }
            antwoord += "Volgende mensen moeten nog aanbod komen: \n";

            for (int i = 0; i < _leerlingen.Count(); i++)
            {
                if (_AanBeurtGeweest[i] == false)
                {
                    antwoord += $"{(i + 1).ToString()})  {_leerlingen[i]} \n";
                }

            }



            return antwoord;
        }

        /// <summary>
        /// Gives standard responses and errors with code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        static public String StandaardAntwoorden(int code)
        {
            String antwoord = null;

            if (code == 0)
            {
                antwoord = "Druk op een toets om terug te keren naar het hoofdmenu";
            }
            else if (code == 1)
            {
                antwoord = "U gaf geen getal in.";
            }
            else if (code == 2)
            {
                antwoord = "Druk op een toets om af te sluiten";
            }
            else if (code == 3)
            {
                antwoord = "U gaf geen geldige keuze in";
            }
            else if (code == 4)
            {
                antwoord = "Tot een volgende keer";
            }
            else if (code == 5)
            {
                antwoord = "Er is geen plaats meer.";
            }
            else if (code == 6)
            {
                antwoord = "De leerling werd opgeslagen.";
            }
            else if (code == 7)
            {
                antwoord = "U gaf geen juist lvl in.";
            }
            else if (code == 8)
            {
                antwoord = "De leerling werd aangepast.";
            }
            else if (code == 9)
            {
                antwoord = "De leerling verwijderd.";
            }
            else if (code == 10)
            {
                antwoord = "de grootte van de array werd aangepast";
            }
            else if (code == 11)
            {
                antwoord = "Alle leerlingen zijn aanbod gekomen.";
            }
            return antwoord;
        }

    }
}
