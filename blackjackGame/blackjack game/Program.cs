using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackjack_game
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Lindsay's Game. Please tell me your name");
            string playerName = Console.ReadLine();
            Console.WriteLine("How much money are you playing with today?");
            int bank = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Hello {0}. Would you like to start a game of Blackjack?", playerName);
            string answer = Console.ReadLine().ToLower();
            if (answer == "yes")
            {
                Player player = new Player(playerName, bank);
                Game game = new TwentyOneGame();
                game += player;
                player.isActivelyPlaying = true;
                while (player.isActivelyPlaying && player.Balance >0)
                {
                    game.Play();
                }
                game -= player;
                Console.WriteLine("Thank you for playing!");
            }
            Console.WriteLine("Feel free to take a look around the casino");
            Console.Read();
        }
    }
}
