using System;
using System.Collections.Generic;
using System.Linq;
using WormsBasic;
using Action = WormsBasic.Action;

namespace WormsAdvanced {
    public class WormMultiplyingStrategy: IWormStrategy<AdvancedWorm> {

        private readonly IFoodLocationProvider _foodLocationProvider;
        
        public WormMultiplyingStrategy(IFoodLocationProvider foodLocationProvider) {
            _foodLocationProvider = foodLocationProvider;
        }

        public WormAction NextAction(AdvancedWorm worm, List<AdvancedWorm> allWorms, int step, int run) {
            return new WormAction { Direction = NextDirection(worm, allWorms), Action = NextAction(worm) };
        }

        private Direction NextDirection (AdvancedWorm worm, List<AdvancedWorm> allWorms) {
            var nearestFood = _foodLocationProvider.GetNearestFood(worm.Location);
            
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
    }
}