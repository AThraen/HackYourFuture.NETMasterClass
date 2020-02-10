using System;
using ThirtyOne.Helpers;
using ThirtyOne.Models;

namespace ThirtyOne
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //Initial test
            Deck d = new Deck();
            d.Initialize();
            Random r = new Random();
            d.Shuffle(r);
            Card c = d.DrawCard();

            //Game implementation
            Console.WriteLine("Let's play 31!");
            ComputerPlayer cp = new ComputerPlayer("Computer");
            Game G = new Game(r, cp, new ConsolePlayer("You"));
            bool isGameOver = false;
            while (!isGameOver)
            {
                Console.WriteLine($"{G.CurrentPlayer.Name} turn!");
                isGameOver = G.NextTurn();
            }
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine($"--- GAME OVER, {G.Winner.Name} WON WITH {G.Winner.Hand.ToListString()} ---");
            Console.ReadLine();

        }
    }
}
