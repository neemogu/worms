namespace WormsBasic {
    public interface IWormStrategy {
        Direction NextDirection(Worm worm);
        WormAction NextAction(Worm worm);
    }
}