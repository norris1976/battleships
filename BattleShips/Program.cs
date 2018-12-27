using BattleShips.Controller;
using BattleShips.Interfaces;
using BattleShips.Utils;
using BattleShips.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
    class Program
    {
        private static string _title = @" 
  ____        _   _   _         _____ _     _           
 |  _ \      | | | | | |       / ____| |   (_)          
 | |_) | __ _| |_| |_| | ___  | (___ | |__  _ _ __  ___ 
 |  _ < / _` | __| __| |/ _ \  \___ \| '_ \| | '_ \/ __|
 | |_) | (_| | |_| |_| |  __/  ____) | | | | | |_) \__ \
 |____/ \__,_|\__|\__|_|\___| |_____/|_| |_|_| .__/|___/
                                             | |        
                                             |_|        by Tim Norris  ";
        static void Main(string[] args)
        {
            // Declare the dependencies. Usually part of this would be done by DI.
            var factory = new BattleFactory();
            var game = new GameController(factory);
            var canvas = new BattleConsoleCanvas();

            // Loop while application is open.
            while (true)
            {
                // Draw the header etc
                DrawTitle();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine("Press any key to start a new game.");
                Console.ReadKey();
                string message = "Find and destroy the 3 ships - good luck!";
                bool gameOver = false;

                // Create the and resources.
                game.CreateGame();
                game.AddShip(5);
                game.AddShip(4);
                game.AddShip(4);

                while (!gameOver)
                {
                    Console.Clear();
                    DrawTitle();

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(message);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();

                    canvas.Draw(game);
                    Console.WriteLine();
                    Console.WriteLine("Enter coords (e.g. A3):");
                    var coord = Console.ReadLine();
                    var result = game.EnterCoords(coord);
                    message = result.Message;

                    if (result.GameOver)
                    {
                        gameOver = true;
                        Console.ReadKey();
                    }
                }
            }
        }

        private static void DrawTitle()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Clear();
            Console.WriteLine(_title);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }
    }
}
