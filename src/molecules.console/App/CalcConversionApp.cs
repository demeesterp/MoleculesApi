using molecules.console.MoleculesLegacy;
using molecules.core.services;

namespace molecules.console.App
{
    public class CalcConversionApp
    {
        #region dependencies

        private readonly ICalcFileConversionService _calcFileConversionService;

        #endregion

        public CalcConversionApp(ICalcFileConversionService calcFileConversionService)
        {
            _calcFileConversionService = calcFileConversionService ?? throw new ArgumentNullException(nameof(calcFileConversionService));
        }

        public void Run(string baseDirectory)
        {
            Console.WriteLine("Welcome to ConversionApp App!");
            Console.WriteLine("Files will be processed from {0}", baseDirectory);
            Console.WriteLine("Press 0 to exit ConversionApp");
            Console.WriteLine("Press 1 to parse geoOpt files from the Conversion folder and produde xyz files");
            Console.WriteLine("Press 2 to parse legacy Molecule.json files from the Moleculesand produce xyz files");
            Console.Write("Select : ");
            var command = Console.ReadLine();
            if (int.TryParse(command, out int option))
            {
                switch (option)
                {
                    case 0:
                        Console.WriteLine("Exiting...");
                        break;
                    case 1:
                        _calcFileConversionService.ConvertGeoOptFileToXyzFileAsync(baseDirectory);
                        break;
                    case 2:
                        _calcFileConversionService.ConvertMoleculeToXyzFileAsync(baseDirectory);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

    }
}
