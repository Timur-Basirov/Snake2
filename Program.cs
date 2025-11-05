using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Snake2;

namespace UusimangTARpv24
{
    class Program
    {

        static void Main(string[] args)
        {
            DataManager.LoadRecords();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("  __  __           _         __  __ _   _             \r\n |  \\/  |         | |       |  \\/  (_) (_)            \r\n | \\  / | __ _  __| |_   _  | \\  / | __ _ _ __   __ _ \r\n | |\\/| |/ _` |/ _` | | | | | |\\/| |/ _` | '_ \\ / _` |\r\n | |  | | (_| | (_| | |_| | | |  | | (_| | | | | (_| |\r\n |_|  |_|\\__,_|\\__,_|\\__,_| |_|  |_|\\__,_|_| |_|\\__, |\r\n                                                 __/ |\r\n                                                |___/ ");
                Console.WriteLine("1. Mängi");
                Console.WriteLine("2. Liidrite tabel");
                Console.WriteLine("3. Välja");
                Console.Write("Valige valik:");
                string choice = Console.ReadLine() ?? "";

                if (choice == "1")
                {
                    Startmang.StartGame();
                }
                else if (choice == "2")
                {
                    DataManager.ShowRecords();
                }
                else if (choice == "3")
                {
                    break;
                }
            }
        }
    }
}
