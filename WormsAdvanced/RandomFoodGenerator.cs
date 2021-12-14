using System;
using System.Collections.Generic;
using WormsBasic;

namespace WormsAdvanced {
    public class RandomFoodGenerator : IFoodGenerator {
        private readonly Random _random = new();
        private const int MaxTries = 10000;
        private readonly int _mu;
        private readonly int _sigma;

        public RandomFoodGenerator(int mu, int sigma) {
            _mu = mu;
            _sigma = sigma;
        }
        
        public void SpawnFood(IDictionary<Point, Food> food) {
            Point newFoodCoord;
            int i = 0;
            do {
                var x = _random.NextNormal( _mu, _sigma);
                var y = _random.NextNormal( _mu, _sigma);
                newFoodCoord = new Point {X = x, Y = y};
                ++i;
            } while (food.ContainsKey(newFoodCoord) && i < MaxTries);
            food.Add(newFoodCoord, new Food());
        }
    }
}