using Microsoft.Extensions.Logging;
using molecules.core.aggregates;
using molecules.core.factories;
using molecules.core.valueobjects.BasisSet;
using molecules.core.valueobjects.CalcOrderItem;
using molecules.core.valueobjects.GmsCalc;
using molecules.core.valueobjects.Molecules;

namespace molecules.core.services
{
    public class CalcDeliveryService : ICalcDeliveryService
    {
        private readonly ICalcOrderService          _calcOrderService;

        private readonly IGmsCalcInputFactory       _calcDeliveryInputFactory;

        private readonly IMoleculeFromGmsFactory    _gmsMoleculeFactory;

        private readonly ICalcMoleculeService       _calcMoleculeService;

        private readonly ILogger<CalcOrderService>  _logger;

        public CalcDeliveryService(ICalcOrderService calcOrderService,
                                    ICalcMoleculeService calcMoleculeService,
                                        IGmsCalcInputFactory calcDeliveryFactory,
                                        IMoleculeFromGmsFactory gmsMoleculeFactory,
                                            ILogger<CalcOrderService> logger)
        {
            _calcOrderService           = calcOrderService;
            _calcDeliveryInputFactory   = calcDeliveryFactory;
            _calcMoleculeService        = calcMoleculeService;
            _calcMoleculeService        = calcMoleculeService;
            _gmsMoleculeFactory         = gmsMoleculeFactory;
            _logger                     = logger;
        }
        
        public async Task ExportCalcDeliveryInputAsync(string basePath)
        {        
            _logger.LogInformation($"ExportCalcDeliveryInputAsync basePath {basePath}");
            List<CalcOrder> orders = await _calcOrderService.ListAsync();
            _calcDeliveryInputFactory.BuildCalcInput(orders).ForEach(async (calcInput) => 
            {
                await File.WriteAllTextAsync(Path.Combine(basePath, "Calculations", $"{calcInput.DisplayName}.inp"), calcInput.Content);
            });
        }

        public async Task ImportCalcDeliveryOutputAsync(string basePath)
        {
            _logger.LogInformation($"ImportCalcDeliveryOutputAsync basePath {basePath}");
            List<CalcOrder> orders = await _calcOrderService.ListAsync();
            foreach(var order in orders)
            {
                foreach(var orderItem in order.Items)
                {
                    var basisSet = (CalcBasisSetTable.GetCalcBasisSet(orderItem.Details.BasisSetCode)?.Name) ?? string.Empty;

                    CalcMolecule? molecule = await _calcMoleculeService.FindAsync(order.Details.Name, basisSet, orderItem.MoleculeName);

                    if (molecule == null)
                    {
                        molecule = await _calcMoleculeService.CreateAsync(new CalcMolecule(order.Details.Name, basisSet, orderItem.MoleculeName)
                        {
                            Molecule = new Molecule(orderItem.Details) // Init with XYZ file
                            {
                                Name = orderItem.MoleculeName
                            }
                        });
                    }

                    if (molecule.Molecule == null)
                    {
                        molecule.Molecule = new Molecule(orderItem.Details)
                        {
                            Name = orderItem.MoleculeName
                        };
                    }

                    if (orderItem.Details.Type == CalcOrderItemType.AllKinds)
                    {

                        // Search for a geoOptFile
                        string geoOptFile = Path.Combine(basePath, "Calculations",
                                                OutFileDisplayName(order.Details.Name, orderItem.Id,
                                                                        molecule.MoleculeName, GmsCalculationKind.GeometryOptimization));
                        if (File.Exists(geoOptFile))
                        {
                            _gmsMoleculeFactory.TryCompleteMolecule(geoOptFile, File.ReadAllLines(geoOptFile).ToList(), molecule.Molecule);

                        }
                    }

                    // Parse GeoDiskCharge
                    string geoDiskFile = Path.Combine(basePath, "Calculations",
                                                        OutFileDisplayName(order.Details.Name, orderItem.Id,
                                                            molecule.MoleculeName, GmsCalculationKind.GeoDiskCharge));

                    if (File.Exists(geoDiskFile))
                    {
                        _gmsMoleculeFactory.TryCompleteMolecule(geoDiskFile, File.ReadAllLines(geoDiskFile).ToList(), molecule.Molecule);
                    }

                    // Parse CHelpGCharge
                    string CHelpGChargeFile = Path.Combine(basePath, "Calculations",
                                                                               OutFileDisplayName(order.Details.Name, orderItem.Id,
                                                                                 molecule.MoleculeName, GmsCalculationKind.CHelpGCharge));

                    if (File.Exists(CHelpGChargeFile))
                    {
                        _gmsMoleculeFactory.TryCompleteMolecule(CHelpGChargeFile, File.ReadAllLines(CHelpGChargeFile).ToList(), molecule.Molecule);
                    }


                    string fukuiNeutralFile = Path.Combine(basePath, "Calculations",
                                                             OutFileDisplayName(order.Details.Name, orderItem.Id,
                                                                                  molecule.MoleculeName, GmsCalculationKind.FukuiNeutral));

                    if (File.Exists(fukuiNeutralFile))
                    {
                        _gmsMoleculeFactory.TryCompleteMolecule(fukuiNeutralFile, File.ReadAllLines(fukuiNeutralFile).ToList(), molecule.Molecule);
                    }

                    string fukuiHOMOFile = Path.Combine(basePath, "Calculations",
                                                             OutFileDisplayName(order.Details.Name, orderItem.Id,
                                                                                  molecule.MoleculeName, GmsCalculationKind.FukuiHOMO));

                    if (File.Exists(fukuiHOMOFile))
                    {
                        _gmsMoleculeFactory.TryCompleteMolecule(fukuiHOMOFile, File.ReadAllLines(fukuiHOMOFile).ToList(), molecule.Molecule);
                    }

                    string fukuiLUMOFile = Path.Combine(basePath, "Calculations",
                                                             OutFileDisplayName(order.Details.Name, orderItem.Id,
                                                                                  molecule.MoleculeName, GmsCalculationKind.FukuiLUMO));

                    if (File.Exists(fukuiLUMOFile))
                    {
                        _gmsMoleculeFactory.TryCompleteMolecule(fukuiLUMOFile, File.ReadAllLines(fukuiLUMOFile).ToList(), molecule.Molecule);
                    }

                    await _calcMoleculeService.UpdateAsync(molecule.Id, molecule.Molecule);
                }
            }
        }

        private string OutFileDisplayName(string orderName, int orderItemId, string moleculeName, GmsCalculationKind kind)
        {
            return $"{orderName}_{orderItemId}_{moleculeName}_{kind}.log";
        }
    }
}
