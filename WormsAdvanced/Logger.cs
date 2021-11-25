using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WormsBasic;

namespace WormsAdvanced {
    public class Logger {
        private const string LogFileName = "WorldHistory.txt";

        public Logger() {
            ClearLogFile();
        }

        public void PrintWorldState<TWorm> (int turn, List<TWorm> worms, FoodContainer foodContainer) where TWorm: Worm {
            var text = $"Turn {turn}:\r\nWorms:\r\n{Worm.WormsArrayToString(worms)}\r\nFood:\r\n{foodContainer.FoodToString()}\r\n";
            Console.Out.Write(text);
            try {
                File.AppendAllText(LogFileName, text);
            } catch (Exception e) {
                Console.Error.WriteLine("Can't log turn " + turn + " to a file " + LogFileName + ": " + e.Message);
            }
        }

        private void ClearLogFile() {
            try {
                File.Delete(LogFileName);
            } catch (Exception e) {
                Console.Error.WriteLine("Can't clear log file " + LogFileName);
            }
        }
    }
}