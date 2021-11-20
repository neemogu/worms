using System;
using WormsBasic;

namespace WormsAdvanced {
    public class WormMultiplyingStrategy: IWormStrategy {
        private readonly Random _random = new ();
        
        public WormMultiplyingStrategy(IFoodContainer foodContainer) {
            FoodContainer = foodContainer;
        }

        private IFoodContainer FoodContainer { get; }

        public Direction NextDirection(Worm worm) {
            var nearestFood = FoodContainer.GetNearestFood(worm.Location);

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

        public WormAction NextAction(Worm worm) {
            if (worm is not AdvancedWorm advancedWorm) {
                throw new ArgumentException("WormMultiplyingStrategy#NextAction: worm is not AdvancedWorm");
            }
            return advancedWorm.Health > AdvancedWorm.HealthRequiredToMultiply ? WormAction.Multiply : WormAction.Move;
        }
    }
}