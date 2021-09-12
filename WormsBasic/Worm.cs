using System;
using System.Collections.Generic;
using System.Text;

namespace WormsBasic {
    public class Worm {
        public Worm(string name, int startX, int startY,
            IWormStrategy strategy) {
            Name = name;
            X = startX;
            Y = startY;
            WormStrategy = strategy;
            _nextDirection = strategy.NextDirection(X, Y);
            NextAction = strategy.NextAction();
        }

        private Direction _nextDirection;
        public WormAction NextAction { get; private set; }
        public string Name { get; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public IWormStrategy WormStrategy { get; set; }

        public int NextX() {
            return _nextDirection switch {
                Direction.Left => X - 1,
                Direction.Right => X + 1,
                _ => X
            };
        }

        public int NextY() {
            return _nextDirection switch {
                Direction.Down => Y - 1,
                Direction.Up => Y + 1,
                _ => Y
            };
        }

        public void Action() {
            switch (NextAction) {
                case WormAction.Move:
                    X = NextX();
                    Y = NextY();
                    break;
            }
            _nextDirection = WormStrategy.NextDirection(X, Y);
            NextAction = WormStrategy.NextAction();
        }

        public override string ToString() {
            return $"{Name} ({X}, {Y})";
        }
        
        public static string WormsArrayToString(List<Worm> worms) {
            var builder = new StringBuilder();
            builder.Append('[');
            worms.ForEach((w) => { builder.Append(w + ", "); });
            builder.Remove(builder.Length - 2, 2);
            builder.Append(']');
            return builder.ToString();
        }
    }
}