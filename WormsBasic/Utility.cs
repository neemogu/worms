using System;

namespace WormsBasic {
    public static class Utility {
        public static int Max(int v1, int v2, int v3, int v4) {
            return Math.Max(Math.Max(v1, v2), Math.Max(v3, v4));
        }
        
        public static Point GetNextLocation(Direction direction, Point currentLocation) {
            var newX = currentLocation.X;
            var newY = currentLocation.Y;
            switch (direction) {
                case Direction.Up:
                    ++newY;
                    break;
                case Direction.Down:
                    --newY;
                    break;
                case Direction.Left:
                    --newX;
                    break;
                case Direction.Right:
                    ++newX;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
            
            return new Point { X = newX, Y = newY};
        }
    }
}