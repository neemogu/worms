namespace WormsBasic {
    public class WormCircleStrategy: IWormStrategy {
        private readonly int _upY;
        private readonly int _downY;
        private readonly int _leftX;
        private readonly int _rightX;
        private Direction _currentDirection = Direction.Left;
        
        public WormCircleStrategy(int circleRadius, int centerX, int centerY) {
            _upY = centerY + circleRadius;
            _downY = centerY - circleRadius;
            _leftX = centerX - circleRadius;
            _rightX = centerX + circleRadius;
        }
        
        public Direction NextDirection(int x, int y) {
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