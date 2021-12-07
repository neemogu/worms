using System.Text;
using WormsBasic;

namespace WormsAdvanced {
    public interface IFoodContainer {
        bool HasFoodIn(Point point);

        string FoodToString();

        void NextTurn();

        void CheckForFoodAndEat(AdvancedWorm worm);

        void CheckForFoodAndEat(AdvancedWorm worm, Point coordToCheck);
    }
}