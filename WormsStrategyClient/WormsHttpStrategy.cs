using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WormsAdvanced;
using WormsBasic;
using WormStrategyAPI;

namespace WormsStrategyClient {
    public class WormsHttpStrategy : IWormStrategy<AdvancedWorm> {
        
        private readonly FoodContainer _foodContainer;

        private readonly string _baseUrl;

        public WormsHttpStrategy(FoodContainer foodContainer, string baseUrl) {
            _foodContainer = foodContainer;
            _baseUrl = baseUrl;
        }
        
        public WormAction NextAction(AdvancedWorm worm, List<AdvancedWorm> allWorms, int step, int run) {
            var dto = new WormsWorldStateDTO { Food = FoodDTOMapper.FromMapToDTO(_foodContainer.Food),
                Worms = WormDTOMapper.FromEntityListToDTOList(allWorms)};
            var wormName = worm.Name;
            var task = Task.Run(async () => await GetNextActionFromServer(dto, wormName, step, run));
            task.Wait();
            return ActionDTOMapper.GetEntityFromDTO(task.Result);
        }

        private async Task<WormActionDTO> GetNextActionFromServer(WormsWorldStateDTO dto, string wormName, int step, int run) {
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            var url = _baseUrl + "/strategy/" + wormName + "/getAction?step=" + step + "&run=" + run;
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient httpClient = new (clientHandler);
            httpClient.BaseAddress = new Uri(url);
            var content = new StringContent(
                JsonSerializer.Serialize(dto, jsonOptions), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("", content);
            using (var stream = await response.Content.ReadAsStreamAsync()) {
                return await JsonSerializer.DeserializeAsync<WormActionDTO>(stream, jsonOptions);
            }
        }
    }
}