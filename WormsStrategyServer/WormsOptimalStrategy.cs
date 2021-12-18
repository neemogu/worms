using System;
using System.Collections.Generic;
using System.Linq;
using WormsAdvanced;
using WormsBasic;
using Action = WormsBasic.Action;

namespace WormsStrategyServer {
    public class WormsOptimalStrategy : IWormStrategy<AdvancedWorm> {
        private readonly IDictionary<Point, Food> _food;

        public WormsOptimalStrategy(IDictionary<Point, Food> food) {
            _food = food;
        }
        public WormAction NextAction (AdvancedWorm worm, List<AdvancedWorm> allWorms, int step, int run) {
            var action = NextAction(worm, step);
            if (action == Action.Move) {
                return new WormAction { Direction = NextMoveDirection(worm, allWorms), Action = action };
            }
            try {
                return new WormAction { Direction = NextMultiplyDirection(worm, allWorms), Action = action };
            } catch (Exception) {
                return new WormAction { Direction = NextMoveDirection(worm, allWorms), Action = action };
            }
        }
        
        private Direction NextMoveDirection (AdvancedWorm worm, List<AdvancedWorm> allWorms) {
            var sortedFood = GetSortedFood(worm.Location);

            foreach (var foodLocation in sortedFood) {
                _food.TryGetValue(foodLocation, out var foundFood);
                if (!IsMakeSenseToMove(worm, allWorms, foodLocation, foundFood)) {
                    continue;
                }
                
                if (new Random().Next(0, 2) == 0) {
                    if (foodLocation.X > worm.Location.X && IsDirectionAllowedToMove(Direction.Right, worm, allWorms)) {
                        return Direction.Right;
                    }

                    if (foodLocation.X < worm.Location.X && IsDirectionAllowedToMove(Direction.Left, worm, allWorms)) {
                        return Direction.Left;
                    }

                    if (foodLocation.Y > worm.Location.Y && IsDirectionAllowedToMove(Direction.Up, worm, allWorms)) {
                        return Direction.Up;
                    }

                    if (foodLocation.Y < worm.Location.Y && IsDirectionAllowedToMove(Direction.Down, worm, allWorms)) {
                        return Direction.Down;
                    }
                } else {
                    if (foodLocation.Y > worm.Location.Y && IsDirectionAllowedToMove(Direction.Up, worm, allWorms)) {
                        return Direction.Up;
                    }

                    if (foodLocation.Y < worm.Location.Y && IsDirectionAllowedToMove(Direction.Down, worm, allWorms)) {
                        return Direction.Down;
                    }
                    
                    if (foodLocation.X > worm.Location.X && IsDirectionAllowedToMove(Direction.Right, worm, allWorms)) {
                        return Direction.Right;
                    }

                    if (foodLocation.X < worm.Location.X && IsDirectionAllowedToMove(Direction.Left, worm, allWorms)) {
                        return Direction.Left;
                    }
                }
            }
            return DirectionToCenter(worm);
        }

        private static Direction DirectionToCenter(AdvancedWorm worm) {
            if (worm.Location.X < 0) {
                return Direction.Right;
            }

            if (worm.Location.X > 0) {
                return Direction.Left;
            }
            
            if (worm.Location.Y < 0) {
                return Direction.Up;
            }

            if (worm.Location.Y > 0) {
                return Direction.Down;
            }

            return (Direction)new Random().Next(0, 4);
        }

        private bool IsMakeSenseToMove(AdvancedWorm worm, List<AdvancedWorm> allWorms, Point foodLocation, Food food) {
            return foodLocation.DistanceTo(worm.Location) <= Food.MaxRecency - food.Recency
                   && !IsLockedByOtherWorms(worm, allWorms, foodLocation);
        }

        private bool IsLockedByOtherWorms(AdvancedWorm worm, List<AdvancedWorm> allWorms, Point foodLocation) {
            var thisWormLockedFood = false;
            foreach (var feWorm in allWorms) {
                var wormDistance = worm.Location.DistanceTo(foodLocation);
                var feWormDistance = feWorm.Location.DistanceTo(foodLocation);
                if (feWormDistance < wormDistance) {
                    if (GetNearestFoodDistance(feWorm.Location) >= feWormDistance) {
                        return true;
                    }
                }

                if (feWormDistance > wormDistance) {
                    continue;
                }

                if (wormDistance == feWormDistance) {
                    if (feWorm == worm) {
                        thisWormLockedFood = true;
                        continue;
                    }

                    // feWorm != worm
                    if (GetNearestFoodDistance(feWorm.Location) < feWormDistance) {
                        continue;
                    }

                    // getNearestFoodDistance(feWorm.Location) == feWormDistance
                    if (!thisWormLockedFood) {
                        return true;
                    }
                }
            }
            return false;
        }

        private int GetNearestFoodDistance(Point fromLocation) {
            var sortedFood = GetSortedFood(fromLocation);
            if (sortedFood.Count > 0) {
                return sortedFood[0].DistanceTo(fromLocation);
            }

            // unexpected
            return -1;
        }

        private Direction NextMultiplyDirection(AdvancedWorm worm, List<AdvancedWorm> allWorms) {
            var list = new List<int> {0, 1, 2, 3};
            list.Shuffle();
            foreach (var i in list) {
                var direction = (Direction)i;
                if (IsDirectionAllowedToMultiply(direction, worm, allWorms)) {
                    return direction;
                }
            }

            throw new Exception();
        }

        private static bool IsDirectionAllowedToMove (Direction direction, AdvancedWorm worm, IEnumerable<AdvancedWorm> allWorms) {
            var nextWormLocation = Utility.GetNextLocation(direction, worm.Location);
            return allWorms.All(w => worm == w || !nextWormLocation.Equals(w.Location));
        }
        
        private bool IsDirectionAllowedToMultiply (Direction direction, AdvancedWorm worm, IEnumerable<AdvancedWorm> allWorms) {
            var nextWormLocation = Utility.GetNextLocation(direction, worm.Location);
            return allWorms.All(w => !nextWormLocation.Equals(w.Location)) &&
                   !_food.ContainsKey(nextWormLocation);
        }

        private static Action NextAction (AdvancedWorm worm, int step) {
            var stepsLeft = 100 - step;
            if (stepsLeft <= worm.Health / AdvancedWorm.HealthRequiredToMultiply) {
                return Action.Multiply;
            }
            return worm.Health >= AdvancedWorm.HealthRequiredToMultiply * 5 / 2 ? Action.Multiply : Action.Move;
        }
        
        private List<Point> GetSortedFood(Point fromPoint) {
            var result = new List<Point>(_food.Keys);
            result.Sort((p1, p2) => fromPoint.DistanceTo(p1) - fromPoint.DistanceTo(p2));
            return result;
        }
    }
}