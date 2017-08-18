using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;


namespace Sänka_skepp
{
    class Program
    {

        private static void StartGame()
        {
            //string head = Head();
            //Console.WriteLine(head);
            //public static string Head()
            //{

            // string a = "   SSSSS    SS    SS   SSS    SS  SS   SS      SSS         SSSSS   SS   SS   SSSSSSS  SSSSS   SSSSS ";
            // string b = "  SS          SSS      SS S   SS  SS  SS      SS SS       SS       SS  SS    SS       SS  SS  SS  SS ";
            // string c = "    SSS      SS  SS    SS  S  SS  SSSS       SS   SS        SSS    SSSS      SSSS     SSSSS   SSSSS  ";
            // string d = "       SS   SSSSSSSS   SS   S SS  SS  SS    SSSSSSSSS          SS  SS  SS    SS       SS      SS   ";
            // string e = "   SSSSS   SS      SS  SS    SSS  SS    SS SS       SS     SSSSS   SS    SS  SSSSSSS  SS      SS ";
            //    return a + b + c + d + e;
            //}

            char[,] gameBoard = new char[,] {{' ','1','2','3','4','5'}, //TODO - Hantera utanför spelområdet
                                             {'1',' ',' ',' ',' ',' '},
                                             {'2',' ',' ',' ',' ',' '},
                                             {'3',' ',' ',' ',' ',' '},
                                             {'4',' ',' ',' ',' ',' '},
                                             {'5',' ',' ',' ',' ',' '} };

            char[,] facitGame = new char[,] {{'2','2','2','2','2','2'}, // Facit-display
                                             {'2','0','0','0','0','0'},
                                             {'2','1','1','0','0','1'},
                                             {'2','0','0','0','0','1'},
                                             {'2','0','0','0','0','1'},
                                             {'2','1','1','1','1','0'} };

            int counter = 15;
            int hitsLeft = 9; 

            DisplayGameBoard(gameBoard, counter, hitsLeft);



            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {
                synth.SetOutputToDefaultAudioDevice();


                do
                {
                    int r = 0;
                    int c = 0;
                    try
                    {
                        Console.Write("Ange vilken position du vill beskjuta (skriv t.ex. för rad 1 kolumn 2 såhär: 12): ");
                        string input = Console.ReadLine();
                        char[] arrayAnswer = input.ToCharArray();
                        r = int.Parse(arrayAnswer[0].ToString());
                        c = int.Parse(arrayAnswer[1].ToString());


                    counter--;



                    if (facitGame[r, c] == '1') //facitGame[arrayAnswer[0],arrayAnswer[1]]
                    {

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("BOOOOOOM!!! <--- Träff");
                        Console.WriteLine();
                        synth.Speak("BOOM!!! You hit the target");
                        Console.ResetColor();                      
                        gameBoard[r, c] = '☻';                        
                        hitsLeft--;
                        Console.Clear();
                        DisplayGameBoard(gameBoard, counter, hitsLeft);
                    }
                    else if ((facitGame[r, c] == '2'))
                    {
                        Console.WriteLine("Out of bounds");

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        Console.WriteLine("Hur kunde du missa, din klant...");
                        synth.Speak("Miss");
                        Console.WriteLine();
                        Console.ResetColor();
                        gameBoard[r, c] = 'x';
                        Console.Clear();
                        DisplayGameBoard(gameBoard, counter, hitsLeft);

                    }

                    }
                    catch (Exception)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Nu kastade du inte bomben i havet. Försök igen.");
                        synth.Speak("Invalid throw");
                        Console.WriteLine();
                    }


                } while (counter > 0 && hitsLeft > 0);

                if (hitsLeft == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("You won");
                    synth.Speak("Hurray!");
                    Console.WriteLine();
                }
                else 
                {
                    Console.WriteLine();
                    Console.WriteLine("Game Over, dina bomber är slut!");
                    synth.Speak("Looser");
                    Console.WriteLine();
                }
            }
        }



        private static void DisplayGameBoard(char[,] gameBoard, int counter, int hitsLeft)
        {


            for (int row = 0; row < gameBoard.GetLength(0); row++) 
            {
                Console.WriteLine("┌───────────────────────────────────────────────┐  ");
                Console.WriteLine("|       |       |       |       |       |       | ");
                Console.Write("|   ");
                for (int col = 0; col < gameBoard.GetLength(1); col++)
                {
                    Console.Write(gameBoard[row, col] + "   |   ");
                }

                Console.WriteLine("");
                Console.WriteLine("|       |       |       |       |       |       |");
            }
            Console.WriteLine("└───────────────────────────────────────────────┘");
            Console.WriteLine();
            Console.WriteLine($"☻ = Träff     x = Miss     Antal bomber kvar = {counter}    Antal träffar kvar för vinst = {hitsLeft}");
            Console.WriteLine();


        }



        static void Main(string[] args)
        {


            Console.OutputEncoding = System.Text.Encoding.Unicode; //Skriver ut små figurer
            Console.InputEncoding = System.Text.Encoding.Unicode;



            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   SSSSS    SS    SS   SSS    SS  SS   SS      SSS         SSSSS   SS   SS   SSSSSSS  SSSSS   SSSSS ");
            Console.WriteLine("  SS          SSS      SS S   SS  SS  SS      SS SS       SS       SS  SS    SS       SS  SS  SS  SS ");
            Console.WriteLine("    SSS      SS  SS    SS  S  SS  SSSS       SS   SS        SSS    SSSS      SSSS     SSSSS   SSSSS  ");
            Console.WriteLine("       SS   SSSSSSSS   SS   S SS  SS  SS    SSSSSSSSS          SS  SS  SS    SS       SS      SS   ");
            Console.WriteLine("   SSSSS   SS      SS  SS    SSS  SS    SS SS       SS     SSSSS   SS    SS  SSSSSSS  SS      SS ");
            Console.ResetColor();
            Console.WriteLine();

            StartGame();
        }


    }
}
