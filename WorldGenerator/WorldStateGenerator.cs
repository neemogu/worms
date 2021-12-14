using System;

namespace WorldGenerator {
    class WorldStateGenerator {
        static void Main(string[] args) {
            if (args.Length < 1) {
                Console.Error.WriteLine("Usage: [world-name]");
                return;
            }
            var name = args[0];
            var saver = new WorldStateSaver(100);
            saver.CreateNewWorldState(name);
            Console.WriteLine($@"World with name {name} has been saved");
        }
    }
}