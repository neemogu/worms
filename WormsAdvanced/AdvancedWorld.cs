using System;
using System.Collections.Generic;
using System.Linq;
using WormsBasic;
using Action = WormsBasic.Action;

namespace WormsAdvanced {
    public class AdvancedWorld: IWorld {
        private readonly int _turnsNumber = 100;
        private IWormStrategy<AdvancedWorm> _strategy;
        private readonly NameGenerator _nameGenerator;
        private readonly IFoodContainer _foodContainer;
        private readonly Logger _logger;

        public AdvancedWorld(IWormStrategy<AdvancedWorm> strategy, NameGenerator nameGenerator,
            IFoodContainer foodContainer, Logger logger) {
            _strategy = strategy;
            _nameGenerator = nameGenerator;
            _foodContainer = foodContainer;
            _logger = logger;
        }
        
        public AdvancedWorld(IWormStrategy<AdvancedWorm> strategy, IFoodContainer foodContainer): this(strategy,
            new NameGenerator(), foodContainer, new Logger()) {}

        private readonly List<AdvancedWorm> _worms = new();
        private readonly ISet<string> _wormsNames = new HashSet<string>();

        public void AddWorm(Worm worm) {
            if (worm is AdvancedWorm advancedWorm) {
                _worms.Add(advancedWorm);
                _wormsNames.Add(advancedWorm.Name);
            }
        }

        public void StartLife() {
            for (var i = 0; i < _turnsNumber; ++i) {
                NextTurn(i + 1);
            }
        }
        
        public void NextTurn(int turn) {
            _foodContainer.NextTurn();
            _logger.PrintWorldState(turn, _worms, _foodContainer);
            ProcessWorms(turn);
        }

        // for tests
        public AdvancedWorm[] GetAllWorms() {
            return _worms.ToArray();
        }

        private void ProcessWorms(int turn) {
            for (var i = _worms.Count - 1; i >= 0; --i) {
                var worm = _worms[i];
                if (worm.Health <= 0) {
                    _worms.RemoveAt(i);
                    _wormsNames.Remove(worm.Name);
                    continue;
                }
                _foodContainer.CheckForFoodAndEat(worm);

                var nextAction = _strategy.NextAction(worm, _worms, turn, 1);
                
                var nextDirection = nextAction.Direction;
                var nextCoord = Utility.GetNextLocation(nextDirection, worm.Location);
                // if there are no worms in the next coord
                if (_worms.All(worm1 => worm1 == worm || !nextCoord.Equals(worm1.Location))) {
                    // is there is a food in the next coord
                    switch (_foodContainer.HasFoodIn(nextCoord)) {
                        case false: {
                            switch (nextAction.Action) {
                                case Action.Move:
                                    worm.Move(nextDirection);
                                    break;
                                case Action.Multiply:
                                    var newWorm = worm.TryMultiply(nextCoord, _nameGenerator.Generate(_wormsNames));
                                    if (newWorm != null) {
                                        AddWorm(newWorm);
                                    }
                                    break;
                            }
                            break;
                        }
                        case true when nextAction.Action == Action.Move:
                            _foodContainer.CheckForFoodAndEat(worm, nextCoord);
                            worm.Move(nextDirection);
                            break;
                    }
                }
                worm.Live();
            }
        }
    }
}