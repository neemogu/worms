using System;
using WormsDatabase;

namespace WorldGenerator {
    class WorldStateGenerator {
        static void Main(string[] args) {
            if (args.Length < 1) {
                Console.Error.WriteLine("Usage: [world-name]");
                return;
            }
            var name = args[0];
            var saver = new WorldStateSaver(100);
            using (var dbContext = new WormsDbContext()) {
                saver.CreateNewWorldState(name, dbContext);
            }
            Console.WriteLine($@"World with name {name} has been saved");
        }
    }
}