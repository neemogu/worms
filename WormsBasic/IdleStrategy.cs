namespace WormsBasic {
    public class IdleStrategy: IWormStrategy {
        public Direction NextDirection() {
            return Direction.Up;
        }

        public WormAction NextAction() {
            return WormAction.Idle;
        }
    }
}