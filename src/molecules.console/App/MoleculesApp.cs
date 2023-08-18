using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using molecules.console.Constants;

namespace molecules.console.App
{
    public class MoleculesApp : BackgroundService
    {
        #region dependencies

        private readonly ILogger<MoleculesApp> _logger;

        private readonly IConfiguration _configuration;

        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        private readonly CalcDeliveryApp _calcDeliveryApp;

        private readonly CalcConversionApp _calcConversionApp;

        private readonly MoleculeReportApp _moleculeReportApp;

        #endregion

        public MoleculesApp(
            CalcDeliveryApp calcDeliveryApp,
            CalcConversionApp calcConversionApp,
            MoleculeReportApp moleculeReportApp,
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
                Console.WriteLine("Press 0 to exit");

                foreach(var enumItem in Enum.GetValues<AppName>())
                {
                    Console.WriteLine("Press {0} for {1}", (int)enumItem, enumItem.ToString());
                }

                Console.Write(":");

                var command = Console.ReadLine();

                if (int.TryParse(command, out int option))
                {
                    if (option == 0)
                    {
                        return AppName.Default;
                    }
                    else if (Enum.IsDefined(typeof(AppName), option))
                    {
                        result = (AppName)option;
                    }
                }
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
                        case AppName.Default:
                            Console.WriteLine("Exiting...");
                            done = true;
                            return;
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
                        default:
                            Console.WriteLine($"Invalid option: {app}");
                            done = true;
                            return;
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
