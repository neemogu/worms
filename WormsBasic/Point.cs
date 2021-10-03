namespace WormsBasic {
    public readonly struct Point {
        public Point(int x, int y) {
            X = x;
            Y = y;
        }
        public int X { get; init; }
        public int Y { get; init; }

        public override string ToString() {
            return $"({X}, {Y})";
        }
        public override bool Equals(object obj) {
            if (obj == null || !(GetType() == obj.GetType())) {
                return false;
            }
            var p = (Point)obj;
            return p.X == X && p.Y == Y;
        }

        public override int GetHashCode() {
            return (X << 2) ^ Y;
        }
    }
}