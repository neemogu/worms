using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WormsAdvanced;
using WormsBasic;
using Point = System.Drawing.Point;

namespace WormsDIContainers {
    public class WorldService : IHostedService {
        private const int TurnsNumber = 100;
        private readonly IServiceScopeFactory _scopeFactory;
        
        private readonly List<AdvancedWorm> _worms = new();
        private readonly IDictionary<Point, Food> _food = new Dictionary<Point, Food>();

        private void RunAsync() {
            using (IServiceScope scope = _scopeFactory.CreateScope()) {
                for (var i = 0; i < TurnsNumber; ++i) {
                    
                }
            }
        }
        
        public Task StartAsync(CancellationToken cancellationToken) {
            Task.Run(RunAsync);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            return Task.CompletedTask;
        }
    }
}