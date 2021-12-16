using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WormsAdvanced;
using WormStrategyAPI;

namespace WormsStrategyServer.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class StrategyController : ControllerBase {

        private readonly ILogger<StrategyController> _logger;

        public StrategyController(ILogger<StrategyController> logger) {
            _logger = logger;
        }

        [HttpPost("{wormName}/getAction"), Produces("application/json"), Consumes("application/json")]
        public WormActionDTO GetNextAction(string wormName, int step, int run, [FromBody] WormsWorldStateDTO worldState) {
            _logger.Log(LogLevel.Information, "Requested action for step {}, run {}, worm {}, world: {}", step, run, wormName, JsonConvert.SerializeObject(worldState));
            var strategy = new WormsOptimalStrategy(FoodDTOMapper.FromDTOToMap(worldState.Food));
            var allWorms = WormDTOMapper.FromDTOListToEntityList(worldState.Worms);
            var worm = allWorms.Find((w) => w.Name.Equals(wormName));
            if (worm == null) {
                StatusCode(400);
                return new WormActionDTO();
            }
            return ActionDTOMapper.GetDTOFromEntity(strategy.NextAction(worm, allWorms, step, run));
        }
    }
}