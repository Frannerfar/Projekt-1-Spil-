using System;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace Projekt_1__Spil_
{
    internal class Program
    {
        // Spillepladen er et array af 9 felter (0–8), som starter med tallene 1–9
        // Så kan man se hvilke felter man kan vælge
        static char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        // Variabler til at holde styr på hvor mange brikker spiller og computer har
        static int playerPieces = 0;
        static int computerPieces = 0;
        static void Main(string[] args)
        {
            bool KørMenu = true;

            while (KørMenu)
            {
                // Tekst i menuen 
                Console.WriteLine();
                Console.WriteLine("Velkommen til spil menu");
                Console.WriteLine("spil 1: Sten, Saks, Papir");
                Console.WriteLine("spil 2: X & O (Kryds og Bolle)");
                Console.WriteLine("q. Afslut");
                Console.Write("\nVælg et spil: ");


                string valg = Console.ReadLine();

                // valgmuligheder til Menuen 
                switch (valg.ToLower()) // konverter input fra brugeren til lowercase
                {
                    case "1":
                        StenSaksPapirSpil();
                        break;

                    case "2":
                        XOGame();
                        break;
                    case "3":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Velkommen til X og O!");
                        Console.ForegroundColor = ConsoleColor.Black;
                        PlayGame();   // Kalder vores spilfunktion
                        break ;
                    case "q": // ToLower tager hensyn til 'q' og 'Q'

                        KørMenu = false;
                        Console.WriteLine("Programmet afsluttes. bye bye motherfucker");
                        break;
                    default:
                        Console.WriteLine("Fatfinger, prøv igen");
                        Console.ReadLine();
                        Console.Clear();
                        break;



                }



            }



        }

        // Funktion til spillet sten, saks, papir
        static void StenSaksPapirSpil()
        {
            int SpillerSejre = 0;
            int ComputerSejre = 0;
            Random random = new Random();
            bool SpilIgang = true;

            while (SpilIgang)
            {
                Console.Clear();
                Console.WriteLine("Sten, Saks, Papir");
                Console.WriteLine("Vælg: Sten (1), Saks (2), Papir(3), eller 'q' for at vende tilbage til menuen:");
                string input = Console.ReadLine();

                // Tager hensyn til små og store bogstaver
                if (input.ToLower() == "q")
                {
                    SpilIgang = false;
                    break;

                }
                // Tager højde for hvis spilleren tryk på andet end valgmulighederne
                int SpillerValg;
                if (!int.TryParse(input, out SpillerValg) || SpillerValg < 1 || SpillerValg > 3)
                {
                    Console.WriteLine("Hov du trykkede forkert. Prøv igen: ");
                    Console.ReadLine();
                    continue;
                }

                int ComputerValg = random.Next(1, 3);// Computeren random valg mellem sten, saks, papir

                Console.Clear();
                Console.WriteLine($"Du valgte:{Valg(SpillerValg)}");
                Console.WriteLine($"Computeren valgte:{Valg(ComputerValg)}");

                // Regler for når man vinder eller taber eller uafgjort
                if (SpillerValg == ComputerValg)
                {
                    Console.WriteLine("Uafgjort!");
                    Console.ReadLine();
                }
                else if ((SpillerValg == 1 && ComputerValg == 2) ||
                         (SpillerValg == 2 && ComputerValg == 3) ||
                         (SpillerValg == 3 && ComputerValg == 1))
                {
                    Console.WriteLine("Du vinder!");
                    SpillerSejre++;
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Computeren vinder! Du tabte! GG easy!");
                    ComputerSejre++;
                    Console.ReadLine();
                }
                //Scorerboard efter hvert spil
                Console.WriteLine($"\nstilling: Spiller:{SpillerSejre} - Computer: {ComputerSejre}");
                Console.Write("\nTryk på en tast for at fortsætte: ");
                Console.ReadLine();



            }


        }
        //Funktion for de forskellige valgmuligheder
        static string Valg(int valg)
        {
            switch (valg)
            {
                case 1: return "Sten";
                case 2: return "Saks";
                case 3: return "Papir";
                default: return "Prøv igen Spasser";

            }

        }//================================================================================

        // ====== X & 0 ======
        static void XOGame()
        {
            char[,] braet = new char[3, 3];
            InitBreat(braet);

            int spillerBrik = 0;
            int computerBrik = 0;
            bool spillerTur = true;
            bool spilIgang = true;


            while (spilIgang)
            {
                Console.Clear();
                Printbreat(braet);

                if (CheckVinder(braet, 'X'))
                {
                    Console.WriteLine("Du Vinder!");
                    spilIgang = false;
                    break;
                }
                else if (CheckVinder(braet, 'O'))
                {
                    Console.WriteLine("Computer Vinder!");
                    spilIgang = false;
                    break;
                }

                if (spillerTur)
                {
                    Console.WriteLine("Din tur (X)");
                    if (spillerBrik < 3)
                    {
                        // Placer ny brik 
                        PlacerBrik(braet, 'X');
                        spillerBrik++;
                    }
                    else
                    {
                        // Flyt en brik 
                        PlacerBrik(braet, 'X');
                    }

                }
                else
                {
                    Console.WriteLine("Computerens tur (O)");
                    Random rand = new Random();

                    if (computerBrik < 3)
                    {
                        //Computer placerer tilfældigt på braettet
                        PlacerRandom(braet, 'O', rand);
                        computerBrik++;
                    }
                    else
                    {
                        //Computeren flytter en tilfældig brik på braettet
                        FlytRandom(braet, 'O', rand);
                    }
                }

                spillerTur = !spillerTur;
            }

            Console.WriteLine("Tryk på en tast for at vende tilbage til menuen: ");
            Console.ReadLine();

        }
        // ===== Hjælpefunktioner til XO =====
        static void InitBreat(char[,] braet)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    braet[i, j] = ' ';
        }

        static void Printbreat(char[,] breat)
        {
            Console.WriteLine(" 0 1 2");
            for (int i = 0; i < 3; i++)
            {
                Console.Write(i + "");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(breat[i, j]);
                    if (j < 2) Console.Write("|");
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine("  -+-+-");
            }

        }

        static bool CheckVinder(char[,] breat, char symbol)
        {
            // Tjekker om man vinder i Rækker
            for (int i = 0; i < 3; i++)
                if (breat[i, 0] == symbol && breat[i, 1] == symbol && breat[i, 2] == symbol) return true;
            // Tjekker om man vinder i Koloner
            for (int j = 0; j < 3; j++)
                if (breat[j, 0] == symbol && breat[j, 1] == symbol && breat[j, 2] == symbol) return true;
            // Tjekker om man vinder diagonalt 
            if (breat[0, 0] == symbol && breat[1, 1] == symbol && breat[2, 2] == symbol) return true;
            if (breat[0, 2] == symbol && breat[1, 1] == symbol && breat[0, 2] == symbol) return true;

            return false;
        }
        // ===== Spiller funktioner X&O =====
        static void PlacerBrik(char[,] braet, char symbol)
        {
            while (true)
            {
                while (true)
                {
                    Console.Write("Indtast række (0-2): ");
                    int row = int.Parse(Console.ReadLine());
                    Console.Write("Indtast kolonne (0-2): ");
                    int col = int.Parse(Console.ReadLine());

                    if (braet[row, col] == ' ')
                    {
                        braet[row, col] = symbol;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Feltet er optaget, prøv igen!");
                    }
                }

            }
        }

        static void FlytBrik(char[,] braet, char symbol)
        {
            while (true)
            {
                Console.Write("Vælg brik du vil flytte (række): ");
                int r1 = int.Parse(Console.ReadLine());
                Console.Write("Vælg brik du vil flytte (kolonne): ");
                int k1 = int.Parse(Console.ReadLine());

                if (braet[r1, k1] == symbol)
                {
                    Console.Write("Vælg ny række (0-2): ");
                    int r2 = int.Parse(Console.ReadLine());
                    Console.Write("Vælg ny kolonne (0-2): ");
                    int k2 = int.Parse(Console.ReadLine());

                    if (braet[r2, k2] == ' ')
                    {
                        braet[r1, k1] = ' ';
                        braet[r2, k2] = symbol;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Det felt er optaget!");
                    }
                }
                else
                {
                    Console.WriteLine("Der er ingen af dine brikker der!");
                }
            }
        }
        // ===== Computer funktioner X&O =====
        static void PlacerRandom(char[,] braet, char symbol, Random rand)
        {
            while (true)
            {
                int r = rand.Next(0, 3);
                int k = rand.Next(0, 3);
                if (braet[r, k] == ' ')
                {
                    braet[r, k] = symbol;
                    break;
                }
            }
        }
        static void FlytRandom(char[,] braet, char symbol, Random rand)
        {
            while (true)
            {
                int r1 = rand.Next(0, 3);
                int k1 = rand.Next(0, 3);
                if (braet[r1, k1] == symbol)
                {
                    int r2 = rand.Next(0, 3);
                    int k2 = rand.Next(0, 3);
                    if (braet[r2, k2] == ' ')
                    {
                        braet[r1, k1] = ' ';
                        braet[r2, k2] = symbol;
                        break;
                    }
                }


            }
        }
        // ===== Highscore System =====
        


        
            
           
            static void PlayGame()
            {
                while (true)   // Kører uendeligt indtil vi bruger break
                {
                    PrintBoard(); // Tegner spillepladen
                    PlayerMove(); // Spilleren vælger et felt
                    if (CheckWin('X')) // Tjekker om spilleren har vundet
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Du vandt!");
                        break;              // Afslutter spillet
                    }

                    ComputerMove();         // Computeren vælger et felt
                    if (CheckWin('O'))      // Tjekker om computeren har vundet
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Computeren vandt!");
                        break;              // Afslutter spillet
                    }
                }
            }

            // Tegner spillepladen i konsollen
            static void PrintBoard()
            {
                Console.Clear();   // Rydder skærmen
                Console.WriteLine($"{board[0]} | {board[1]} | {board[2]}");
                Console.WriteLine("---------");
                Console.WriteLine($"{board[3]} | {board[4]} | {board[5]}");
                Console.WriteLine("---------");
                Console.WriteLine($"{board[6]} | {board[7]} | {board[8]}");
            }

            // Spilleren vælger et felt
            static void PlayerMove()
            {
                Console.Write("Vælg en position (1-9): ");
                int pos;

                // Vi bruger int.TryParse til at sikre at spilleren skriver et gyldigt tal
                // while‐løkke kører indtil vi har et tal mellem 1 og 9
                // og feltet ikke allerede er taget af X eller O
                while (!int.TryParse(Console.ReadLine(), out pos)
                       || pos < 1 || pos > 9
                       || board[pos - 1] == 'X' || board[pos - 1] == 'O')
                {
                    Console.Write("Ugyldigt valg, prøv igen: ");
                }

                // Når vi har et gyldigt felt → sæt et X på pladen
                board[pos - 1] = 'X';
                playerPieces++;
            }

            // Computeren vælger tilfældigt et felt
            static void ComputerMove()
            {
                Random rand = new Random();
                int pos;

                // Find et tilfældigt felt 1–9 der ikke er optaget
                do { pos = rand.Next(1, 10); }
                while (board[pos - 1] == 'X' || board[pos - 1] == 'O');

                // Sæt et O i feltet
                board[pos - 1] = 'O';
                computerPieces++;
            }

            // Tjekker om en spiller har 3 på stribe
            static bool CheckWin(char symbol)
            {
                // Alle mulige vinderrækker (rækker, kolonner, diagonaler)
                int[,] wins = {
            {0,1,2}, {3,4,5}, {6,7,8},   // rækker
            {0,3,6}, {1,4,7}, {2,5,8},   // kolonner
            {0,4,8}, {2,4,6}             // diagonaler
        };

                // Loop gennem alle kombinationer
                for (int i = 0; i < 8; i++)
                {
                    // Hvis alle tre felter indeholder samme symbol (X eller O)
                    if (board[wins[i, 0]] == symbol
                     && board[wins[i, 1]] == symbol
                     && board[wins[i, 2]] == symbol)
                        return true;  // Spilleren har vundet
                }
                return false;  // Ingen vinder endnu
            }
        }
    }


