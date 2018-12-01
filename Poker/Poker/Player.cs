using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Player 
    {
        public List<Card> Hand { get; set; }
        public int Chips { get; set; }
        public bool Active { get; set; }
        public int CurrentBet { get; set; }
        private String Name { get; }
        private static int compNumber = 1;

        //Compartmentalized test constructor
        public Player(Deck d, int chips, string name)
        {
            Hand = new List<Card>(5);
            for (int i = 0; i < 5; i++)
            {
                Hand.Add(d.DrawCard());
            }

            CurrentBet = 0;
            Active = true;
            Chips = chips;
            Name = name;
        }

        //Release constructor
        public Player(int chips, bool human)
        {
            Hand = new List<Card>(5);
            Chips = chips;
            CurrentBet = 0;
            if (!human) {
                Name = $"ComPlayer{compNumber}";
                compNumber++;
            }
            else
            {
                Console.WriteLine("Welcome to poker!\nPlease enter your name:");
                Name = Console.ReadLine();
            }
            Active = true;
        }

        public void SwapCards(Deck d)
        {
            Console.WriteLine("Which cards would you like to replace? \nJust type in the numbers!");
            String cardsToReplace = Console.ReadLine();
            int[] cardNumbers = new int[cardsToReplace.Length];

            for (int i = 0; i < cardsToReplace.Length; i++) //Parse user input to array locations
            {
                cardNumbers[i] = int.Parse(cardsToReplace[i].ToString()) - 1;
            }

            for (int i = 0; i < cardNumbers.Length; i++) //Remove cards specified and replace them with fresh draws
            {
                Hand.RemoveAt(cardNumbers[i]);
                Hand.Insert(cardNumbers[i], d.DrawCard());
                Console.WriteLine(cardNumbers[i]);
            }
        }

        public override string ToString()
        {
            return $"{Name} : {Chips}";
        }
    }
}
