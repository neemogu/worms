using System;
using System.Collections.Generic;
using System.Text;

namespace WormsBasic {
    public abstract class Worm {
        protected Worm(string name, Point startPoint) {
            Name = name;
            Location = startPoint;
            Strategy = new IdleStrategy();
        }
        
        public WormAction NextAction { get; private set; }
        private Direction NextDirection { get; set; }
        public string Name { get; }
        public Point Location { get; protected set; }

        private IWormStrategy _strategy;
        public IWormStrategy Strategy {
            get => _strategy;
            set {
                _strategy = value;
                PrepareForNextAction();
            }
        }
        
        private int NextX() {
            return NextDirection switch {
                Direction.Left => Location.X - 1,
                Direction.Right => Location.X + 1,
                _ => Location.X
            };
        }

        private int NextY() {
            return NextDirection switch {
                Direction.Down => Location.Y - 1,
                Direction.Up => Location.Y + 1,
                _ => Location.Y
            };
        }

        public Point NextCoord() {
            return new Point { X = NextX(), Y = NextY() };
        }
        
        public override string ToString() {
            return $"{Name}: {Location}";
        }

        protected void PrepareForNextAction() {
            NextDirection = Strategy.NextDirection();
            NextAction = Strategy.NextAction();
        }

        public abstract void Action();
        
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