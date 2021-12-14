using System;
using WormsAdvanced;
using WormsBasic;

namespace WormsGeneratedWorld {
    class Program {
        static void Main(string[] args) {
            if (args.Length < 1) {
                Console.Error.WriteLine("Usage: [world-name]");
                return;
            }
            var foodContainer = new FoodContainer(new PersistedFoodGenerator(args[0]));
            IWormStrategy strategy = new WormMultiplyingStrategy(foodContainer);
            var world = new AdvancedWorld(strategy, foodContainer);

            Worm john = new AdvancedWorm("John", new Point {X = 0, Y = 0});
            world.AddWorm(john);
            
            world.StartLife();
        }
    }
}