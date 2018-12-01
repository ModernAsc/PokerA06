using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    static class Table
    {
        private static int[] pStart = { 5, 3 };
        private static int[] tStart = { 35, 3 };
        private static int[] info = { 5, 15 };
        public static void PrintPlayers(Player[] players)
        {
            Console.CursorTop = pStart[1];
            foreach (Player p in players)
            {
                Console.CursorLeft = pStart[0];
                Console.WriteLine(p.ToString());
                Console.CursorLeft = pStart[0];
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write("|");
                foreach (Card c in p.Hand)
                {
                    Console.Write(c);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("|");
                }
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n");
            }
        }

        public static void DrawTable()
        {
            Console.BufferHeight = 600;
            Console.BackgroundColor = ConsoleColor.Gray;
            String fill = new string(' ', Console.BufferWidth);
            for (int  i = 0;  i < Console.BufferHeight;  i++)
            {
                Console.WriteLine(fill);
            }

            Console.BackgroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(tStart[0], tStart[1]);
            String fillTable = new string(' ', 40);
            for (int i = 0; i < (Console.BufferHeight / 32); i++)
            {
                Console.SetCursorPosition(tStart[0], tStart[1] + i);
                Console.WriteLine(fillTable);
            }
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, 0);
        }

        public static void WriteInfo(string infoString)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(info[0], info[1]);
            Console.WriteLine(infoString);
        }

        private static void ClearInfo()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(info[0], info[1]);
            for (int i = 0; i < 4; i++)
            {
                Console.CursorTop = info[1] + i;
                Console.WriteLine(new string(' ', Console.BufferWidth - 1));
            }
            
        }
    }
}
