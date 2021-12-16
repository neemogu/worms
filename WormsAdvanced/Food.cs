using WormsBasic;

namespace WormsAdvanced {
    public class Food {
        public Food(): this(0) {}

        public Food(int recency) {
            Recency = recency;
        }
        
        public const int MaxRecency = 10;
        public int Recency { get; private set; }

        public void Stale() {
            ++Recency;
        }

        public bool IsRotten() {
            return Recency > MaxRecency;
        }
    }
}