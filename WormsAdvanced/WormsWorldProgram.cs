using WormsBasic;

namespace WormsAdvanced {
    internal static class WormsWorldProgram
    {
        private static void Main(string[] args) {
            
            var world = new AdvancedWorld(100);
            
            IWormStrategy strategy = new WormMultiplyingStrategy(world);
            world.SetStrategy(strategy);
            
            Worm john = new AdvancedWorm("John", new Point {X = 0, Y = 0});
            world.AddWorm(john);
            
            world.StartLife();
        }
    }
}