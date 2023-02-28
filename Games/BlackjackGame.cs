using System;
using Cards;

namespace Games
{
    class BlackjackGame : Game
    {
        // TODO: add a dealer

        public BlackjackGame(int playerCount)
        {
            for (int i = 0; i < playerCount; i++)
            {
                Console.WriteLine("Enter the name of player " + (i + 1) + ":");
                string name = Console.ReadLine();
                players.Add(new BlackjackPlayer(name));
            }
        }

        public override void Play()
        {
            deck.Shuffle();

            // Deal two cards to each player.
            foreach (BlackjackPlayer player in players)
            {
                player.Hand.Add(deck.Deal());
                player.Hand.Add(deck.Deal());
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
                        if (player.Score == -1)
                        {
                            Console.WriteLine($"{player.Name} is bust!");
                        }
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

                    // Output the player's hand.
                    Console.WriteLine(player);
                }
            } while (!AnyIsBust() && !AllStick());

            Console.WriteLine("Game over!");

            // Ouput the winner.
            BlackjackPlayer winner;

            if (players[0].Score == -1)
            {
                winner = (BlackjackPlayer)players[1];
            }
            else if (players[1].Score == -1)
            {
                winner = (BlackjackPlayer)players[0];
            }
            else
            {
                winner = (BlackjackPlayer)players[0];
                foreach (BlackjackPlayer player in players)
                {
                    if (player.Score > winner.Score)
                    {
                        winner = player;
                    }
                }
            }
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
