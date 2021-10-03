using System;
using WormsBasic;

namespace WormsAdvanced {
    public class WormMultiplyingStrategy: IWormStrategy {
        private readonly Random _random = new Random();
        
        public WormMultiplyingStrategy(AdvancedWorm worm, IFoodContainer foodContainer) {
            Worm = worm;
            FoodContainer = foodContainer;
        }

        private AdvancedWorm Worm { get; }
        private IFoodContainer FoodContainer { get; }

        public Direction NextDirection() {
            var nearestFood = FoodContainer.getNearestFood(Worm.Location);

            var result = Direction.Up;
            
            if (nearestFood.X > Worm.Location.X) {
                result = NextAction() != WormAction.Multiply ? Direction.Right : Direction.Left;
            }

            if (nearestFood.X < Worm.Location.X) {
                result = NextAction() != WormAction.Multiply ? Direction.Left : Direction.Right;
            }

            if (_random.Next(2) == 0 && result != Direction.Up) {
                return result;
            }
            
            if (nearestFood.Y > Worm.Location.Y) {
                return NextAction() != WormAction.Multiply ? Direction.Up : Direction.Down;
            }

            if (nearestFood.Y < Worm.Location.Y) {
                return NextAction() != WormAction.Multiply ? Direction.Down : Direction.Up;
            }
            return result;
        }

        public WormAction NextAction() {
            return Worm.Health > AdvancedWorm.HealthRequiredToMultiply ? WormAction.Multiply : WormAction.Move;
        }

        public WormMultiplyingStrategy CreateForAnotherWorm(AdvancedWorm worm) {
            return new WormMultiplyingStrategy(worm, FoodContainer);
        }
    }
}