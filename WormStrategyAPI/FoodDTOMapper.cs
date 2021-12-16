using System.Collections.Generic;
using WormsAdvanced;
using WormsBasic;
using WormStrategyAPI;

namespace WormStrategyAPI {
    public static class FoodDTOMapper  {
        public static IDictionary<Point, Food> FromDTOToMap(IEnumerable<FoodDTO> food) {
            var result = new Dictionary<Point, Food>();
            foreach (var foodDto in food) {
                result.Add(foodDto.Position, new Food(Food.MaxRecency - foodDto.ExpiresIn));
            }
            return result;
        }
        
        public static IEnumerable<FoodDTO> FromMapToDTO(IDictionary<Point, Food> foodMap) {
            var result = new List<FoodDTO>();
            foreach (var (location, food) in foodMap) {
                result.Add(new FoodDTO {ExpiresIn = Food.MaxRecency - food.Recency, Position = location});
            }
            return result;
        }
    }
}