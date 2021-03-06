using System;
using WormsAdvanced;
using WormsBasic;
using WormsDatabase;

namespace WormsGeneratedWorld {
    class Program {
        static void Main(string[] args) {
            if (args.Length < 1) {
                Console.Error.WriteLine("Usage: [world-name]");
                return;
            }

            using (var dbContext = new WormsDbContext()) {
                var foodContainer = new FoodContainer(new PersistedFoodGenerator(args[0], dbContext));
                
                IWormStrategy<AdvancedWorm> strategy = new WormMultiplyingStrategy(foodContainer);
                var world = new AdvancedWorld(strategy, foodContainer);

                Worm john = new AdvancedWorm("John", new Point { X = 0, Y = 0 });
                world.AddWorm(john);

                world.StartLife();
            }
        }
    }
}