using System;
using WormsAdvanced;
using WormsBasic;

namespace WormsStrategyClient {
    class WormsStrategyClient {
        private const string baseUrl = "http://localhost";
        
        static void Main(string[] args) {
            var foodContainer = new FoodContainer(new RandomFoodGenerator(0, 5));
            IWormStrategy<AdvancedWorm> strategy = new WormsHttpStrategy(foodContainer, baseUrl);
            var world = new AdvancedWorld(strategy, foodContainer);

            Worm john = new AdvancedWorm("John", new Point {X = 0, Y = 0});
            world.AddWorm(john);
            
            world.StartLife();
        }
    }
}