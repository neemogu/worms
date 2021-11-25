using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WormsAdvanced;
using WormsBasic;

namespace WormsDIContainers {
    class WormsWorldProgram {
        static void Main(string[] args) {
            IHost host = CreateHostBuilder(args).Build();
            host.Run();
            host.StopAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => 
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) => {
                    services.AddHostedService<WorldService>()
                        .AddSingleton<FoodGenerator>(new FoodGenerator(0, 5))
                        .AddSingleton<NameGenerator>(new NameGenerator())
                        .AddSingleton<FoodContainer>(sp => new FoodContainer(sp.GetService<FoodGenerator>()))
                        .AddSingleton<IWormStrategy, WormMultiplyingStrategy>(sp =>
                            new WormMultiplyingStrategy(sp.GetService<FoodContainer>()))
                        .AddSingleton<Logger>(new Logger())
                        .AddSingleton<IWorld, AdvancedWorld>(sp => new AdvancedWorld(100,
                            sp.GetService<IWormStrategy>(),
                            sp.GetService<NameGenerator>(), sp.GetService<FoodContainer>(), sp.GetService<Logger>()));
                });
    }
}