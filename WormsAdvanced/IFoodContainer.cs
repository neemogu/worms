using WormsBasic;

namespace WormsAdvanced {
    public interface IFoodContainer {
        Point GetNearestFood(Point fromCoord);
    }
}