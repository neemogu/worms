using System.Collections.Generic;

namespace WormsBasic {
    public class IdleStrategy: IWormStrategy<Worm> {
        public WormAction NextAction(Worm worm, List<Worm> allWorms, int step, int run) {
            return new WormAction { Direction = Direction.Up, Action = Action.Idle };
        }
    }
}