namespace WormsBasic
{
    internal static class WormsWorldProgram
    {
        private static void Main(string[] args) {
            var john = new BasicWorm("John", new Point {X = 0, Y = 0});
            IWormStrategy johnStrategy = new WormCircleStrategy(5, new Point {X = 0, Y = 0}, john);
            john.Strategy = johnStrategy;
            var world = new World(100);
            world.AddWorm(john);
            world.StartLife();
        }
    }
}