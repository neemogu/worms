namespace WormsBasic
{
    internal static class WormsWorldProgram
    {
        private static void Main(string[] args) {
            var john = new BasicWorm("John", new Point(0, 0));
            IWormStrategy johnStrategy = new WormCircleStrategy(5, new Point(0, 0), john);
            john.Strategy = johnStrategy;
            var bob = new BasicWorm("Bob", new Point(-1, 0));
            IWormStrategy bobStrategy = new WormCircleStrategy(5, new Point(0, 0), bob);
            bob.Strategy = bobStrategy;
            var world = new World();
            world.AddWorm(john);
            world.AddWorm(bob);
            world.StartLife();
        }
    }
}