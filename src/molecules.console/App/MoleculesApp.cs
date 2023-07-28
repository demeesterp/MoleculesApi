using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using molecules.core.services;

namespace molecules.console.App
{
    public class MoleculesApp : BackgroundService
    {
        private readonly ILogger<MoleculesApp> _logger;

        private readonly IConfiguration _configuration;

        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        private readonly ICalcDeliveryService _calcDeliveryService;

        public MoleculesApp(ICalcDeliveryService calcDeliveryService,
                                IConfiguration configuration,
                                    ILogger<MoleculesApp> logger,
                                        IHostApplicationLifetime hostApplicationLifetime)
        {
            _logger = logger;
            _configuration = configuration;
            _hostApplicationLifetime = hostApplicationLifetime;
            _calcDeliveryService = calcDeliveryService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("MoleculesApp running at: {time}", DateTimeOffset.Now);
            Console.WriteLine("Welcome to molecules App!");
            string basePath = _configuration["basePath"] ?? Directory.GetCurrentDirectory();
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    Console.WriteLine("Press 0 to exit");
                    Console.WriteLine("Press 1 to export calculation input files");
                    Console.WriteLine("Press 2 to import calculation output files");
                    var result = Console.ReadLine();
                    if (int.TryParse(result, out int option))
                    {
                        if (option == 0)
                        {
                            break;
                        }
                        else if (option == 1)
                        {
                            await _calcDeliveryService.ExportCalcDeliveryInputAsync(basePath);
                            break;
                        }
                        else if (option == 2)
                        {
                            await _calcDeliveryService.ImportCalcDeliveryOutputAsync(basePath);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Something went wrong");
                Console.WriteLine("An error happend please retry!");
                Console.WriteLine("Press any key!");
                Console.ReadLine();
            }
            finally
            {
               _hostApplicationLifetime.StopApplication();
            }
        }
    }
}
