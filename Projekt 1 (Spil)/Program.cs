using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_1__Spil_
{
    internal class Program
    {
        // Spillepladen er et array af 9 felter (0–8), som starter med tallene 1–9
        // Så kan man se hvilke felter man kan vælge
        static char[] board = { '1','2','3','4','5','6','7','8','9' };

        // Variabler til at holde styr på hvor mange brikker spiller og computer har
        static int playerPieces = 0;
        static int computerPieces = 0;

        // Programmet starter her
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Velkommen til X og O!");
            Console.ForegroundColor = ConsoleColor.Black;
            PlayGame();   // Kalder vores spilfunktion
        }
        // Kører selve spillet i en løkke indtil nogen vinder
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
               || board[pos-1] == 'X' || board[pos-1] == 'O')
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
        while (board[pos-1] == 'X' || board[pos-1] == 'O');

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
            if (board[wins[i,0]] == symbol 
             && board[wins[i,1]] == symbol 
             && board[wins[i,2]] == symbol)
                return true;  // Spilleren har vundet
        }
        return false;  // Ingen vinder endnu
    }
    }
}
