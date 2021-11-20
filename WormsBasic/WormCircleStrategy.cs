namespace WormsBasic {
    public class WormCircleStrategy: IWormStrategy {
        private int UpY { get; }
        private int DownY { get; }
        private int LeftX { get; }
        private int RightX { get; }

        public WormCircleStrategy(int circleRadius, Point centerPoint) {
            UpY = centerPoint.Y + circleRadius;
            DownY = centerPoint.Y - circleRadius;
            LeftX = centerPoint.X - circleRadius;
            RightX = centerPoint.X + circleRadius;
        }
        
        public Direction NextDirection(Worm worm) {
            var x = worm.Location.X;
            var y = worm.Location.Y;
            if (x < LeftX) {
                return Direction.Right;
            } 
            if (x > RightX) {
                return Direction.Left;
            } 
            if (x == LeftX) {
                if (y < UpY) {
                    return Direction.Up;
                } 
                if (y < UpY) {
                    return Direction.Down;
                }
                return Direction.Right;
            }
            if (x == RightX) {
                if (y > DownY) {
                    return Direction.Down;
                }
                if (y < DownY) {
                    return Direction.Up;
                }
                return Direction.Left;
            }
            
            if (y > UpY) {
                return Direction.Down;
            } 
            if (y == UpY) {
                return Direction.Right;
            }
            if (y < DownY) {
                return Direction.Up;
            }
            if (y == DownY) {
                return Direction.Left;
            }
            return Direction.Left;
        }

        public WormAction NextAction(Worm worm) {
            return WormAction.Move;
        }
    }
}