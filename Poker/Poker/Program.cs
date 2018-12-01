using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.OutputEncoding = Encoding.Unicode;
            //Deck d = new Deck();
            //Player test = new Player(d, 500, "Test");
            //Table.DrawTable();

            //Console.WriteLine(test);
            //Table.PrintCardsHori(test.Hand);
            //test.SwapCards(d);
            //Console.WriteLine();
            //Console.WriteLine(test);
            //Table.PrintCardsHori(test.Hand);

            Game g = new Game();
            //int test = g.ScoreHand(new List<Card> { new Card(ConsoleColor.Red, '\u2660', 11), new Card(ConsoleColor.Red, '\u2663', 11),
            //    new Card(ConsoleColor.Red, '\u2665', 11), new Card(ConsoleColor.Red, '\u2666', 13), new Card(ConsoleColor.Red, '\u2660', 13) });
            //Console.WriteLine(test);
        }
    }
}
