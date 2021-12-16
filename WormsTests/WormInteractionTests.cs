using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using WormsAdvanced;
using WormsBasic;
using Action = WormsBasic.Action;

namespace WormsTests {
    public class WormInteractionTests {
        private Mock<IFoodContainer> _foodContainerMock;
        private Mock<IWormStrategy<AdvancedWorm>> _strategyMock;
        private NameGenerator _nameGenerator;
        private Mock<Logger> _loggerMock;
        private AdvancedWorld _world;
        private AdvancedWorm _worm;

        [SetUp]
        public void Init() {
            _foodContainerMock = new Mock<IFoodContainer>();
            _strategyMock = new Mock<IWormStrategy<AdvancedWorm>>(MockBehavior.Strict);
            _nameGenerator = new NameGenerator();
            _loggerMock = new Mock<Logger>();
            _world = new AdvancedWorld(_strategyMock.Object, _nameGenerator, _foodContainerMock.Object,
                _loggerMock.Object);
            _worm = new AdvancedWorm("John", new Point { X = 0, Y = 0 });
            _world.AddWorm(_worm);
        }

        [Test]
        public void MoveToEmptyLocation() {
            _strategyMock.Setup(strategy => strategy.NextAction(
                It.IsAny<AdvancedWorm>(), It.IsAny<List<AdvancedWorm>>(), It.IsAny<int>(), It.IsAny<int>()
                )).Returns(new WormAction { Direction = Direction.Up, Action = Action.Move });
            _foodContainerMock.Setup(container => container.HasFoodIn(
                It.Is<Point>(point => point.X == _worm.Location.X && point.Y == _worm.Location.Y + 1)
                )).Returns(false);
            var startHealth = _worm.Health;
            var startLocation = _worm.Location;
            _world.NextTurn(1);
            Assert.AreEqual(startHealth - 1, _worm.Health);
            Assert.AreEqual(startLocation.X, _worm.Location.X);
            Assert.AreEqual(startLocation.Y + 1, _worm.Location.Y);
        }
        
        [Test]
        public void FoodAtWormLocation() {
            _strategyMock.Setup(strategy => strategy.NextAction(
                It.IsAny<AdvancedWorm>(), It.IsAny<List<AdvancedWorm>>(), It.IsAny<int>(), It.IsAny<int>()
            )).Returns(new WormAction {Direction = Direction.Up, Action = Action.Move});
            _foodContainerMock.Setup(container => container.HasFoodIn(
                It.Is<Point>(point => point.X == _worm.Location.X && point.Y == _worm.Location.Y + 1)
                )).Returns(false);
            _foodContainerMock.Setup(container => container.CheckForFoodAndEat(_worm)).Callback((AdvancedWorm worm) => worm.Eat());
            var startHealth = _worm.Health;
            _world.NextTurn(1);
            Assert.AreEqual(startHealth + 9, _worm.Health);
        }
        
        [Test]
        public void MoveToLocationWithFood() {
            _strategyMock.Setup(strategy => strategy.NextAction(
                It.IsAny<AdvancedWorm>(), It.IsAny<List<AdvancedWorm>>(), It.IsAny<int>(), It.IsAny<int>()
            )).Returns(new WormAction {Direction = Direction.Up, Action = Action.Move});
            _foodContainerMock.Setup(container => container.HasFoodIn(
                It.Is<Point>(point => point.X == _worm.Location.X && point.Y == _worm.Location.Y + 1)
                )).Returns(true);
            _foodContainerMock.Setup(container => container.CheckForFoodAndEat(_worm, 
                    It.Is<Point>(point => point.X == _worm.Location.X && point.Y == _worm.Location.Y + 1)
                    ))
                .Callback((AdvancedWorm worm, Point point) => worm.Eat());
            var startHealth = _worm.Health;
            var startLocation = _worm.Location;
            _world.NextTurn(1);
            Assert.AreEqual(startHealth + 9, _worm.Health);
            Assert.AreEqual(startLocation.X, _worm.Location.X);
            Assert.AreEqual(startLocation.Y + 1, _worm.Location.Y);
        }
        
        [Test]
        public void MoveToLocationWithFoodFromLocationWithFood() {
            _strategyMock.Setup(strategy => strategy.NextAction(
                It.IsAny<AdvancedWorm>(), It.IsAny<List<AdvancedWorm>>(), It.IsAny<int>(), It.IsAny<int>()
            )).Returns(new WormAction {Direction = Direction.Up, Action = Action.Move});
            _foodContainerMock.Setup(container => container.HasFoodIn(
                It.Is<Point>(point => point.X == _worm.Location.X && point.Y == _worm.Location.Y + 1)
            )).Returns(true);
            _foodContainerMock.Setup(container => container.CheckForFoodAndEat(_worm)).Callback((AdvancedWorm worm) => worm.Eat());
            _foodContainerMock.Setup(container => container.CheckForFoodAndEat(_worm, 
                    It.Is<Point>(point => point.X == _worm.Location.X && point.Y == _worm.Location.Y + 1)
                ))
                .Callback((AdvancedWorm worm, Point point) => worm.Eat());
            var startHealth = _worm.Health;
            var startLocation = _worm.Location;
            _world.NextTurn(1);
            Assert.AreEqual(startHealth + 19, _worm.Health);
            Assert.AreEqual(startLocation.X, _worm.Location.X);
            Assert.AreEqual(startLocation.Y + 1, _worm.Location.Y);
        }


        [Test]
        public void MoveToLocationWithAnotherWorm() {
            var bob = new AdvancedWorm("Bob", new Point { X = 0, Y = 1 });
            _world.AddWorm(bob);
            _strategyMock.Setup(strategy => strategy.NextAction(
                It.IsAny<AdvancedWorm>(), It.IsAny<List<AdvancedWorm>>(), It.IsAny<int>(), It.IsAny<int>()
            )).Returns(new WormAction {Direction = Direction.Up, Action = Action.Move});
            _strategyMock.Setup(strategy => strategy.NextAction(
                bob, It.IsAny<List<AdvancedWorm>>(), It.IsAny<int>(), It.IsAny<int>()
                )).Returns(new WormAction {Direction = Direction.Up, Action = Action.Idle});
            var startLocation = _worm.Location;
            _world.NextTurn(1);
            Assert.AreEqual(startLocation.X, _worm.Location.X);
            Assert.AreEqual(startLocation.Y, _worm.Location.Y);
        }

        [Test]
        public void MultiplyToLocationWithAnotherWorm() {
            var bob = new AdvancedWorm("Bob", new Point { X = 0, Y = 1 });
            _world.AddWorm(bob);
            
            _strategyMock.Setup(strategy => strategy.NextAction(
                It.IsAny<AdvancedWorm>(), It.IsAny<List<AdvancedWorm>>(), It.IsAny<int>(), It.IsAny<int>()
            )).Returns(new WormAction {Direction = Direction.Up, Action = Action.Multiply});
            _strategyMock.Setup(strategy => strategy.NextAction(
                bob, It.IsAny<List<AdvancedWorm>>(), It.IsAny<int>(), It.IsAny<int>()
            )).Returns(new WormAction {Direction = Direction.Up, Action = Action.Idle});
            var startHealth = _worm.Health;
            _world.NextTurn(1);
            Assert.AreEqual(startHealth - 1, _worm.Health);
            Assert.AreEqual(2, _world.GetAllWorms().Length);
        }
        
        [Test]
        public void MultiplyToLocationWithFood() {
            _strategyMock.Setup(strategy => strategy.NextAction(
                It.IsAny<AdvancedWorm>(), It.IsAny<List<AdvancedWorm>>(), It.IsAny<int>(), It.IsAny<int>()
            )).Returns(new WormAction {Direction = Direction.Up, Action = Action.Multiply});
            _foodContainerMock.Setup(container => container.HasFoodIn(
                It.Is<Point>(point => point.X == _worm.Location.X && point.Y == _worm.Location.Y + 1)
            )).Returns(true);
            var startHealth = _worm.Health;
            _world.NextTurn(1);
            Assert.AreEqual(startHealth - 1, _worm.Health);
            Assert.AreEqual(1, _world.GetAllWorms().Length);
        }
        
        [Test]
        public void MultiplyFreeLocation() {
            _strategyMock.Setup(strategy => strategy.NextAction(
                It.IsAny<AdvancedWorm>(), It.IsAny<List<AdvancedWorm>>(), It.IsAny<int>(), It.IsAny<int>()
            )).Returns(new WormAction { Direction = Direction.Up, Action = Action.Multiply });
            _foodContainerMock.Setup(container => container.HasFoodIn(
                It.Is<Point>(point => point.X == _worm.Location.X && point.Y == _worm.Location.Y + 1)
            )).Returns(false);
            _worm.Eat();
            var startHealth = _worm.Health;
            _world.NextTurn(1);
            Assert.AreEqual(startHealth - 11, _worm.Health);
            Assert.AreEqual(2, _world.GetAllWorms().Length);
            Assert.AreEqual(new Point {X = 0, Y = 1}, _world.GetAllWorms()[1].Location);
        }
    }
}