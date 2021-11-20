using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WormsBasic;

namespace WormsAdvanced {
    public class AdvancedWorld: IFoodContainer, IWorld {
        private const string LogFileName = "WorldHistory.txt";
        
        private readonly int _turnsNumber;
        private readonly Random _random;
        private IWormStrategy _strategy;
        
        public AdvancedWorld(int turnsNumber, IWormStrategy strategy) {
            ClearLogFile();
            _turnsNumber = turnsNumber;
            _random = new Random();
            _strategy = strategy;
        }
        
        public AdvancedWorld(int turnsNumber) {
            ClearLogFile();
            _turnsNumber = turnsNumber;
            _random = new Random();
            _strategy = new IdleStrategy();
        }
        
        private readonly List<AdvancedWorm> _worms = new();
        private readonly IDictionary<Point, Food> _food = new Dictionary<Point, Food>();

        public void AddWorm(Worm worm) {
            if (worm is AdvancedWorm advancedWorm) {
                _worms.Add(advancedWorm);
            }
        }

        public void StartLife() {
            for (var i = 0; i < _turnsNumber; ++i) {
                ClearRottenFood();
                SpawnFood();
                PrintWorms(i + 1);
                ProcessWorms();
            }
        }

        public void SetStrategy(IWormStrategy strategy) => _strategy = strategy;

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
                
                var nextDirection = _strategy.NextDirection(worm);
                var nextCoord = worm.GetNextLocation(nextDirection);
                // if there are no worms in the next coord
                if (_worms.All(worm1 => worm1 == worm || !nextCoord.Equals(worm1.Location))) {
                    // is there is a food in the next coord
                    switch (_food.ContainsKey(nextCoord)) {
                        case false: {
                            switch (_strategy.NextAction(worm)) {
                                case WormAction.Move:
                                    worm.Move(nextDirection);
                                    break;
                                case WormAction.Multiply:
                                    var newWorm = worm.TryMultiply(nextCoord);
                                    if (newWorm != null) {
                                        AddWorm(newWorm);
                                    }
                                    break;
                            }
                            break;
                        }
                        case true when _strategy.NextAction(worm) == WormAction.Move:
                            CheckForFoodAndEat(worm, nextCoord);
                            break;
                    }
                }
                worm.Live();
            }
        }
        
        private void PrintWorms(int turn) {
            var text = $"Turn {turn}:\r\nWorms:\r\n{Worm.WormsArrayToString(_worms)}\r\nFood:\r\n{FoodToString()}\r\n";
            Console.Out.Write(text);
            try {
                File.AppendAllText(LogFileName, text);
            } catch (Exception e) {
                Console.Error.WriteLine("Can't log turn " + turn + " to a file " + LogFileName + ": " + e.Message);
            }
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

        public Point GetNearestFood(Point fromCoord) {
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
        
        private void ClearLogFile() {
            try {
                File.Delete(LogFileName);
            } catch (Exception e) {
                Console.Error.WriteLine("Can't clear log file " + LogFileName);
            }
        }
    }
}