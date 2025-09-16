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
                Console.WriteLine("Tryk 1 for:  Sten, Saks, Papir");
                Console.WriteLine("Tryk 2 for:  Virker ikke længere");
                Console.WriteLine("Tryk 3 for:  X & O, meget bedre");
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
                        bool playAgain = true;

                        while (playAgain)
                        {
                            Console.Clear();
                            Console.WriteLine("Velkommen til Blackjack!\n");

                            StartGame();

                            Console.WriteLine("\nVil du spille igen? (j/n)");
                            string again = Console.ReadLine().ToLower();
                            playAgain = (again == "j");
                        }

                        Console.WriteLine("Tak for spillet!");
                        break;
                    case "3":
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


        // ===== Highscore System =====





        static void PlayGame()
            {
            
                //Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Velkommen til X og O!");
                //Console.ForegroundColor = ConsoleColor.Black;
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
                
                //===== Nice to have features ======
                // Spørger om brugeren vil spille igen 
                // En quit option efter spillet Console.writeLine ("Tryk q for at gå tilbage til menuen")
            }
        //===========================================================================================


        static void StartGame()
        {

            string[] playerHand = new string[0];
            string[] dealerHand = new string[0];

            // Start-hænder
            playerHand = AddCard(playerHand, DrawCard());
            playerHand = AddCard(playerHand, DrawCard());

            dealerHand = AddCard(dealerHand, DrawCard());
            dealerHand = AddCard(dealerHand, DrawCard());

            bool playerBust = PlayerTurn(ref playerHand, dealerHand);

            if (!playerBust)
            {
                DealerTurn(ref dealerHand);
            }

            DetermineWinner(playerHand, dealerHand, playerBust);
        }

        static bool PlayerTurn(ref string[] playerHand, string[] dealerHand)
        {
            bool playerTurn = true;
            bool playerBust = false;

            while (playerTurn)
            {
                Console.WriteLine("Dine kort: " + string.Join(", ", playerHand) + $" (Total: {HandValue(playerHand)})");
                Console.WriteLine($"Dealers kort: {dealerHand[0]}, ?");

                Console.WriteLine("\nVil du [H]it eller [S]tand?");
                string choice = Console.ReadLine().ToLower();

                if (choice == "h")
                {
                    playerHand = AddCard(playerHand, DrawCard());
                    if (HandValue(playerHand) > 21)
                    {
                        Console.WriteLine("Du trækker et kort og går bust!");
                        playerBust = true;
                        playerTurn = false;
                    }
                }
                else if (choice == "s")
                {
                    playerTurn = false;
                }
            }

            return playerBust;
        }

        static void DealerTurn(ref string[] dealerHand)
        {
            Console.WriteLine("\n--- Dealerens tur ---");
            Console.WriteLine("Dealerens kort: " + string.Join(", ", dealerHand) + $" (Total: {HandValue(dealerHand)})");

            while (HandValue(dealerHand) < 17)
            {
                Console.WriteLine("Dealer trækker et kort...");
                dealerHand = AddCard(dealerHand, DrawCard());
                Console.WriteLine("Dealerens kort: " + string.Join(", ", dealerHand) + $" (Total: {HandValue(dealerHand)})");
            }
        }

        static void DetermineWinner(string[] playerHand, string[] dealerHand, bool playerBust)
        {
            int playerTotal = HandValue(playerHand);
            int dealerTotal = HandValue(dealerHand);

            Console.WriteLine("\n--- Resultat ---");
            Console.WriteLine($"Dine kort: {string.Join(", ", playerHand)} (Total: {playerTotal})");
            Console.WriteLine($"Dealerens kort: {string.Join(", ", dealerHand)} (Total: {dealerTotal})");

            if (playerBust)
            {
                Console.WriteLine("Du tabte! (Bust)");
            }
            else if (dealerTotal > 21)
            {
                Console.WriteLine("Dealer går bust! Du vinder!");
            }
            else if (playerTotal > dealerTotal)
            {
                Console.WriteLine("Du vinder!");
            }
            else if (playerTotal < dealerTotal)
            {
                Console.WriteLine("Dealer vinder!");
            }
            else
            {
                Console.WriteLine("Uafgjort!");
            }
        }

        // ---------------- HJÆLPEFUNKTIONER ----------------
        static Random random = new Random();
        static string DrawCard()
        {
            string[] cards = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            return cards[random.Next(cards.Length)];
        }

        static int CardValue(string card)
        {
            if (card == "J" || card == "Q" || card == "K")
                return 10;
            else if (card == "A")
                return 11; // håndteres senere hvis bust
            else
                return int.Parse(card);
        }

        static int HandValue(string[] hand)
        {
            int total = 0;
            int aceCount = 0;

            foreach (string card in hand)
            {
                int value = CardValue(card);
                total += value;
                if (card == "A") aceCount++;
            }

            // Juster for esser (A = 1 i stedet for 11 hvis bust)
            while (total > 21 && aceCount > 0)
            {
                total -= 10;
                aceCount--;
            }

            return total;
        }

        static string[] AddCard(string[] hand, string card)
        {
            string[] newHand = new string[hand.Length + 1];
            for (int i = 0; i < hand.Length; i++)
            {
                newHand[i] = hand[i];
            }
            newHand[newHand.Length - 1] = card;
            return newHand;
        }
    }
    
    
}


