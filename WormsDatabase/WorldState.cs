using System.Collections.Generic;
using Newtonsoft.Json;
using WormsBasic;

namespace WormsDatabase {
    public class WorldState {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Point> GeneratedFood { get; set; }

        public string GeneratedFoodJson {
            get => JsonConvert.SerializeObject(GeneratedFood ?? new List<Point>());
            set => GeneratedFood = string.IsNullOrWhiteSpace(value)
                ? new List<Point>()
                : JsonConvert.DeserializeObject<ICollection<Point>>(value);
        }
    }
}