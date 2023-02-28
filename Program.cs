using System;
using Games;

namespace Card_Games
{
    class Program
    {
        static void Main(string[] args)
        {
            // Play a game of Blackjack. TODO: Make this a menu.
            BlackjackGame blackjackGame = new BlackjackGame(2);
            blackjackGame.Play();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
