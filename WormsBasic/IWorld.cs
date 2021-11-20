namespace WormsBasic {
    public interface IWorld {
        void AddWorm(Worm worm);
        void StartLife();
        void SetStrategy(IWormStrategy strategy);
    }
}