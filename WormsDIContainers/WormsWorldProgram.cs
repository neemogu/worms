using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WormsAdvanced;
using WormsBasic;

namespace WormsDIContainers {
    class WormsWorldProgram {
        static void Main(string[] args) {
            IHost host = CreateHostBuilder(args).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => 
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) => {
                    services.AddHostedService<WorldService>()
                        .AddSingleton<IWorld, AdvancedWorld>()
                        .AddSingleton<IWormStrategy, WormMultiplyingStrategy>()
                        .AddSingleton<FoodContainer>()
                        .AddSingleton<FoodGenerator>(new FoodGenerator(0, 5))
                        .AddSingleton<NameGenerator>()
                        .AddSingleton<Logger>();

                });
    }
}