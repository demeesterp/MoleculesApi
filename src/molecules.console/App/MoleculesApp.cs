using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using molecules.console.MoleculesLegacy;
using molecules.core.services;

namespace molecules.console.App
{
    public class MoleculesApp : BackgroundService
    {
        private readonly ILogger<MoleculesApp> _logger;

        private readonly IConfiguration _configuration;

        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        private readonly ICalcDeliveryService _calcDeliveryService;

        private readonly ICalcFileConversionService _calcFileConversionService;

        public MoleculesApp(ICalcDeliveryService calcDeliveryService,
            ICalcFileConversionService calcFileConversionService,
                                IConfiguration configuration,
                                    ILogger<MoleculesApp> logger,
                                        IHostApplicationLifetime hostApplicationLifetime)
        {
            _logger = logger;
            _configuration = configuration;
            _hostApplicationLifetime = hostApplicationLifetime;
            _calcDeliveryService = calcDeliveryService;
            _calcFileConversionService = calcFileConversionService;
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
                    Console.WriteLine("Press 3 to convert geoopt files to xyz files");
                    Console.WriteLine("Press 4 to convert legacy molecule files to xyz files");
                    var command = Console.ReadLine();
                    if (int.TryParse(command, out int option))
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
                        else if (option == 3)
                        {
                            _calcFileConversionService.ConvertGeoOptFileToXyzFileAsync(basePath);
                        }
                        else if (option == 4)
                        {
                            foreach (var item in Directory.EnumerateFiles(Path.Combine(basePath, "Molecules"), "*.json", SearchOption.AllDirectories))
                            {
                                string result = File.ReadAllText(item);
                                var molecule = Molecule.DeserializeFromJsonString(result);
                                if (molecule != null)
                                {
                                    var xyzFileData = Molecule.GetXyzFileData(molecule);
                                    File.WriteAllText(Path.Combine(basePath, "Molecules", $"{molecule.NameInfo}.xyz"), xyzFileData);
                                }
                            }
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
