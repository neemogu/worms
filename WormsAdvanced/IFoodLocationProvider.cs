using WormsBasic;

namespace WormsAdvanced {
    public interface IFoodLocationProvider {
        public Point GetNearestFood(Point fromCoord);
    }
}