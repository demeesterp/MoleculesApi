using Microsoft.Extensions.Logging;
using molecules.core.aggregates;
using molecules.core.valueobjects.Molecules;
using System.Runtime.InteropServices;

namespace molecules.core.services
{
    public class MoleculeFileService : IMoleculeFileService
    {

        private readonly ICalcMoleculeService _calcMoleculeService;

        private readonly ILogger<MoleculeFileService> _logger;

        public MoleculeFileService(ICalcMoleculeService calcMoleculeService, ILogger<MoleculeFileService> logger)
        {
            _calcMoleculeService = calcMoleculeService;
            _logger = logger;
        }


        public async Task<string> GetXyzFileContentAsync(int moleculeId)
        {
            _logger.LogInformation($"GetXyzFileContentAsync for molecule {moleculeId}");

            CalcMolecule result = await _calcMoleculeService.GetAsync(moleculeId);

            return Molecule.GetXyzFileData(result?.Molecule);
        }
    }
}
