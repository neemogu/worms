using WormsBasic;

namespace WormsAdvanced {
    public class Food {
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