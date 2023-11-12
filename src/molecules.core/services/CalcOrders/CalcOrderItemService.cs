using Microsoft.Extensions.Logging;
using molecule.infrastructure.data.interfaces.DbEntities;
using molecule.infrastructure.data.interfaces.Repositories;
using molecules.core.aggregates;
using molecules.core.Factories;
using molecules.core.services.validators.servicehelpers;
using molecules.core.valueobjects.CalcOrderItem;

namespace molecules.core.services.CalcOrders
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
                                                    ILogger<CalcOrderItemService> logger)
        {
            _calcOrderItemFactory = calcOrderItemFactory ?? throw new ArgumentNullException(nameof(calcOrderItemFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _calcOrderItemRepository = calcOrderItemRepository ?? throw new ArgumentNullException(nameof(calcOrderItemRepository));
            _calcOrderItemServiceValidations = calcOrderItemServiceValidations ?? throw new ArgumentNullException(nameof(calcOrderItemServiceValidations));
        }

        // <inheritdoc />
        public async Task<CalcOrderItem> CreateAsync(int calcOrderId, CreateCalcOrderItem createCalcOrderItem)
        {
            _logger.LogInformation("CreateAsync for calcOrderId {0} and molecule {1} with details {3} was called ",
                            calcOrderId,
                            createCalcOrderItem.MoleculeName,
                            createCalcOrderItem.Details);

            _calcOrderItemServiceValidations.Validate(createCalcOrderItem);

            var result = await _calcOrderItemRepository.CreateAsync(new CalcOrderItemDbEntity()
            {
                CalcOrderId = calcOrderId,
                MoleculeName = createCalcOrderItem.MoleculeName,
                XYZ = createCalcOrderItem.Details.XYZ,
                Charge = createCalcOrderItem.Details.Charge,
                CalcType = createCalcOrderItem.Details.Type.ToString(),
                BasissetCode = createCalcOrderItem.Details.BasisSetCode.ToString()
            });

            await _calcOrderItemRepository.SaveChangesAsync();

            return _calcOrderItemFactory.CreateCalcOrderItem(result);
        }

        // <inheritdoc />
        public async Task<CalcOrderItem> UpdateAsync(int id, UpdateCalcOrderItem updateCalcOrderItem)
        {
            _logger.LogInformation("UpdateAsync with id {0}", id);

            _calcOrderItemServiceValidations.Validate(updateCalcOrderItem);

            var result = await _calcOrderItemRepository.UpdateAsync(id, updateCalcOrderItem.Details.Charge,
                                                updateCalcOrderItem.MoleculeName, updateCalcOrderItem.Details.Type.ToString(),
                                                updateCalcOrderItem.Details.BasisSetCode.ToString(), updateCalcOrderItem.Details.XYZ);

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
