using System;
using WormsBasic;

namespace WormsAdvanced {
    public class AdvancedWorm: Worm {
        private const int StartHealth = 10;
        private const int HealthSpendToMultiply = 1;
        public const int HealthRequiredToMultiply = 11;
        private const int HealthRestoredWithFood = 10;

        private readonly IFoodContainer _foodContainer;
        
        public AdvancedWorm(string name, Point startPoint, IFoodContainer foodContainer) : base(name, startPoint) {
            Health = StartHealth;
            _foodContainer = foodContainer;
        }

        public int Health { get; private set; }

        public override AdvancedWorm Action() {
            AdvancedWorm newWorm = null;
            switch (NextAction) {
                case WormAction.Move:
                    Location = NextCoord();
                    break;
                case WormAction.Multiply:
                    if (Health >= HealthRequiredToMultiply) {
                        newWorm = new AdvancedWorm(Utils.NextName(), NextCoord(), _foodContainer);
                        var newWormStrategy = new WormMultiplyingStrategy(newWorm, _foodContainer);
                        newWorm.Strategy = newWormStrategy;
                        Health -= HealthSpendToMultiply;
                    }
                    break;
            }
            PrepareForNextAction();
            return newWorm;
        }

        public void Live() {
            --Health;
        }

        public void Eat() {
            Health += HealthRestoredWithFood;
        }

        public override string ToString() {
            return $"{Name} ({Health} HP) at {Location}";
        }
    }
}