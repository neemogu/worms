using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WormsAdvanced;
using WormsBasic;

namespace WormsDIContainers {
    public class LoggingService {
        private const string LogFileName = "WorldHistory.txt";

        public LoggingService() {
            File.Delete(LogFileName);
        }

        public void PrintWorldState(int turn, List<AdvancedWorm> worms, IDictionary<Point, Food> foodMap) {
            var text = $"Turn {turn}:\r\nWorms:\r\n{Worm.WormsArrayToString(worms)}\r\nFood:\r\n{FoodToString(foodMap)}\r\n";
            Console.Out.Write(text);
            File.AppendAllText(LogFileName, text);
        }

        private string FoodToString(IDictionary<Point, Food> foodMap) {
            var builder = new StringBuilder();
            builder.Append("[\r\n");
            foreach (var (location, food) in foodMap) {
                builder.Append("\t" + location + ",\r\n");
            }
            builder.Remove(builder.Length - 3, 1);
            builder.Append(']');
            return builder.ToString();
        }
    }
}