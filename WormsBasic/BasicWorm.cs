namespace WormsBasic {
    public class BasicWorm: Worm {
        public BasicWorm(string name, Point startPoint) : base(name, startPoint) {}

        public override Worm Action() {
            Location = NextAction switch {
                WormAction.Move => NextCoord(),
                _ => Location
            };
            PrepareForNextAction();
            return null;
        }
    }
}