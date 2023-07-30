using Microsoft.Extensions.Logging;
using molecules.core.factories.GmsParsers;
using molecules.core.valueobjects.Molecules;

namespace molecules.core.services
{
    public class CalcFileConversionService : ICalcFileConversionService
    {
        #region dependencies

        private ILogger<CalcFileConversionService> _Logger;

        #endregion

        public CalcFileConversionService(ILogger<CalcFileConversionService> logger)
        {
            _Logger = logger;
        }

        public void ConvertGeoOptFileToXyzFileAsync(string basePath)
        {
            _Logger.LogInformation("ConvertMoleculeToXyzFileAsync basePath {0}", basePath);
            foreach (var item in Directory.EnumerateFiles(Path.Combine(basePath, "Conversion"), "*geoopt*.log", SearchOption.AllDirectories))
            {
                Molecule molecule = new Molecule();              
                GeoOptParser.Parse(File.ReadAllLines(item).ToList(), molecule);
                File.WriteAllText(Path.Combine(basePath, "Conversion", $"{Path.GetFileNameWithoutExtension(item)}.xyz"),
                                        Molecule.GetXyzFileData(molecule));
            }
        }

        public void ConvertMoleculeToXyzFileAsync(string basePath)
        {
            _Logger.LogInformation("ConvertMoleculeToXyzFileAsync basePath {0}", basePath);
            foreach (var item in Directory.EnumerateFiles(Path.Combine(basePath, "Molecules"), "*.json", SearchOption.AllDirectories))
            {
                string result = File.ReadAllText(item);
                var molecule = Molecule.DeserializeFromJsonString(result);
                if (molecule != null)
                {
                    var xyzFileData = Molecule.GetXyzFileData(molecule);
                    File.WriteAllText(Path.Combine(basePath, "Molecules", $"{molecule.Name}.xyz"), xyzFileData);
                }
            }
        }
    }
}
