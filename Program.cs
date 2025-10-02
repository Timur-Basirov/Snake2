using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Unicode;
using System.Threading;

namespace Snake
{
    class Program
    {
        static List<PlayerRecord> records = new List<PlayerRecord>();
        static string recordsFilePath = "records.txt";
        static ComplexWalls? cw;
        

        static void Main(string[] args)
        {
            
            LoadRecords();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("  __  __           _         __  __ _   _             \r\n |  \\/  |         | |       |  \\/  (_) (_)            \r\n | \\  / | __ _  __| |_   _  | \\  / | __ _ _ __   __ _ \r\n | |\\/| |/ _` |/ _` | | | | | |\\/| |/ _` | '_ \\ / _` |\r\n | |  | | (_| | (_| | |_| | | |  | | (_| | | | | (_| |\r\n |_|  |_|\\__,_|\\__,_|\\__,_| |_|  |_|\\__,_|_| |_|\\__, |\r\n                                                 __/ |\r\n                                                |___/ ");
                Console.WriteLine("1. Mängi");
                Console.WriteLine("2. Liidrite tabel");
                Console.WriteLine("3. Välja");
                Console.Write("Valige valik:");
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
            Console.Write("Sisesta sinu nimi:");
            string playerName = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Vali madu värv:");
            Console.WriteLine("1. Valge");
            Console.WriteLine("2. Kollane");
            Console.WriteLine("3. Roheline");
            Console.WriteLine("4. Punane");
            Console.Write("Sisesta värvi number:");
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
                    Console.WriteLine("Vale valik, vaikimisi värv on (valge).");
                    Thread.Sleep(3000);
                    break;
            }

            Console.Clear();
            Console.WriteLine("Vali kaart, mida mängida:");
            Console.WriteLine("1. Lihtne");
            Console.WriteLine("2. Raske");
            Console.Write("Sisesta kaardi number:");
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
                Console.WriteLine("Vale valik, vaikimisi kaart on (Lihtne).");
                Thread.Sleep(3000);
                walls = new Walls(80, 25); 
            }
            Console.Clear();
            Console.WriteLine("Vali toidu värv:");
            Console.WriteLine("1. Valge");
            Console.WriteLine("2. Kollane");
            Console.WriteLine("3. Roheline");
            Console.WriteLine("4. Punane");
            Console.Write("Sisesta värvi number:");
            string foodColorChoice = Console.ReadLine();
            Console.Clear();
            ConsoleColor foodColor = ConsoleColor.White; 
            switch (foodColorChoice)
            {
                case "1":
                    foodColor = ConsoleColor.White;
                    break;
                case "2":
                    foodColor = ConsoleColor.Yellow;
                    break;
                case "3":
                    foodColor = ConsoleColor.Green;
                    break;
                case "4":
                    foodColor = ConsoleColor.Red;
                    break;
                default:
                    Console.WriteLine("Vale valik, vaikimisi värv on (valge).");
                    Thread.Sleep(2000);
                    break;
            }

            //параметры игрока
            Game playerGame = new Game(playerName, snakeColor, walls, foodColor);
            int score = playerGame.Play();

            //добавляем данные в records.txt
            records.Add(new PlayerRecord { Name = playerName, Score = score });

            SaveRecords();
        }

        static void ShowRecords()
        {
            Console.Clear();
            Console.WriteLine("Edetabel");
            if (records.Count == 0)
            {
                Console.WriteLine("Ei ole olemas liidri statistikat.");
            }
            else
            {
                var sortedRecords = records.OrderByDescending(r => r.Score).Take(10).ToList();
                foreach (var record in sortedRecords)
                {
                    Console.WriteLine($"{record.Name}: {record.Score}");
                }
            }
            Console.WriteLine("Vajuta mis tahes nuppu, et tagasi minna.");
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
                        Console.WriteLine("Не удалось открыть GIF: " + ex.Message);
                    }

                    try
                    {
                        SoundPlayer player = new SoundPlayer("Kaotus.wav"); 
                        player.PlaySync(); 
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка при воспроизведении звука: " + ex.Message);
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
