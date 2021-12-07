using WormsBasic;

namespace WormsAdvanced {
    public interface IFoodLocationProvider {
        Point GetNearestFood(Point fromCoord);
    }
}