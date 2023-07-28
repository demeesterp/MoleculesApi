using Microsoft.Extensions.Logging;
using molecule.infrastructure.data.interfaces.DbEntities;
using molecule.infrastructure.data.interfaces.Repositories;
using molecules.core.aggregates;
using molecules.core.Factories;
using molecules.core.services.validators.servicehelpers;
using molecules.core.valueobjects.CalcOrderItem;

namespace molecules.core.services
{
    public class CalcOrderItemService : ICalcOrderItemService
    {
        #region dependencies

        private readonly ICalcOrderItemFactory _calcOrderItemFactory;

        private readonly ICalcOrderItemRepository _calcOrderItemRepository;

        private readonly ICalcOrderItemServiceValidations _calcOrderItemServiceValidations;

        private readonly ILogger<CalcOrderItemService> _logger;

        #endregion

        public CalcOrderItemService(ICalcOrderItemRepository calcOrderItemRepository,
                                                ICalcOrderItemFactory calcOrderItemFactory,
                                                ICalcOrderItemServiceValidations calcOrderItemServiceValidations,
                                                    ILogger<CalcOrderItemService> logger )
        {
            _calcOrderItemFactory               = calcOrderItemFactory ?? throw new ArgumentNullException(nameof(calcOrderItemFactory));
            _logger                             = logger ?? throw new ArgumentNullException(nameof(logger));
            _calcOrderItemRepository            = calcOrderItemRepository ?? throw new ArgumentNullException(nameof(calcOrderItemRepository));
            _calcOrderItemServiceValidations    = calcOrderItemServiceValidations ?? throw new ArgumentNullException(nameof(calcOrderItemServiceValidations));
        }

        // <inheritdoc />
        public async Task<CalcOrderItem> CreateAsync(int calcOrderId, CreateCalcOrderItem calcOrderItem)
        {
            _logger.LogInformation("CreateAsync for calcOrderId {0} and molecule {1} with details {3} was called ", 
                            calcOrderId,
                            calcOrderItem.MoleculeName,
                            calcOrderItem.Details);

            _calcOrderItemServiceValidations.Validate(calcOrderItem);

            var result = await _calcOrderItemRepository.CreateAsync(new CalcOrderItemDbEntity()
            {
                CalcOrderId = calcOrderId,
                MoleculeName = calcOrderItem.MoleculeName,
                XYZ = calcOrderItem.Details.XYZ,
                Charge = calcOrderItem.Details.Charge,
                CalcType = calcOrderItem.Details.Type.ToString(),
                BasissetCode = calcOrderItem.Details.BasisSetCode.ToString()
            });

            await _calcOrderItemRepository.SaveChangesAsync();

            return _calcOrderItemFactory.CreateCalcOrderItem(result);
        }

        // <inheritdoc />
        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("DeleteAsync with id {0}", id);

            await _calcOrderItemRepository.DeleteAsync(id);

            await _calcOrderItemRepository.SaveChangesAsync();
        }
    }
}
