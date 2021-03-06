using System.Collections.Generic;
using System.Linq;
using WormsAdvanced;
using WormsBasic;
using WormsDatabase;

namespace WormsGeneratedWorld {
    public class PersistedFoodGenerator : IFoodGenerator {
        private readonly Point[] _locations;
        private int _locationIterator;

        public PersistedFoodGenerator(string worldName, WormsDbContext dbContext) {
            _locations = LoadWorldState(worldName, dbContext);
            _locationIterator = 0;
        }

        private static Point[] LoadWorldState(string worldName, WormsDbContext dbContext) {
            return dbContext.WorldStates.FirstOrDefault(ws => ws.Name.Equals(worldName))
                ?.GeneratedFood
                .ToArray();
        }


        public void SpawnFood(IDictionary<Point, Food> food) {
            if (_locations == null) {
                return;
            }
            if (_locationIterator >= _locations.Length) {
                _locationIterator = 0;
            }
            if (!food.ContainsKey(_locations[_locationIterator])) {
                food.Add(_locations[_locationIterator++], new Food());
            }
        }
    }
}