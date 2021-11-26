using System.Text;
using WormsBasic;

namespace WormsAdvanced {
    public interface IFoodContainer {
        public bool HasFoodIn(Point point);

        public string FoodToString();

        public void NextTurn();

        public void CheckForFoodAndEat (AdvancedWorm worm);

        public void CheckForFoodAndEat (AdvancedWorm worm, Point coordToCheck);
    }
}