namespace WormsBasic {
    public class IdleStrategy: IWormStrategy {
        public Direction NextDirection(Worm worm) {
            return Direction.Up;
        }

        public WormAction NextAction(Worm worm) {
            return WormAction.Idle;
        }
    }
}