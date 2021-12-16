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
            AdvancedWorm worm = new AdvancedWorm("john", new Point {X = 0, Y = 0});
            var allWorms = new List<AdvancedWorm> { worm };
            var foodLocationProviderMock = new Mock<IFoodLocationProvider>();
            foodLocationProviderMock.Setup((flp) => flp.GetNearestFood(It.IsAny<Point>()))
                .Returns(new Point {X = 2, Y = 2});
            IWormStrategy<AdvancedWorm> strategy = new WormMultiplyingStrategy(foodLocationProviderMock.Object);
            var nextDirection = strategy.NextAction(worm, allWorms, 1, 1).Direction;
            Assert.True(nextDirection.Equals(Direction.Right) || nextDirection.Equals(Direction.Up));
        }
        
        [Test]
        public void MoveToNearestFoodWithAnotherWormsStrategyTest() {
            var worm = new AdvancedWorm("john", new Point {X = 0, Y = 0});
            var allWorms = new List<AdvancedWorm> { worm };
            var foodLocationProviderMock = new Mock<IFoodLocationProvider>();
            foodLocationProviderMock.Setup((flp) => flp.GetNearestFood(It.IsAny<Point>()))
                .Returns(new Point {X = 2, Y = 2});
            IWormStrategy<AdvancedWorm> strategy = new WormMultiplyingStrategy(foodLocationProviderMock.Object);
            AdvancedWorm rightWorm = new AdvancedWorm("right", new Point { X = 1, Y = 0 });
            AdvancedWorm upWorm = new AdvancedWorm("up", new Point { X = 0, Y = 1 });
            allWorms.Add(rightWorm);
            var nextDirection = strategy.NextAction(worm, allWorms, 1, 1).Direction;
            Assert.AreEqual(Direction.Up, nextDirection);
            allWorms.Remove(rightWorm);
            allWorms.Add(upWorm);
            nextDirection = strategy.NextAction(worm, allWorms, 1, 1).Direction;
            Assert.AreEqual(Direction.Right, nextDirection);
        }
    }
}