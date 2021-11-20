using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WormsDIContainers {
    class WormsWorldProgram {
        static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => 
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) => {
                    services.AddHostedService<WorldService>()
                        .AddScoped<FoodGeneratorService>();
                });
    }
}