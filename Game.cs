using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UusimangTARpv24
{
    class Game
    {
        string playerName;
        ConsoleColor snakeColor;
        Walls walls;
        private ConsoleColor foodColor;

        public Game(string name, ConsoleColor snakeColor, Walls walls, ConsoleColor foodColor)
        {
            playerName = name;
            this.snakeColor = snakeColor;
            this.walls = walls;
            this.foodColor = foodColor;
        }

        public int Play()
        {
            //карта
            walls.Draw();

            //змейка
            Point p = new Point(25, 12, '*');
            Snake snake = new Snake(p, 3, Direction.RIGHT, snakeColor);
            snake.Draw();

            //еда
            FoodCreator foodCreator = new FoodCreator(80, 25, '$', this.walls);
            Point food = foodCreator.CreateFood();
            Console.ForegroundColor = foodColor;
            food.Draw();
            Console.ResetColor();


            while (true)
            {
                if (walls.IsHit(snake) || snake.IsHitTail())
                {

                    string gifPath = "Kaotus.gif";
                    try
                    {
                        Process.Start(new ProcessStartInfo(gifPath) { UseShellExecute = true });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                }

                if (snake.Eat(food))
                {
                    food = foodCreator.CreateFood();
                    Console.ForegroundColor = foodColor;
                    food.Draw();
                    Console.ResetColor();
                }
                else
                {
                    snake.Move();
                }


                Console.SetCursorPosition(30, 26);
                Console.Write($"Score: {snake.Score}");

                Thread.Sleep(100);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }
            }


            Console.Clear();
            Thread.Sleep(1000);
            Console.WriteLine($"Sa kaotasid");
            Thread.Sleep(1000);
            Console.WriteLine($"Teie tulemus oli: {snake.Score}");
            Thread.Sleep(1000);
            Console.WriteLine("Vajuta mis tahes nuppu, et tagasi minna.");
            Console.ReadKey();


            return snake.Score;
        }
    }
}
