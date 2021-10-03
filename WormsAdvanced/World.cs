using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using WormsBasic;

namespace WormsAdvanced {
    public class World: IFoodContainer {
        private const string LogFileName = "WorldHistory.txt";
        
        private readonly int _turnsNumber;
        private readonly Random _random;
        public World(int turnsNumber) {
            _turnsNumber = turnsNumber;
            _random = new Random();
        }
        
        private readonly List<AdvancedWorm> _worms = new();
        private readonly IDictionary<Point, Food> _food = new Dictionary<Point, Food>();

        public void AddWorm(AdvancedWorm worm) {
            _worms.Add(worm);
        }

        public void StartLife() {
            File.Delete(LogFileName);
            for (var i = 0; i < _turnsNumber; ++i) {
                ClearRottenFood();
                SpawnFood();
                PrintWorms(i + 1);
                ProcessWorms();
            }
        }

        private void ClearRottenFood() {
            var rottenFoodLocations = new List<Point>();
            foreach (var (location, food) in _food) {
                food.Stale();
                if (food.IsRotten()) {
                    rottenFoodLocations.Add(location);
                }
            }
            foreach (var location in rottenFoodLocations) {
                _food.Remove(location);
            }
        }

        private void SpawnFood() {
            Point newFoodCoord;
            do {
                var x = _random.NextNormal( 0, 5);
                var y = _random.NextNormal( 0, 5);
                newFoodCoord = new Point {X = x, Y = y};
            } while (_food.ContainsKey(newFoodCoord));
            _food.Add(newFoodCoord, new Food());
        }

        private void CheckForFoodAndEat(AdvancedWorm worm) {
            if (_food.Remove(worm.Location)) {
                worm.Eat();
            }
        }
        
        private void CheckForFoodAndEat(AdvancedWorm worm, Point coordToCheck) {
            if (_food.Remove(coordToCheck)) {
                worm.Eat();
            }
        }
        
        private void ProcessWorms() {
            for (var i = _worms.Count - 1; i >= 0; --i) {
                var worm = _worms[i];
                if (worm.Health <= 0) {
                    _worms.RemoveAt(i);
                    continue;
                }
                CheckForFoodAndEat(worm);
                var nextCoord = worm.NextCoord();
                // if there are no worms in the next coord
                if (_worms.All(worm1 => worm1 == worm || !nextCoord.Equals(worm1.Location))) {
                    // is there is a food in the next coord
                    switch (_food.ContainsKey(nextCoord)) {
                        case false: {
                            var newWorm = worm.Action();
                            if (newWorm != null) {
                                AddWorm(newWorm);
                            }
                            break;
                        }
                        case true when worm.NextAction == WormAction.Move:
                            CheckForFoodAndEat(worm, worm.NextCoord());
                            break;
                    }
                }
                worm.Live();
            }
        }
        
        private void PrintWorms(int turn) {
            var text = $"Turn {turn}:\r\nWorms:\r\n{Worm.WormsArrayToString(_worms)}\r\nFood:\r\n{FoodToString()}\r\n";
            Console.Out.Write(text);
            File.AppendAllText(LogFileName, text);
        }

        private string FoodToString() {
            var builder = new StringBuilder();
            builder.Append("[\r\n");
            foreach (var (location, food) in _food) {
                builder.Append("\t" + location + ",\r\n");
            }
            builder.Remove(builder.Length - 3, 1);
            builder.Append(']');
            return builder.ToString();
        }

        public Point getNearestFood(Point fromCoord) {
            var nearest = new Point { X = int.MaxValue, Y = int.MaxValue};  
            var lowestDistance = int.MaxValue;
            foreach (var (foodLocation, food) in _food) {
                var distance = fromCoord.DistanceTo(foodLocation);
                if (distance >= lowestDistance) continue;
                lowestDistance = distance;
                nearest = foodLocation;
            }
            return nearest;
        }
    }
}