using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace WormsBasic {
    public class World {
        private readonly int _turnsNumber;
        public World(int turnsNumber) {
            _turnsNumber = turnsNumber;
        }
        
        private readonly List<Worm> _worms = new();

        public void AddWorm(Worm worm) {
            _worms.Add(worm);
        }

        public void StartLife() {
            for (var i = 0; i < _turnsNumber; ++i) {
                PrintWorms(i + 1);
                ProcessWorms();
            }
        }

        private void ProcessWorms() {
            foreach (var worm in _worms) {
                var nextCoord = worm.NextCoord();
                if (_worms.All(worm1 => worm1 == worm || !nextCoord.Equals(worm1.Location))) {
                    worm.Action();
                }
            }
        }
        
        private void PrintWorms(int turn) {
            var text = $"Turn {turn}:\r\n{Worm.WormsArrayToString(_worms)}";
            Console.Out.WriteLine(text);
            File.WriteAllText("WorldHistory.txt", text);
        }
    }
}