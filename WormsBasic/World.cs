using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace WormsBasic {
    public class World {
        private readonly List<Worm> _worms = new();

        public void AddWorm(Worm worm) {
            _worms.Add(worm);
        }

        public void StartLife() {
            while (true) {
                PrintWorms();
                foreach (var worm in _worms) {
                    var nextX = worm.NextX();
                    var nextY = worm.NextY();
                    if (_worms.All(worm1 => worm1 == worm ||
                                            nextX != worm1.X ||
                                            nextY != worm1.Y)) {
                        worm.Action();
                    }
                }
                Thread.Sleep(1000);
            }
        }

        private void PrintWorms() {
            Console.Out.WriteLine($"Worms: {Worm.WormsArrayToString(_worms)}");
        }
    }
}