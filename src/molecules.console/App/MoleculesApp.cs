using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace molecules.console.App
{
    public class MoleculesApp : BackgroundService
    {
        private readonly ILogger<MoleculesApp> _logger;

        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public MoleculesApp(ILogger<MoleculesApp> logger, IHostApplicationLifetime hostApplicationLifetime)
        {
            _logger = logger;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("MoleculesApp running at: {time}", DateTimeOffset.Now);
            Console.WriteLine("Welcome to molecules App!");
            while (!stoppingToken.IsCancellationRequested) {
                Console.WriteLine("Press 0 to exit");
                Console.WriteLine("Press 1 to start processing orders");
                var result = Console.ReadLine();
                if ( int.TryParse(result, out int option) ) {
                    if ( option == 0) {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                await Task.Delay(1000, stoppingToken);
            }
            _hostApplicationLifetime.StopApplication();
        }
    }
}
