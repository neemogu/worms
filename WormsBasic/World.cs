using System;
using System.Collections.Generic;
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
                PrintWorms();
                foreach (var worm in _worms) {
                    var nextCoord = worm.NextCoord();
                    if (_worms.All(worm1 => worm1 == worm || !nextCoord.Equals(worm1.Location))) {
                        worm.Action();
                    }
                }
            }
        }

        private void PrintWorms() {
            Console.Out.WriteLine($"Worms: {Worm.WormsArrayToString(_worms)}");
        }
    }
}