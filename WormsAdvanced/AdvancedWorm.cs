using System;
using WormsBasic;

namespace WormsAdvanced {
    public class AdvancedWorm: Worm {
        private const int StartHealth = 10;
        private const int HealthSpendToMultiply = 1;
        public const int HealthRequiredToMultiply = 11;
        private const int HealthRestoredWithFood = 10;

        public AdvancedWorm(string name, Point startPoint) : base(name, startPoint) {
            Health = StartHealth;
        }

        public int Health { get; private set; }

        public AdvancedWorm TryMultiply(Point multiplyCoord, string newWormName) {
            if (Health < HealthRequiredToMultiply) return null;
            var newWorm = new AdvancedWorm(newWormName, multiplyCoord);
            Health -= HealthSpendToMultiply;
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