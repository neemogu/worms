namespace WormsBasic {
    public interface IWormStrategy {
        Direction NextDirection(int x, int y);
        WormAction NextAction();
    }
}