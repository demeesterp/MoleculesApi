using Microsoft.Extensions.Logging;
using molecules.core.factories.Reports;
using molecules.core.services.CalcMolecules;
using molecules.core.valueobjects.MoleculeReport;

namespace molecules.core.services.Reporting
{
    public class MoleculeReportService : IMoleculeReportService
    {
        private readonly IMoleculeReportFactory _moleculeReportFactory;

        private readonly ICalcMoleculeService _calcMoleculeService;

        private readonly ILogger<MoleculeReportService> _logger;

        public MoleculeReportService(ICalcMoleculeService calcMoleculeService,
                                        IMoleculeReportFactory moleculeReportFactory,
                                            ILogger<MoleculeReportService> logger)
        {
            _moleculeReportFactory = moleculeReportFactory ?? throw new ArgumentNullException(nameof(moleculeReportFactory));
            _calcMoleculeService = calcMoleculeService ?? throw new ArgumentNullException(nameof(calcMoleculeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<GeneralMoleculeReport>> GetGeneralMoleculeReportsAsync(int moleculeId)
        {
            _logger.LogInformation($"GetGeneralMoleculeReportsAsync({moleculeId})");
            var molecule = await _calcMoleculeService.GetAsync(moleculeId);
            return _moleculeReportFactory.GetGeneralMoleculeReport(molecule?.Molecule);
        }

        public async Task<List<MoleculeAtomOrbitalReport>> GetMoleculeAtomOrbitalReportAsync(int moleculeId)
        {
            _logger.LogInformation($"GetMoleculeAtomOrbitalReportAsync({moleculeId})");
            var molecule = await _calcMoleculeService.GetAsync(moleculeId);
            return _moleculeReportFactory.GetMoleculeAtomOrbitalReport(molecule?.Molecule);
        }

        public async Task<List<MoleculeAtomsChargeReport>> GetMoleculeAtomsChargeReportAsync(int moleculeId)
        {
            _logger.LogInformation($"GetMoleculeAtomsChargeReportAsync({moleculeId})");
            var molecule = await _calcMoleculeService.GetAsync(moleculeId);
            return _moleculeReportFactory.GetMoleculeAtomsChargeReport(molecule?.Molecule);
        }

        public async Task<List<MoleculeBondsReport>> GetMoleculeBondsReportsAsync(int moleculeId)
        {
            _logger.LogInformation($"GetMoleculeBondsReportsAsync({moleculeId})");
            var molecule = await _calcMoleculeService.GetAsync(moleculeId);
            return _moleculeReportFactory.GetMoleculeBondsReports(molecule?.Molecule);
        }

        public async Task<List<MoleculeAtomsPopulationReport>> GetMoleculePopulationReportAsync(int moleculeId)
        {
            _logger.LogInformation($"GetMoleculePopulationReportAsync({moleculeId})");
            var molecule = await _calcMoleculeService.GetAsync(moleculeId);
            return _moleculeReportFactory.GetMoleculePopulationReport(molecule?.Molecule);
        }
    }
}
