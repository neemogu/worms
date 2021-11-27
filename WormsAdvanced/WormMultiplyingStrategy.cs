using System;
using System.Collections.Generic;
using System.Linq;
using WormsBasic;

namespace WormsAdvanced {
    public class WormMultiplyingStrategy: IWormStrategy {

        private readonly IFoodLocationProvider _foodLocationProvider;
        
        public WormMultiplyingStrategy(IFoodLocationProvider foodLocationProvider) {
            _foodLocationProvider = foodLocationProvider;
        }
        
        public Direction NextDirection <TWorm> (TWorm worm, List<TWorm> allWorms) where TWorm : Worm {
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

        private bool IsDirectionAllowed <TWorm> (Direction direction, TWorm worm, IEnumerable<TWorm> allWorms)
            where TWorm : Worm {
            var nextWormLocation = Utility.GetNextLocation(direction, worm.Location);
            return allWorms.All(w => worm == w || !nextWormLocation.Equals(w.Location));
        }

        public WormAction NextAction(Worm worm) {
            if (worm is not AdvancedWorm advancedWorm) {
                throw new ArgumentException("WormMultiplyingStrategy#NextAction: worm is not AdvancedWorm");
            }
            return advancedWorm.Health > AdvancedWorm.HealthRequiredToMultiply ? WormAction.Multiply : WormAction.Move;
        }
    }
}