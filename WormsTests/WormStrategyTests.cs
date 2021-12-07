using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using WormsAdvanced;
using WormsBasic;

namespace WormsTests {
    public class WormStrategyTests {

        [Test]
        public void MoveToNearestFoodStrategyTest() {
            Worm worm = new AdvancedWorm("john", new Point {X = 0, Y = 0});
            var allWorms = new List<Worm> { worm };
            var foodLocationProviderMock = new Mock<IFoodLocationProvider>();
            foodLocationProviderMock.Setup((flp) => flp.GetNearestFood(It.IsAny<Point>()))
                .Returns(new Point {X = 2, Y = 2});
            IWormStrategy strategy = new WormMultiplyingStrategy(foodLocationProviderMock.Object);
            var nextDirection = strategy.NextDirection(worm, allWorms);
            Assert.True(nextDirection.Equals(Direction.Right) || nextDirection.Equals(Direction.Up));
        }
        
        [Test]
        public void MoveToNearestFoodWithAnotherWormsStrategyTest() {
            Worm worm = new AdvancedWorm("john", new Point {X = 0, Y = 0});
            var allWorms = new List<Worm> { worm };
            var foodLocationProviderMock = new Mock<IFoodLocationProvider>();
            foodLocationProviderMock.Setup((flp) => flp.GetNearestFood(It.IsAny<Point>()))
                .Returns(new Point {X = 2, Y = 2});
            IWormStrategy strategy = new WormMultiplyingStrategy(foodLocationProviderMock.Object);
            Worm rightWorm = new AdvancedWorm("right", new Point { X = 1, Y = 0 });
            Worm upWorm = new AdvancedWorm("up", new Point { X = 0, Y = 1 });
            allWorms.Add(rightWorm);
            Assert.AreEqual(Direction.Up, strategy.NextDirection(worm, allWorms));
            allWorms.Remove(rightWorm);
            allWorms.Add(upWorm);
            Assert.AreEqual(Direction.Right, strategy.NextDirection(worm, allWorms));
        }
    }
}