namespace WormsBasic
{
    class WormsWorldProgram
    {
        static void Main(string[] args) {
            var john = new Worm("John", 0, 0,
                new WormCircleStrategy(5, 0, 0));
            var bob = new Worm("Bob", -1, 0,
                new WormCircleStrategy(5, 0, 0));
            var world = new World();
            world.AddWorm(john);
            world.AddWorm(bob);
            world.StartLife();
        }
    }
}