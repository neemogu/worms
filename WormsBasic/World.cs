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
                if (!Thread.CurrentThread.IsAlive) {
                    break;
                }
                PrintWorms();
                foreach (var worm in _worms) {
                    var nextCoord = worm.NextCoord();
                    if (_worms.All(worm1 => worm1 == worm || !nextCoord.Equals(worm1.Location))) {
                        worm.Action();
                    }
                }
                try {
                    Thread.Sleep(1000);
                } catch (ThreadInterruptedException e) {
                    Console.Out.WriteLine(e.Message);
                    Thread.CurrentThread.Interrupt();
                }
            }
        }

        private void PrintWorms() {
            Console.Out.WriteLine($"Worms: {Worm.WormsArrayToString(_worms)}");
        }
    }
}