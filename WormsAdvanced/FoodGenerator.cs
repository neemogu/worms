using System;
using System.Collections.Generic;
using WormsBasic;

namespace WormsAdvanced {
    public class FoodGenerator {
        private readonly Random _random = new();
        private readonly int _mu;
        private readonly int _sigma;

        public FoodGenerator(int mu, int sigma) {
            _mu = mu;
            _sigma = sigma;
        }
        
        public void SpawnFood(IDictionary<Point, Food> food) {
            Point newFoodCoord;
            do {
                var x = _random.NextNormal( _mu, _sigma);
                var y = _random.NextNormal( _mu, _sigma);
                newFoodCoord = new Point {X = x, Y = y};
            } while (food.ContainsKey(newFoodCoord));
            food.Add(newFoodCoord, new Food());
        }
    }
}