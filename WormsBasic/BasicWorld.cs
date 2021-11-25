using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WormsBasic {
    public class BasicWorld: IWorld {
        private readonly int _turnsNumber;
        private const string LogFileName = "WorldHistory.txt";
        private IWormStrategy _wormStrategy;
        
        public BasicWorld(int turnsNumber, IWormStrategy wormStrategy) {
            ClearLogFile();
            _turnsNumber = turnsNumber;
            _wormStrategy = wormStrategy;
        }

        private readonly List<Worm> _worms = new();

        public void AddWorm(Worm worm) => _worms.Add(worm);

        public void StartLife() {
            for (var i = 0; i < _turnsNumber; ++i) {
                PrintWorms(i + 1);
                ProcessWorms();
            }
        }

        private void ProcessWorms() {
            foreach (var worm in _worms) {
                switch (_wormStrategy.NextAction(worm)) {
                    case WormAction.Move:
                        var nextDirection = _wormStrategy.NextDirection(worm);
                        var nextLocation = worm.GetNextLocation(nextDirection);
                        if (_worms.All(worm1 => worm1 == worm || !nextLocation.Equals(worm1.Location))) {
                            worm.Move(nextDirection);
                        }
                        break;
                }
            }
        }

        private void PrintWorms(int turn) {
            var text = $"Turn {turn}:\r\n{Worm.WormsArrayToString(_worms)}";
            Console.Out.WriteLine(text);
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