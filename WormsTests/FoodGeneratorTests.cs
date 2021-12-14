using System.Collections.Generic;
using NUnit.Framework;
using WormsAdvanced;
using WormsBasic;

namespace WormsTests {
    public class FoodGeneratorTests {
        [Test]
        public void FoodLocationUniquenessTest() {
            const int foodCountToGenerate = 50;
            var generator = new RandomFoodGenerator(0, 5);
            var allFood = new Dictionary<Point, Food>();
            for (var i = 0; i < 10; ++i) {
                for (var j = 0; j < foodCountToGenerate; ++j) {
                    generator.SpawnFood(allFood);
                }
                Assert.AreEqual(foodCountToGenerate, allFood.Keys.Count);
                allFood.Clear();
            }
        }
    }
}