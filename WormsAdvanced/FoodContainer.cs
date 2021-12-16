using System.Collections.Generic;
using System.Text;
using WormsBasic;

namespace WormsAdvanced {
    public class FoodContainer : IFoodLocationProvider, IFoodContainer {

        private readonly IFoodGenerator _foodGenerator;
        
        public FoodContainer(IFoodGenerator generator) {
            _foodGenerator = generator;
        }

        public IDictionary<Point, Food> Food { get; } = new Dictionary<Point, Food>();

        private void ClearRottenFood() {
            var rottenFoodLocations = new List<Point>();
            foreach (var (location, food) in Food) {
                food.Stale();
                if (food.IsRotten()) {
                    rottenFoodLocations.Add(location);
                }
            }
            foreach (var location in rottenFoodLocations) {
                Food.Remove(location);
            }
        }

        public bool HasFoodIn(Point point) {
            return Food.ContainsKey(point);
        }
        
        public string FoodToString() {
            var builder = new StringBuilder();
            builder.Append("[\r\n");
            foreach (var (location, food) in Food) {
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
        
        public void CheckForFoodAndEat(AdvancedWorm worm) {
            CheckForFoodAndEat(worm, worm.Location);
        }
        
        public void CheckForFoodAndEat(AdvancedWorm worm, Point coordToCheck) {
            if (Food.Remove(coordToCheck)) {
                worm.Eat();
            }
        }
        
        private void SpawnFood() {
            _foodGenerator.SpawnFood(Food);
        }
        
        public Point GetNearestFood(Point fromCoord) {
            var nearest = new Point { X = int.MaxValue, Y = int.MaxValue};  
            var lowestDistance = int.MaxValue;
            foreach (var (foodLocation, food) in Food) {
                var distance = fromCoord.DistanceTo(foodLocation);
                if (distance >= lowestDistance) continue;
                lowestDistance = distance;
                nearest = foodLocation;
            }
            return nearest;
        }
        
        
    }
}