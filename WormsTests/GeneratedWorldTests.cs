using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using WormsAdvanced;
using WormsBasic;
using WormsDatabase;
using WormsGeneratedWorld;

namespace WormsTests {
    public class GeneratedWorldTests {
        private DbContextOptions<WormsDbContext> _dbContextOptions;

        [SetUp]
        public void Init() {
            _dbContextOptions = new DbContextOptionsBuilder<WormsDbContext>()
                .UseInMemoryDatabase("WormsTestDb")
                .Options;
        }
        
        [Test]
        public void PersistedFoodGeneratorTest() {
            using (var dbContext = new WormsDbContext(_dbContextOptions)) {
                const string worldName = "world";
                var foodLocations = new List<Point>();
                foodLocations.Add(new Point {X = 0, Y = 0});
                foodLocations.Add(new Point {X = 1, Y = 0});
                foodLocations.Add(new Point {X = 0, Y = 1});
                foodLocations.Add(new Point {X = 1, Y = 1});
                var worldState = new WorldState {
                    Id = 1,
                    Name = worldName,
                    GeneratedFood = foodLocations,
                };
                dbContext.WorldStates.Add(worldState);
                dbContext.SaveChanges();

                IFoodGenerator foodGenerator = new PersistedFoodGenerator(worldName, dbContext);

                var food = new Dictionary<Point, Food>();

                for (var i = 0; i < foodLocations.Count; ++i) {
                    foodGenerator.SpawnFood(food);
                    Assert.AreEqual(1, food.Count);
                    Assert.True(food.ContainsKey(foodLocations[i]));
                    food.Clear();
                }
                
                foodGenerator = new PersistedFoodGenerator(worldName, dbContext);

                food = new Dictionary<Point, Food>();

                for (var i = 0; i < foodLocations.Count; ++i) {
                    foodGenerator.SpawnFood(food);
                }

                Assert.AreEqual(foodLocations.Count, food.Keys.Count);
            }
        }
    }
}