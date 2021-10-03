namespace WormsBasic {
    public class WormCircleStrategy: IWormStrategy {
        private readonly int _upY;
        private readonly int _downY;
        private readonly int _leftX;
        private readonly int _rightX;
        private Direction _currentDirection = Direction.Left;
        private readonly Worm _worm;

        public WormCircleStrategy(int circleRadius, Point centerPoint, Worm worm) {
            _upY = centerPoint.Y + circleRadius;
            _downY = centerPoint.Y - circleRadius;
            _leftX = centerPoint.X - circleRadius;
            _rightX = centerPoint.X + circleRadius;
            _worm = worm;
        }
        
        public Direction NextDirection() {
            var x = _worm.Location.X;
            var y = _worm.Location.Y;
            if (x < _leftX) {
                _currentDirection = Direction.Right;
            } else if (x > _rightX) {
                _currentDirection = Direction.Left;
            } else if (x == _leftX) {
                if (y < _upY) {
                    _currentDirection = Direction.Up;
                } else if (y == _upY) {
                    _currentDirection = Direction.Right;
                } else {
                    _currentDirection = Direction.Down;
                }
            } else if (x == _rightX) {
                if (y > _downY) {
                    _currentDirection = Direction.Down;
                } else if (y == _downY) {
                    _currentDirection = Direction.Left;
                } else {
                    _currentDirection = Direction.Up;
                }
            } else {
                if (y > _upY) {
                    _currentDirection = Direction.Down;
                } else if (y == _upY) {
                    _currentDirection = Direction.Right;
                } else if (y < _downY) {
                    _currentDirection = Direction.Up;
                } else if (y == _downY) {
                    _currentDirection = Direction.Left;
                } else {
                    _currentDirection = Direction.Left;
                }
            }
            return _currentDirection;
        }

        public WormAction NextAction() {
            return WormAction.Move;
        }
    }
}