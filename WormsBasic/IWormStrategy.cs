using System.Collections.Generic;

namespace WormsBasic {
    public interface IWormStrategy {
        Direction NextDirection <TWorm> (TWorm worm, List<TWorm> allWorms) where TWorm : Worm;
        WormAction NextAction(Worm worm);
    }
}