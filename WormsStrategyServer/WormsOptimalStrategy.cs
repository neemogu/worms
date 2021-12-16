using System.Collections.Generic;
using System.Linq;
using WormsAdvanced;
using WormsBasic;

namespace WormsStrategyServer {
    public class WormsOptimalStrategy : IWormStrategy<AdvancedWorm> {
        private readonly IDictionary<Point, Food> _food;
        
        public WormsOptimalStrategy(IDictionary<Point, Food> food) {
            _food = food;
        }
        public WormAction NextAction (AdvancedWorm worm, List<AdvancedWorm> allWorms, int step, int run) {
            return new WormAction { Direction = NextDirection(worm, allWorms), Action = NextAction(worm) };
        }
        
        private Direction NextDirection (AdvancedWorm worm, List<AdvancedWorm> allWorms) {
            var nearestFood = GetNearestFood(worm.Location);
            
            if (nearestFood.X > worm.Location.X && IsDirectionAllowed(Direction.Right, worm, allWorms)) {
                return Direction.Right;
            }

            if (nearestFood.X < worm.Location.X && IsDirectionAllowed(Direction.Left, worm, allWorms)) {
                return Direction.Left;
            }

            if (nearestFood.Y > worm.Location.Y && IsDirectionAllowed(Direction.Up, worm, allWorms)) {
                return Direction.Up;
            }

            if (nearestFood.Y < worm.Location.Y && IsDirectionAllowed(Direction.Down, worm, allWorms)) {
                return Direction.Down;
            }
            return Direction.Up;
        }

        private static bool IsDirectionAllowed (Direction direction, AdvancedWorm worm, IEnumerable<AdvancedWorm> allWorms) {
            var nextWormLocation = Utility.GetNextLocation(direction, worm.Location);
            return allWorms.All(w => worm == w || !nextWormLocation.Equals(w.Location));
        }

        private static Action NextAction (AdvancedWorm worm) {
            return worm.Health > AdvancedWorm.HealthRequiredToMultiply ? Action.Multiply : Action.Move;
        }
        
        private Point GetNearestFood(Point fromCoord) {
            var nearest = new Point { X = int.MaxValue, Y = int.MaxValue};  
            var lowestDistance = int.MaxValue;
            foreach (var foodLocation in _food.Keys) {
                var distance = fromCoord.DistanceTo(foodLocation);
                if (distance >= lowestDistance) continue;
                lowestDistance = distance;
                nearest = foodLocation;
            }
            return nearest;
        }
    }
}