using System.Collections.Generic;
using WormsBasic;

namespace WormsAdvanced {
    public interface IFoodGenerator {
        void SpawnFood(IDictionary<Point, Food> food);
    }
}