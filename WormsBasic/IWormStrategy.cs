namespace WormsBasic {
    public interface IWormStrategy {
        Direction NextDirection();
        WormAction NextAction();
    }
}