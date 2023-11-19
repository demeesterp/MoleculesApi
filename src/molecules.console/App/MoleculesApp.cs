using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using molecules.console.App.Services;
using molecules.console.Constants;

namespace molecules.console.App
{
    public class MoleculesApp : BackgroundService
    {
        #region dependencies

        private readonly ILogger<MoleculesApp>          _logger;

        private readonly IConfiguration                 _configuration;

        private readonly IHostApplicationLifetime       _hostApplicationLifetime;

        private readonly CalcDeliveryServices           _calcDeliveryApp;

        private readonly CalcConversionService          _calcConversionApp;

        private readonly MoleculeReportService          _moleculeReportApp;

        #endregion

        public MoleculesApp(CalcDeliveryServices calcDeliveryApp,
                                CalcConversionService calcConversionApp,
                                    MoleculeReportService moleculeReportApp,
                                        IConfiguration configuration,
                                            ILogger<MoleculesApp> logger,
                                                IHostApplicationLifetime hostApplicationLifetime)
        {
            _logger = logger;
            _configuration = configuration;
            _calcDeliveryApp = calcDeliveryApp;
            _calcConversionApp = calcConversionApp;
            _moleculeReportApp = moleculeReportApp;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        private AppName GetApp()
        {
            if ( !Enum.TryParse(_configuration["appName"], true, out AppName result))
            {
                foreach(var enumItem in Enum.GetValues<AppName>())
                {
                    Console.WriteLine("For {0} press {1}", enumItem.ToString(), (int)enumItem);
                }
                Console.Write("Your choice:");
                var userInput = Console.ReadLine();
                bool invalidInput = !int.TryParse(userInput, out int option) || !Enum.IsDefined(typeof(AppName), option);
                while (invalidInput)
                {
                    Console.WriteLine($"\"{userInput}\" is an invalid choice !");
                    Console.WriteLine("Try again or press 0 to exit.");
                    Console.Write("Your choice:");
                    userInput = Console.ReadLine();
                    invalidInput = !int.TryParse(userInput, out option) || !Enum.IsDefined(typeof(AppName), option);
                }
                result = (AppName)option;
            }
            return result;
        }

        private string GetBasePath()
        {
            return _configuration["basePath"] ?? Directory.GetCurrentDirectory();
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("MoleculesApp running at: {time}", DateTimeOffset.Now);
            try
            {
                bool done = false; 
                while (!stoppingToken.IsCancellationRequested && !done)
                {
                    AppName app = GetApp();
                    switch (app)
                    {
                        case AppName.CalcDeliveryApp:
                            // Run Calculation Delivery : Generate molecules in DB
                            await _calcDeliveryApp.RunAsync(GetBasePath());
                            continue;
                        case AppName.ConversionApp:
                            // Do some custom conversions
                            _calcConversionApp.Run(GetBasePath());
                            break;
                        case AppName.MoleculeReportApp:
                            // Write reports to consumers
                            await _moleculeReportApp.RunAsync();
                            done = true;
                            return;
                        case AppName.Exit:
                            done = true;
                            return;
                        default:
                            Console.WriteLine($"Unknow application: {app}");
                            done = true;
                            return;
                    }
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Something went wrong");
                Console.WriteLine("An error happend please request support or retry!");
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
