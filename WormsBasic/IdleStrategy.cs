using System.Collections.Generic;

namespace WormsBasic {
    public class IdleStrategy: IWormStrategy {
        public Direction NextDirection<TWorm>(TWorm worm, List<TWorm> allWorms) where TWorm : Worm {
            return Direction.Up;
        }

        public WormAction NextAction(Worm worm) {
            return WormAction.Idle;
        }
    }
}