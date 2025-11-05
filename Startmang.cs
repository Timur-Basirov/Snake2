using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UusimangTARpv24;

namespace Snake2
{
    public class Startmang
    {
        static ComplexWalls? cw;
        public static void StartGame()
        {
            Console.Clear();
            Console.Write("Sisesta sinu nimi:");
            string playerName = Console.ReadLine() ?? "Player";

            Console.Clear();
            Console.WriteLine("Vali madu värv:");
            Console.WriteLine("1. Valge");
            Console.WriteLine("2. Kollane");
            Console.WriteLine("3. Roheline");
            Console.WriteLine("4. Punane");
            Console.Write("Sisesta värvi number:");
            string colorChoice = Console.ReadLine() ?? "1";
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
            string mapChoice = Console.ReadLine() ?? "1";
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
            string foodColorChoice = Console.ReadLine() ?? "1";
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

            DataManager.records.Add(new PlayerRecord { Name = playerName, Score = score });
            DataManager.SaveRecords();
        }       
    }
}
