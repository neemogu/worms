using System.Collections.Generic;

namespace WormsBasic {
    public interface IWormStrategy <TWorm> where TWorm : Worm{
        WormAction NextAction (TWorm worm, List<TWorm> allWorms, int step, int run) ;
    }
}