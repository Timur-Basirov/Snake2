using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UusimangTARpv24;

namespace Snake2
{
    public static class DataManager
    {
        public static List<PlayerRecord> records = new List<PlayerRecord>();
        static string recordsFilePath = "records.txt";
        public static void ShowRecords()
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


       public static void LoadRecords()
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

        public static void SaveRecords()
        {
            var lines = records.Select(r => $"{r.Name},{r.Score}").ToList();
            File.WriteAllLines(recordsFilePath, lines);
        }
    }
}
