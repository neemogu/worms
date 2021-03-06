using System;
using System.Collections.Generic;
using System.Text;

namespace WormsBasic {
    public abstract class Worm {
        protected Worm(string name, Point startPoint) {
            Name = name;
            Location = startPoint;
        }
        
        public string Name { get; }
        public Point Location { get; protected set; }

        public override string ToString() {
            return $"{Name}: {Location}";
        }

        public void Move(Direction direction) {
            Location = Utility.GetNextLocation(direction, Location);
        }

        public static string WormsArrayToString<T>(List<T> worms) where T: Worm {
            var builder = new StringBuilder();
            builder.Append("[\r\n");
            worms.ForEach((w) => { builder.Append("\t" + w + ",\r\n"); });
            builder.Remove(builder.Length - 3, 1);
            builder.Append(']');
            return builder.ToString();
        }
    }
}