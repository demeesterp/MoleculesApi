using Microsoft.Extensions.Logging;
using molecules.core.aggregates;
using molecules.core.factories;
using molecules.core.valueobjects.GmsCalc.Output;
using System.Text;

namespace molecules.core.services
{
    public class CalcDeliveryService : ICalcDeliveryService
    {
        private readonly ICalcOrderService          _calcOrderService;

        private readonly ICalcDeliveryFactory       _calcDeliveryFactory;

        private readonly ILogger<CalcOrderService>  _logger;

        public CalcDeliveryService(ICalcOrderService calcOrderService,
                                    ICalcDeliveryFactory calcDeliveryFactory,
                                        ILogger<CalcOrderService> logger)
        {
            _calcOrderService = calcOrderService;
            _calcDeliveryFactory = calcDeliveryFactory;
            _logger = logger;
        }
        
        public async Task ExportCalcDeliveryInputAsync(string basePath)
        {        
            _logger.LogInformation($"ExportCalcDeliveryInputAsync basePath {basePath}");
            IList<CalcOrder> orders = await _calcOrderService.ListAsync();
            foreach(var input in _calcDeliveryFactory.BuildCalcInput(orders).Items)
            {
               await File.WriteAllTextAsync(Path.Combine(basePath, "Calculations", $"{input.DisplayName}.inp"), input.Content);
            };
        }

        public async Task ImportCalcDeliveryOutputAsync(string basePath)
        {
            _logger.LogInformation($"ImportCalcDeliveryOutputAsync basePath {basePath}");
            GmsCalcOutput result = new GmsCalcOutput();
            foreach (var file in Directory.EnumerateFiles(basePath, "*.log"))
            {
                result.AddItem(Path.GetFileNameWithoutExtension(file),
                                new StringBuilder(await File.ReadAllTextAsync(file)));
            }

            
        }
    }
}
