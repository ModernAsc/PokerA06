using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Poker
{
    /// <summary>
    /// <author>Todd T.</author>
    /// </summary>
    [Serializable]
    class Game
    {
        public Player[] Players { get; private set; }
        private Player highestBetter;
        public Deck gameDeck { get; }
        private int pot;

        /// <summary>
        /// Constructor
        /// Comment out the foreach if you need to print something to the screen for testing
        /// </summary>
        public Game()
        {
            gameDeck = new Deck();
            pot = 0;

            InitPlayers();
            DealHands();

            Table.DrawTable();
            //foreach (var player in Players)
            //{
            //    Console.WriteLine(player);
            //    SortHand(player.Hand);
            //    Console.WriteLine(ScoreHand(player.Hand));
            //    Console.WriteLine("------------");
            //}
            Table.PrintPlayers(Players);
            Table.WriteInfo("test");
            //Ante();
            //PlaceBet(Players[0]);
        }

        private int PlaceBet(Player p)
        {
            int playerBet = 0;
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(pot);
                Console.WriteLine(pot == (Players.Length * 25));
                Console.ReadLine();
                /*
                 * Calling or raising a previous bet 
                 */
                if (pot > (Players.Length * 25))
                {
                    if (p.CurrentBet < highestBetter.CurrentBet)
                    {
                        Console.WriteLine("Place your bet, press f to fold, or c to call!");
                        string playerIn = Console.ReadLine();
                        if (playerIn[0] == 'c')
                        {
                            playerBet = highestBetter.CurrentBet - p.CurrentBet;
                            Console.WriteLine($"You have called the highest bet!\n {playerBet} chips have gone to the pot!");
                            Thread.Sleep(300);
                            return playerBet;

                        }
                        else if (playerIn[0] == 'f')
                        {
                            Console.WriteLine($"You have folded!\n {p.CurrentBet} chips are lost to the pot!");
                            Thread.Sleep(300);
                            return playerBet;
                        }
                        else if (int.TryParse(playerIn, out playerBet))
                        {
                            if (playerBet >= highestBetter.CurrentBet)
                            {
                                Console.WriteLine($"You have placed the highest bet!\n {playerBet} chips have gone to the pot!");
                                Thread.Sleep(300);
                                return playerBet;
                            }
                            else if (playerBet == highestBetter.CurrentBet)
                            {
                                Console.WriteLine($"You have called the highest bet!\n {playerBet} chips have gone to the pot!");
                                Thread.Sleep(300);
                                return playerBet;
                            }
                        }
                    }
                }
                else if (pot == Players.Length * 25 || highestBetter == null)
                {
                    Console.WriteLine("Place your bet, press f to fold, or c to check!");
                    string playerIn = Console.ReadLine();
                    if (playerIn[0] == 'c')
                    {
                        playerBet = -1;
                        Console.WriteLine($"You have checked!\n {playerBet} of your chips are in the pot!");
                        Thread.Sleep(300);
                        return playerBet;

                    }
                    else if (playerIn[0] == 'f')
                    {
                        Console.WriteLine($"You have folded!\n {p.CurrentBet} chips are lost to the pot!");
                        Thread.Sleep(300);
                        return playerBet;
                    }
                    else if (int.TryParse(playerIn, out playerBet))
                    {
                        if (playerBet >= highestBetter.CurrentBet)
                        {
                            Console.WriteLine($"You have placed the opening bet!\n {playerBet} chips have gone to the pot!");
                            Thread.Sleep(300);
                            return playerBet;
                        }
                    }
                }
                Console.WriteLine("Please make a valid choice!");               
            }
            return 0;
        }
    
            
        

        /// <summary>
        /// Takes a given collection of cards and assigns a score based on the tradition ranking system.
        /// See interior comments for more info
        /// </summary>
        /// <param name="hand">Set of cards to be evaluated. Expected to be sorted in descending value of rank.</param>
        /// <returns>Decimal Value</returns>
        public double ScoreHand(List<Card> hand)
        {
            //Var used to add high card to hands upon return
            //The decimal value for the sake of clarity for testing and not interferring in low scoring hands
            double highCard = hand[0].Value * .01;

            //FLUSH
            if (hand.GroupBy(c => c.Suit).Count() == 1) //Checks if cards grouped by suit form a single group
            {
                //STRAIGHT FLUSH
                if (hand.GroupBy(c => c.Value).Count(group => group.Count() > 1) == 0 && hand[0].Value - hand[4].Value == 4) //See comment for straight
                {
                    //ROYAL FLUSH
                    if (hand[0].Value == 14) //After all other checks, checking for an Ace at the front is all that's needed for a royal flush check
                    {
                        return 1000;
                    }
                    return 900 + highCard;
                }
                return 600 + highCard; //This return is out of sequence, but having a flush with any kind of pairing hand is impossible
            }


            //IGrouping variable used to check for occurences of the same value for their respective hands, including once for a straight
            var dupCheck = hand.GroupBy(card => card.Value).OrderByDescending(group => group.Count()).ThenByDescending(group => group.ElementAt(0).Value);

            /*FOUR OF A KIND
             */
            if (dupCheck.ElementAt(0).Count() == 4)
            {               
                int fourOfAKindHigh = dupCheck.ElementAt(0).ElementAt(0).Value;
                return 800 + highCard;
            }

            //FULL HOUSE
            if(dupCheck.ElementAt(0).Count() == 3 && dupCheck.ElementAt(1).Count() == 2)
            {
                int FullHouseHigh = dupCheck.ElementAt(0).ElementAt(0).Value;
                return 700 + FullHouseHigh;
            }


            /*STRAIGHT
             *Checks for any value groups with a count higher than one 
             *then checks if the difference between the highest card and lowest card is 4 indicating a sequence.
             *This step requires cards to be ordered upon being passed in, for clarity rather than logic 
             */
            if (dupCheck.Count(group => group.Count() > 1) == 0 && hand[0].Value - hand[4].Value == 4)
            {
                return 500 + highCard;
            }

            //THREE OF A KIND
            if (dupCheck.ElementAt(0).Count() == 3)
            {
                int threeKindValue = dupCheck.ElementAt(0).ElementAt(0).Value 
                    + 400;
                return threeKindValue + highCard;
            }

            //TWO PAIR
            if (dupCheck.ElementAt(0).Count() == 2 && dupCheck.ElementAt(1).Count() == 2)
            {
                int twoPairValue = 
                    dupCheck.ElementAt(0).ElementAt(0).Value
                    + dupCheck.ElementAt(1).ElementAt(0).Value
                    + 100;
                return twoPairValue + highCard;
            }

            //ONE PAIR
            if (dupCheck.ElementAt(0).Count() == 2)
            {
                int pairValue = dupCheck.ElementAt(0).ElementAt(0).Value;
                return pairValue + highCard;
            }

            //HIGH CARD
            return highCard;
        }

        /// <summary>
        /// Sorts hand value in place in descending order
        /// </summary>
        /// <param name="hand">Unformatted list of cards</param>
        private void SortHand(List<Card> hand)
        {
            hand.Sort((x, y) => y.Value.CompareTo(x.Value));
        }

        /// <summary>
        /// Creates the human player and AI players
        /// For use specifically in constructor
        /// </summary>
        private void InitPlayers()
        {
            Players = new Player[4];

            Players[0] = new Player(500, true);
            highestBetter = Players[0];

            for (int i = 1; i < Players.Length; i++)
            {
                Players[i] = new Player(500, false);
            }
        }

        /// <summary>
        /// Fills the hands of all players with 5 cards taken from the deck
        /// </summary>
        private void DealHands()
        {
            foreach (Player p in Players)
            {
                for (int i = 0; i < 5; i++)
                    p.Hand.Add(gameDeck.DrawCard());
            }
        }

        private void Ante()
        {
            int ante = 25;
            foreach (var player in Players)
            {
                player.Chips -= ante;
                player.CurrentBet = ante;
                pot += ante;
            }
        }
    }
}
