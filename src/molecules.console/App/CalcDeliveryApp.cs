using molecules.core.services;

namespace molecules.console.App
{
    public class CalcDeliveryApp
    {
        #region dependencies

        private readonly ICalcDeliveryService _calcDeliveryService;

        #endregion

        public CalcDeliveryApp(ICalcDeliveryService calcDeliveryService)
        {
            _calcDeliveryService = calcDeliveryService;
        }


        public async Task RunAsync(string baseDirectory)
        {
            Console.WriteLine("Welcome to CalcDeliveryApp App!");
            Console.WriteLine("Files will be processed from {0}", baseDirectory);
            Console.WriteLine("Press 0 to return to main menu");
            Console.WriteLine("Press 1 to produce input files");
            Console.WriteLine("Press 2 to parse output files");
            Console.Write("Your choice : ");
            var userInput = Console.ReadLine();
            if (int.TryParse(userInput, out int option))
            {
                switch (option)
                {
                    case 0:
                        Console.WriteLine("Exiting...");
                        break;
                    case 1:
                        await _calcDeliveryService.ExportCalcDeliveryInputAsync(baseDirectory);
                        break;
                    case 2:
                        await _calcDeliveryService.ImportCalcDeliveryOutputAsync(baseDirectory);
                        break;
                    default:
                        Console.WriteLine($"{userInput} is an invalid option.");
                        Console.WriteLine("The application will return to main menu.");
                        Console.Write("Press any key to proceed :");
                        Console.ReadKey();
                        Console.WriteLine();
                        break;
                }
            }
        }
    }
}
