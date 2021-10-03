using System;

namespace WormsBasic {
    public readonly struct Point {
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

        public int DistanceTo(Point p) {
            return Math.Abs(p.X - X) + Math.Abs(p.Y - Y);
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }
    }
}