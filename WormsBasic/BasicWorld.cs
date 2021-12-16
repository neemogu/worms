using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WormsBasic {
    public class BasicWorld: IWorld {
        private readonly int _turnsNumber;
        private const string LogFileName = "WorldHistory.txt";
        private readonly IWormStrategy<Worm> _wormStrategy;
        
        public BasicWorld(int turnsNumber, IWormStrategy<Worm> wormStrategy) {
            ClearLogFile();
            _turnsNumber = turnsNumber;
            _wormStrategy = wormStrategy;
        }

        private readonly List<Worm> _worms = new();

        public void AddWorm(Worm worm) => _worms.Add(worm);

        public void StartLife() {
            for (var i = 0; i < _turnsNumber; ++i) {
                PrintWorms(i + 1);
                ProcessWorms(i + 1);
            }
        }

        private void ProcessWorms(int step) {
            foreach (var worm in _worms) {
                WormAction nextAction = _wormStrategy.NextAction(worm, _worms, step, 1);
                switch (nextAction.Action) {
                    case Action.Move:
                        var nextDirection = nextAction.Direction;
                        var nextLocation = Utility.GetNextLocation(nextDirection, worm.Location);
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