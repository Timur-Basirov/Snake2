using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace Snake
{
    class Program
    {
        static List<PlayerRecord> records = new List<PlayerRecord>();
        static string recordsFilePath = "records.txt";
        static ComplexWalls? cw;

        static void Main(string[] args)
        {
            //загружаем рекорд
            LoadRecords();

            while (true)
            {
                Console.Clear();
                Console.WriteLine(" Madu mäng");
                Console.WriteLine("1. Play");
                Console.WriteLine("2. LeaderBoard");
                Console.WriteLine("3. Exit");
                Console.Write("Choose option: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    StartGame();
                }
                else if (choice == "2")
                {
                    ShowRecords();
                }
                else if (choice == "3")
                {
                    break;
                }
            }
        }

        static void StartGame()
        {
            Console.Clear();
            Console.Write("Insert your name: ");
            string playerName = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Choose snake color:");
            Console.WriteLine("1. White");
            Console.WriteLine("2. Yellow");
            Console.WriteLine("3. Green");
            Console.WriteLine("4. Red");
            Console.Write("Insert color number: ");
            string colorChoice = Console.ReadLine();
            Console.Clear();

            ConsoleColor snakeColor = ConsoleColor.White;
            switch (colorChoice)
            {
                case "1":
                    snakeColor = ConsoleColor.White;
                    break;
                case "2":
                    snakeColor = ConsoleColor.Yellow;
                    break;
                case "3":
                    snakeColor = ConsoleColor.Green;
                    break;
                case "4":
                    snakeColor = ConsoleColor.Red;
                    break;
                default:
                    Console.WriteLine("Wrong choise, default color will be (white).");
                    Thread.Sleep(3000);
                    break;
            }

            Console.Clear();
            Console.WriteLine("Choose map to play:");
            Console.WriteLine("1. Default");
            Console.WriteLine("2. Hard");
            Console.Write("Insert map number: ");
            string mapChoice = Console.ReadLine();


            //выбор карты
            Walls walls;
            if (mapChoice == "1")
            {
                //обычная
                walls = new Walls(80, 25);
            }
            else if (mapChoice == "2")
            {
                //сложная
                walls = new ComplexWalls(80, 25);
                cw = (ComplexWalls?)walls;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Wrong choise, default map will be (default).");
                Thread.Sleep(3000);
                walls = new Walls(80, 25); 
            }
            Console.Clear();

            //параметры игрока
            Game playerGame = new Game(playerName, snakeColor, walls);
            int score = playerGame.Play();

            //добавляем данные в records.txt
            records.Add(new PlayerRecord { Name = playerName, Score = score });

            SaveRecords();
        }

        static void ShowRecords()
        {
            Console.Clear();
            Console.WriteLine("Leaderboard");
            if (records.Count == 0)
            {
                Console.WriteLine("Theres no Leader stats.");
            }
            else
            {
                var sortedRecords = records.OrderByDescending(r => r.Score).Take(10).ToList();
                foreach (var record in sortedRecords)
                {
                    Console.WriteLine($"{record.Name}: {record.Score}");
                }
            }
            Console.WriteLine("Press any button to return.");
            Console.ReadKey();
        }

        static void LoadRecords()
        {
            if (File.Exists(recordsFilePath))
            {
                var lines = File.ReadAllLines(recordsFilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        records.Add(new PlayerRecord { Name = parts[0], Score = int.Parse(parts[1]) });
                    }
                }
            }
        }

        static void SaveRecords()
        {
            var lines = records.Select(r => $"{r.Name},{r.Score}").ToList();
            File.WriteAllLines(recordsFilePath, lines);
        }
    }

    class PlayerRecord
    {
        public string Name { get; set; }
        public int Score { get; set; }
    }

    class Game
    {
        string playerName;
        ConsoleColor snakeColor;
        Walls walls;

        public Game(string name, ConsoleColor color, Walls wall)
        {
            playerName = name;
            snakeColor = color;
            walls = wall;
        }

        public int Play()
        {
            //карта
            walls.Draw();

            //змейка
            Point p = new Point(25, 12, '*');
            Snake snake = new Snake(p, 2, Direction.RIGHT, snakeColor);
            snake.Draw();

            //еда
            FoodCreator foodCreator = new FoodCreator(80, 25, '$', this.walls);
            Point food = foodCreator.CreateFood();
            food.Draw();

            //игровой процесс
            while (true)
            {
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    break;
                }

                if (snake.Eat(food))
                {
                    food = foodCreator.CreateFood();
                    food.Draw();
                }
                else
                {
                    snake.Move();
                }
                
                //Счёт
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
            Console.WriteLine($"You lost");
            Thread.Sleep(1000);
            Console.WriteLine($"Your score was: {snake.Score}");
            Thread.Sleep(1000);
            Console.WriteLine("Press any button, to return.");
            Console.ReadKey();

            return snake.Score;
        }

    }
}
