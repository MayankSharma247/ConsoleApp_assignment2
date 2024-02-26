using System;
using System.Collections.Generic;

namespace GemHunters
{
    class Program
    {
        public static string[,] squareboard = new string[6, 6];
        public static int player1_A = 0; // For Players Row Movement
        public static int player1_B = 0; // For Players Col Movement
        public static int player2_A = 5;
        public static int player2_B = 5;
        public static int Gems1 = 0;
        public static int Gems2 = 0;
        public static int totalturns = 0;

        //public class Place
        //{
        //    public int Xaxis;
        //    public int Yaxis;

        //    public Place(int xaxis, int yaxis)
        //    {
        //        Xaxis = xaxis;
        //        Yaxis = yaxis;
        //    }
        //}


        static void Main(string[] args)
        {
            InitializeGemPlayBoard();
            Obstacles();
            Gems();

            while (totalturns < 30)
            {
                DisplayBoard_Status();
                PlayerMovement(1);
                totalturns++;

                if (totalturns == 30)
                {
                    break;
                }


                DisplayBoard_Status();
                PlayerMovement(2);

                totalturns++;
            }

            Console.WriteLine("Game Finish");
            Console.WriteLine($"Player 1 Gems = {Gems1}");
            Console.WriteLine($"Player 2 Gems = {Gems2}");

            if (Gems1 > Gems2)
            {
                Console.WriteLine("Winner Player 1");
                Console.ReadLine();
            }
            else if (Gems2 > Gems1)
            {
                Console.WriteLine("Winner Player 2");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Game Tied");
                Console.ReadLine();
            }
        }

        static void InitializeGemPlayBoard()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    squareboard[i, j] = "-";
                }
            }
            squareboard[player1_A, player1_B] = "P1";
            squareboard[player2_A, player2_B] = "P2";
        }

        static void DisplayBoard_Status()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Console.Write(squareboard[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        static void Obstacles()
        {
            Random random = new Random();
            int Obstacles = random.Next(5, 10);
            int obstaclesPlacedonBoard = 0;

            while (obstaclesPlacedonBoard < Obstacles)
            {
                int r = random.Next(0, 6);
                int c = random.Next(0, 6);

                if (squareboard[r, c] == "-")
                {
                    squareboard[r, c] = "O";
                    obstaclesPlacedonBoard = obstaclesPlacedonBoard + 1;
                }
            }
        }

        static void Gems()
        {
            Random random = new Random();
            int Gems = random.Next(10, 15);
            int gemsPlacedonBoard = 0;

            while (gemsPlacedonBoard < Gems)
            {
                int r = random.Next(0, 6);
                int c = random.Next(0, 6);

                if (squareboard[r, c] == "-")
                {
                    squareboard[r, c] = "G";
                    gemsPlacedonBoard = gemsPlacedonBoard + 1;
                }
            }
        }

        static void PlayerMovement(int p)
        {
            Console.WriteLine($"Player turn =  {p}");

            int currentR = (p == 1) ? player1_A : player2_A;
            int currentC = (p == 1) ? player1_B : player2_B;

            Console.WriteLine("Enter turn (U/D/L/R):");
            char movement = Char.ToUpper(Console.ReadKey().KeyChar);

            int newR = currentR;
            int newC = currentC;

            if (movement == 'U')
            {
                newR = newR - 1;
            }
            else if (movement == 'D')
            {
                newR = newR + 1;
            }
            else if (movement == 'R')
            {
                newR = newR + 1;
            }
            else
            {
                Console.WriteLine("Invalid input");
                PlayerMovement(p);
                return;
            }

            if (newR < 0 || newR >= 6 || newC < 0 || newC >= 6 || squareboard[newR, newC] == "O")
            {
                Console.WriteLine("Invalid input");
                PlayerMovement(p);
                return;
            }

            if (squareboard[newR, newC] == "G")
            {
                if (p == 1)
                {
                    Gems1 = Gems1 + 1;
                }

                else
                {
                    Gems2 = Gems2 + 1;
                }


                squareboard[newR, newC] = "-";
            }
        }
    }