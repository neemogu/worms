namespace WormsBasic {
    public class WormCircleStrategy: IWormStrategy {
        private int UpY { get; }
        private int DownY { get; }
        private int LeftX { get; }
        private int RightX { get; }
        private Direction CurrentDirection { get; set; } = Direction.Left;
        private Worm Worm { get; }

        public WormCircleStrategy(int circleRadius, Point centerPoint, Worm worm) {
            UpY = centerPoint.Y + circleRadius;
            DownY = centerPoint.Y - circleRadius;
            LeftX = centerPoint.X - circleRadius;
            RightX = centerPoint.X + circleRadius;
            Worm = worm;
        }
        
        public Direction NextDirection() {
            var x = Worm.Location.X;
            var y = Worm.Location.Y;
            if (x < LeftX) {
                CurrentDirection = Direction.Right;
            } else if (x > RightX) {
                CurrentDirection = Direction.Left;
            } else if (x == LeftX) {
                if (y < UpY) {
                    CurrentDirection = Direction.Up;
                } else if (y == UpY) {
                    CurrentDirection = Direction.Right;
                } else {
                    CurrentDirection = Direction.Down;
                }
            } else if (x == RightX) {
                if (y > DownY) {
                    CurrentDirection = Direction.Down;
                } else if (y == DownY) {
                    CurrentDirection = Direction.Left;
                } else {
                    CurrentDirection = Direction.Up;
                }
            } else {
                if (y > UpY) {
                    CurrentDirection = Direction.Down;
                } else if (y == UpY) {
                    CurrentDirection = Direction.Right;
                } else if (y < DownY) {
                    CurrentDirection = Direction.Up;
                } else if (y == DownY) {
                    CurrentDirection = Direction.Left;
                } else {
                    CurrentDirection = Direction.Left;
                }
            }
            return CurrentDirection;
        }

        public WormAction NextAction() {
            return WormAction.Move;
        }
    }
}