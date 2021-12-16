using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WormsAdvanced;

namespace WormStrategyAPI {
    public static class WormDTOMapper {
        public static AdvancedWorm FromDTOToEntity(WormDTO wormDto) {
            return new AdvancedWorm(wormDto.Name, wormDto.Position, wormDto.LifeStrength);
        }
        
        public static List<AdvancedWorm> FromDTOListToEntityList(IEnumerable<WormDTO> wormDtos) {
            return wormDtos.Select(FromDTOToEntity).ToList();
        }
        
        public static WormDTO FromEntityToDTO(AdvancedWorm worm) {
            return new WormDTO {Name = worm.Name, Position = worm.Location, LifeStrength = worm.Health};
        }
        
        public static IEnumerable<WormDTO> FromEntityListToDTOList(List<AdvancedWorm> worms) {
            return worms.Select(FromEntityToDTO).ToList();
        }
    }
}