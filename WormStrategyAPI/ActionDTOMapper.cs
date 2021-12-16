using System;
using WormsBasic;
using Action = WormsBasic.Action;

namespace WormStrategyAPI {
    public static class ActionDTOMapper {
        public static WormActionDTO GetDTOFromEntity(WormAction action) {
            return new WormActionDTO {
                Direction = action.Direction.ToString(),
                Split = action.Action == Action.Multiply
            };
        }

        public static WormAction GetEntityFromDTO(WormActionDTO dto) {
            return new WormAction { Action = dto.Split ? Action.Multiply : Action.Move, Direction = Enum.Parse<Direction>(dto.Direction)};
        }
    }
}