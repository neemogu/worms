using System.Collections.Generic;
using System.Text;
using WormsBasic;

namespace WormsAdvanced {
    public class FoodContainer : IFoodLocationProvider, IFoodContainer {

        private readonly FoodGenerator _foodGenerator;
        
        public FoodContainer(FoodGenerator generator) {
            _foodGenerator = generator;
        }
        
        private readonly IDictionary<Point, Food> _food = new Dictionary<Point, Food>();

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

        public bool HasFoodIn(Point point) {
            return _food.ContainsKey(point);
        }
        
        public string FoodToString() {
            var builder = new StringBuilder();
            builder.Append("[\r\n");
            foreach (var (location, food) in _food) {
                builder.Append("\t" + location + ",\r\n");
            }
            builder.Remove(builder.Length - 3, 1);
            builder.Append(']');
            return builder.ToString();
        }

        public void NextTurn() {
            ClearRottenFood();
            SpawnFood();
        }
        
        public void CheckForFoodAndEat (AdvancedWorm worm) {
            if (_food.Remove(worm.Location)) {
                worm.Eat();
            }
        }
        
        public void CheckForFoodAndEat (AdvancedWorm worm, Point coordToCheck) {
            if (_food.Remove(coordToCheck)) {
                worm.Eat();
            }
        }
        
        private void SpawnFood() {
            _foodGenerator.SpawnFood(_food);
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
        
        
    }
}