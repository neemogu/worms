using WormsBasic;

namespace WormsAdvanced {
    internal static class WormsWorldProgram
    {
        private static void Main(string[] args) {
            FoodContainer foodContainer = new FoodContainer(new RandomFoodGenerator(0, 5));
            IWormStrategy<AdvancedWorm> strategy = new WormMultiplyingStrategy(foodContainer);
            var world = new AdvancedWorld(strategy, foodContainer);

            Worm john = new AdvancedWorm("John", new Point {X = 0, Y = 0});
            world.AddWorm(john);
            
            world.StartLife();
        }
    }
}