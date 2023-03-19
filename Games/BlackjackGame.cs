using System;
using System.Collections.Generic;
using Cards;

namespace Games
{
    class BlackjackGame : Game
    {
        private BlackjackPlayer dealer;

        public BlackjackGame(int playerCount)
        {
            for (int i = 0; i < playerCount; i++)
            {
                Console.WriteLine("Enter the name of player " + (i + 1) + ":");
                string name = Console.ReadLine();
                players.Add(new BlackjackPlayer(name));
            }

            dealer = new BlackjackPlayer("Dealer");
        }

        public override void Play()
        {
            deck.Shuffle();

            // Deal two cards to the dealer.
            dealer.Hand.Add(deck.Deal());
            foreach (BlackjackPlayer player in players)
            {
                player.Hand.Add(deck.Deal());
            }

            dealer.Hand.Add(deck.Deal());
            foreach (BlackjackPlayer player in players)
            {
                player.Hand.Add(deck.Deal());
            }

            // Output the hands of the players and the first card of the dealer.
            List<Rank> checkRanks = new List<Rank>
            {
                Rank.Ace,
                Rank.Ten,
                Rank.Jack,
                Rank.Queen,
                Rank.King
            };
            // check face up card of dealer
            if (checkRanks.Contains(dealer.Hand[0].Rank) && dealer.Score == 21)
            {
                Console.WriteLine("The dealer has a blackjack! Game over.");
                return;
            }
            else
            {
                Console.WriteLine("The dealer has: " + dealer.Hand[0] + " and an unknown card.");
            }

            foreach (BlackjackPlayer player in players)
            {
                Console.WriteLine(player);
            }

            bool naturalBlackjack = false;

            // Check if any player has a blackjack.
            foreach (BlackjackPlayer player in players)
            {
                if (player.Score == 21)
                {
                    Console.WriteLine(player.Name + " has a blackjack!");
                    naturalBlackjack = true;
                }
            }

            // If there is a blackjack, remove the players who have blackjack from the list of players.
            if (naturalBlackjack)
            {
                for (int i = players.Count - 1; i >= 0; i--)
                {
                    if (players[i].Score == 21)
                    {
                        players.RemoveAt(i);
                    }
                }
            }

            // Let players take turns drawing cards. They can draw as many cards as they want until they are bust. The player with the highest score wins.
            do
            {
                foreach (BlackjackPlayer player in players)
                {
                    // Let the player draw a card or pass.
                    Console.WriteLine(
                        $"It is {player.Name}'s turn. They have {player.Score} points."
                    );
                    Console.WriteLine("Do you want to draw a card? (y/n)");

                    string input = Console.ReadLine();
                    if (input == "y")
                    {
                        player.Hand.Add(deck.Deal());
                    }
                    else if (input == "n")
                    {
                        player.Stick = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. You have been forced to stick.");
                        player.Stick = true;
                    }

                    // Five card rule
                    if (player.Hand.Count == 5)
                    {
                        player.Stick = true;
                    }

                    // Output the player's hand.
                    if (player.Score == -1)
                    {
                        Console.WriteLine($"{player.Name} is bust!");
                    }
                    else
                    {
                        Console.WriteLine(player);
                    }
                }

                if (!AnyIsBust())
                {
                    // Let the dealer draw cards until they have at least 17 points.
                    if (dealer.Score < 17)
                    {
                        dealer.Hand.Add(deck.Deal());
                    }
                    else
                    {
                        dealer.Stick = true;
                    }

                    // Output the dealer's hand.
                    if (dealer.Score == -1)
                    {
                        Console.WriteLine("The dealer is bust!");
                    }
                    else
                    {
                        Console.WriteLine(dealer);
                    }
                }
            } while (!AnyIsBust() && !AllStick());

            Console.WriteLine("Game over!");

            // Ouput the winner.
            BlackjackPlayer winner = null;

            winner = (BlackjackPlayer)players[0];
            foreach (BlackjackPlayer player in players)
            {
                if (player.Score > winner.Score)
                {
                    winner = player;
                }
            }

            if (dealer.Score > winner.Score)
            {
                winner = (BlackjackPlayer)dealer;
            }

            // five card rule
            foreach (BlackjackPlayer player in players)
            {
                if (player.Hand.Count == 5)
                {
                    winner = player;
                }
            }

            Console.WriteLine($"The winner is {winner.Name} with {winner.Score} points!");
        }

        private bool AnyIsBust()
        {
            foreach (BlackjackPlayer player in players)
            {
                if (player.Score == -1)
                {
                    return true;
                }
            }

            if (dealer.Score == -1)
            {
                return true;
            }

            return false;
        }

        private bool AllStick()
        {
            foreach (BlackjackPlayer player in players)
            {
                if (!player.Stick)
                {
                    return false;
                }
            }

            if (!dealer.Stick)
            {
                return false;
            }

            return true;
        }

        internal class BlackjackPlayer : Player
        {
            public bool Stick { get; set; }

            public BlackjackPlayer(string name) : base(name) { }

            public override int Score
            {
                get
                {
                    int score = 0;

                    foreach (Card card in Hand)
                    {
                        if (card.Rank == Rank.Ace)
                        {
                            if (score + 11 <= 21)
                            {
                                score += 11;
                            }
                            else
                            {
                                score += 1;
                            }
                        }
                        else if (
                            card.Rank == Rank.Jack
                            || card.Rank == Rank.Queen
                            || card.Rank == Rank.King
                        )
                        {
                            score += 10;
                        }
                        else
                        {
                            score += (int)card.Rank;
                        }
                    }

                    // If the player has a score of higher than 21, they are bust
                    if (score > 21)
                    {
                        score = -1;
                    }

                    return score;
                }
            }
        }
    }
}
