using WormsBasic;

namespace WormsAdvanced {
    internal static class WormsWorldProgram
    {
        private static void Main(string[] args) {
            var world = new World(100);
            var john = new AdvancedWorm("John", new Point {X = 0, Y = 0}, world);
            IWormStrategy johnStrategy = new WormMultiplyingStrategy(john, world);
            john.Strategy = johnStrategy;
            
            world.AddWorm(john);
            world.StartLife();
        }
    }
}