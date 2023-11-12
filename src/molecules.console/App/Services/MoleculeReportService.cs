using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using molecules.console.Constants;
using molecules.core.services.Reporting;
using molecules.shared;

namespace molecules.console.App.Services
{
    public class MoleculeReportService
    {

        #region dependencies

        private readonly ILogger<IMoleculeReportService> _logger;

        private readonly IMoleculeReportService _moleculeReportService;

        private readonly IConfiguration _configuration;

        #endregion

        public MoleculeReportService(IMoleculeReportService moleculeReportService,
                                    IConfiguration configuration,
                                        ILogger<IMoleculeReportService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _moleculeReportService = moleculeReportService ?? throw new ArgumentNullException(nameof(moleculeReportService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration)); ;
        }


        public async Task RunAsync()
        {
            switch (GetReportType())
            {
                case ReportName.Population:
                    var populationReport = await _moleculeReportService.GetMoleculePopulationReportAsync(GetMoleculeId());
                    Console.Write(StringConversion.ToJsonString(populationReport));
                    break;
                case ReportName.Bonds:
                    var bondsReport = await _moleculeReportService.GetMoleculeBondsReportsAsync(GetMoleculeId());
                    Console.Write(StringConversion.ToJsonString(bondsReport));
                    break;
                case ReportName.Charge:
                    var chargeReport = await _moleculeReportService.GetMoleculeAtomsChargeReportAsync(GetMoleculeId());
                    Console.Write(StringConversion.ToJsonString(chargeReport));
                    break;
                case ReportName.AtomOrbital:
                    var atomOrbitalReport = await _moleculeReportService.GetMoleculeAtomOrbitalReportAsync(GetMoleculeId());
                    Console.Write(StringConversion.ToJsonString(atomOrbitalReport));
                    break;
                case ReportName.None:
                default:
                    break;
            }
        }


        private int GetMoleculeId()
        {
            if (!int.TryParse(_configuration["MoleculeId"], out int result))
            {
                _logger.LogError("Invalid MoleculeId");
                return -1;
            }
            return result;
        }

        private ReportName GetReportType()
        {
            if (!Enum.TryParse(_configuration["ReportType"], true, out ReportName result))
            {
                return ReportName.None;
            }
            return result;
        }


    }
}
