namespace WormsBasic {
    public class BasicWorm: Worm {
        public BasicWorm(string name, Point startPoint) : base(name, startPoint) {}

        public override void Action() {
            Location = NextAction switch {
                WormAction.Move => NextCoord(),
                _ => Location
            };
            PrepareForNextAction();
        }
    }
}