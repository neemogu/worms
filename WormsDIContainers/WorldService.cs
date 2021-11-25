using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WormsAdvanced;
using WormsBasic;

namespace WormsDIContainers {
    public class WorldService : IHostedService {
        private readonly IWorld _world;
        private readonly IHostApplicationLifetime _applicationLifetime;
 
        public WorldService(IWorld world, IHostApplicationLifetime applicationLifetime) {
            _world = world;
            _applicationLifetime = applicationLifetime;
        }

        private void RunAsync() {
            _world.AddWorm(new AdvancedWorm("John", new Point { X = 0, Y = 0 })); 
            _world.StartLife();
            _applicationLifetime.StopApplication();
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            Task.Run(RunAsync, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            return Task.CompletedTask;
        }
    }
}