using System.Collections.Generic;
using System.Linq;
using WormsAdvanced;
using WormsBasic;
using WormsDatabase;

namespace WorldGenerator {
    public class WorldStateSaver {
        private readonly IFoodGenerator _foodGenerator;
        private readonly int _numberOfTurns;

        public WorldStateSaver(int numberOfTurns) {
            _foodGenerator = new RandomFoodGenerator(0, 5);
            _numberOfTurns = numberOfTurns;
        }

        private List<Point> GenerateFoodLocations() {
            var generatedFood = new Dictionary<Point, Food>();
            foreach (var _ in Enumerable.Range(0, _numberOfTurns)) {
                _foodGenerator.SpawnFood(generatedFood);
            }

            return generatedFood.Keys.ToList();
        }

        public List<Point> CreateNewWorldState(string name, WormsDbContext dbContext) {
            var generatedFood = GenerateFoodLocations();
            var existingState = dbContext.WorldStates.FirstOrDefault(ws => ws.Name.Equals(name));
            if (existingState != null) {
                existingState.GeneratedFood = generatedFood;
                dbContext.WorldStates.Update(existingState);
            } else {
                var newWorldState = new WorldState {
                    Name = name,
                    GeneratedFood = generatedFood
                };
                dbContext.WorldStates.Add(newWorldState);
            }

            dbContext.SaveChanges();

            return generatedFood;
        }
    }
}