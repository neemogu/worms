using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using WorldGenerator;
using WormsDatabase;

namespace WormsTests {
    public class WorldStateGeneratorTests {
        private DbContextOptions<WormsDbContext> _dbContextOptions;

        [SetUp]
        public void Init() {
            _dbContextOptions = new DbContextOptionsBuilder<WormsDbContext>()
                .UseInMemoryDatabase("WormsTestDb")
                .Options;
        }

        [Test]
        public void WorldStateSaverTest() {
            using (var dbContext = new WormsDbContext(_dbContextOptions)) {
                const int turns = 5;
                var saver = new WorldStateSaver(turns);
                const string worldName = "world";
                
                var generatedFood = saver.CreateNewWorldState(worldName, dbContext);
                Assert.AreEqual(turns, generatedFood.Count);
                Assert.True(dbContext.WorldStates.Count() == 1);
                var savedState = dbContext.WorldStates.FirstOrDefault(ws => ws.Name.Equals(worldName));
                Assert.NotNull(savedState);
                Assert.AreEqual(worldName, savedState.Name);
                Assert.AreEqual(turns, savedState.GeneratedFood.Count);
                var i = 0;
                foreach (var point in savedState.GeneratedFood) {
                    Assert.AreEqual(generatedFood[i], point);
                    ++i;
                }
                
                generatedFood = saver.CreateNewWorldState(worldName, dbContext);
                Assert.AreEqual(turns, generatedFood.Count);
                Assert.True(dbContext.WorldStates.Count() == 1);
                savedState = dbContext.WorldStates.FirstOrDefault(ws => ws.Name.Equals(worldName));
                Assert.NotNull(savedState);
                Assert.AreEqual(worldName, savedState.Name);
                Assert.AreEqual(turns, savedState.GeneratedFood.Count);
                i = 0;
                foreach (var point in savedState.GeneratedFood) {
                    Assert.AreEqual(generatedFood[i], point);
                    ++i;
                }
            }
        }
    }
}