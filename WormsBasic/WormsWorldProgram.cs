namespace WormsBasic
{
    internal static class WormsWorldProgram
    {
        private static void Main(string[] args) {
            Worm john = new BasicWorm("John", new Point {X = 0, Y = 0});
            IWormStrategy strategy = new WormCircleStrategy(5, new Point {X = 0, Y = 0});
            IWorld world = new BasicWorld(100, strategy);
            world.AddWorm(john);
            world.StartLife();
        }
    }
}