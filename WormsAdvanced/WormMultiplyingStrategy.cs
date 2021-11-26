using System;
using System.Collections.Generic;
using System.Linq;
using WormsBasic;

namespace WormsAdvanced {
    public class WormMultiplyingStrategy: IWormStrategy {

        private readonly Random _random = new ();

        private readonly IFoodLocationProvider _foodLocationProvider;
        
        public WormMultiplyingStrategy(IFoodLocationProvider foodLocationProvider) {
            _foodLocationProvider = foodLocationProvider;
        }

        //TODO: rework strategy
        public Direction NextDirection <TWorm> (TWorm worm, List<TWorm> allWorms) where TWorm : Worm {
            var nearestFood = _foodLocationProvider.GetNearestFood(worm.Location);
            
            var result = Direction.Up;
            
            if (nearestFood.X > worm.Location.X) {
                result = NextAction(worm) != WormAction.Multiply ? Direction.Right : Direction.Left;
            }

            if (nearestFood.X < worm.Location.X) {
                result = NextAction(worm) != WormAction.Multiply ? Direction.Left : Direction.Right;
            }

            if (_random.Next(2) == 0 && result != Direction.Up) {
                return result;
            }
            
            if (nearestFood.Y > worm.Location.Y) {
                return NextAction(worm) != WormAction.Multiply ? Direction.Up : Direction.Down;
            }

            if (nearestFood.Y < worm.Location.Y) {
                return NextAction(worm) != WormAction.Multiply ? Direction.Down : Direction.Up;
            }
            return result;
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