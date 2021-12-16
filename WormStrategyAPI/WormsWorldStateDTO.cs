using System.Collections.Generic;

namespace WormStrategyAPI {
    public class WormsWorldStateDTO {
        public IEnumerable<WormDTO> Worms { get; set; }
        public IEnumerable<FoodDTO> Food { get; set; }
    }
}