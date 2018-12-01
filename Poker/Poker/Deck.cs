using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Deck
    {
        List<Card> DiscardPile;
        Stack<Card> DrawPile;
        Random shuffler = new Random();
        public Deck()
        {
            DiscardPile = new List<Card>(52);
            DrawPile = new Stack<Card>(52);
            for (int i = 0; i < 4; i++)
            {

                ConsoleColor cardColor = ConsoleColor.Black;
                char suit = ' ';
                switch (i)
                {
                    case 0:
                        suit = '\u2660';
                        break;
                    case 1:
                        suit = '\u2663';
                        break;
                    case 2:
                        cardColor = ConsoleColor.Red;
                        suit = '\u2665';
                        break;
                    case 3:
                        cardColor = ConsoleColor.Red;
                        suit = '\u2666';
                        break;
                }
                for (int k = 2; k < 15; k++)
                {
                    DiscardPile.Add(new Card(cardColor, suit, k));
                }
            }
            ShuffleDeck(DiscardPile, DrawPile);
            //foreach (var item in DrawPile)
            //{
            //    Console.Write(item.ToString() + " ");
            //}
        }

        private void ShuffleDeck(IList<Card> dis, Stack<Card> draw)
        {


            int index = dis.Count;
            while (index > 0)
            {
                int shuffleIndex = shuffler.Next(0, index);
                Card Temp = dis[shuffleIndex];
                index--;
                dis[shuffleIndex] = dis[index];
                dis[index] = Temp;
            }

            foreach (Card c in dis)
            {
                draw.Push(c);
            }

            dis.Clear();
        }

        public Card DrawCard()
        {
            return DrawPile.Pop();
        }
    }

    struct Card
    {
        ConsoleColor color;
        public Char Suit { get; private set; }
        public int Value { get; private set; } 
        String display;

        public Card(ConsoleColor c, char suit, int value)
        {
            this.color = c;
            this.Suit = suit;
            this.Value = value;

            if (value < 11)
            {
                display = value == 10 ? $"{value.ToString()}{Suit} " : $" {value.ToString()}{Suit} ";
            }
            else if (value == 11)
            {
                display = $" J " + Suit;
            }
            else if (value == 12)
            {
                display = $" Q " + Suit;
            }
            else if (value == 13)
            {
                display = $" K " + Suit;
            }
            else
            {
                display = $" A " + Suit;
            }
        }

        public override string ToString()
        {
            
            Console.ForegroundColor = color;
            return display;
        }

    }

}

