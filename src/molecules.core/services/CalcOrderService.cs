﻿using Microsoft.Extensions.Logging;
using molecule.infrastructure.data.interfaces.DbEntities;
using molecule.infrastructure.data.interfaces.Repositories;
using molecules.core.aggregates;
using molecules.core.services.validators.servicehelpers;
using molecules.core.valueobjects.CalcOrder;

namespace molecules.core.services
{
    /// <inheritdoc/>
    public class CalcOrderService : ICalcOrderService
    {
        #region dependencies

        private readonly ILogger<CalcOrderService> _logger;

        private readonly ICalcOrderServiceValidations _validations;

        private readonly ICalcOrderRepository _calcOrderRepository;

        #endregion

        /// <summary>
        /// Construct the CalcOrderService
        /// </summary>
        /// <param name="validations">The validation service helper</param>
        /// <param name="logger">The logger</param>
        public CalcOrderService(ICalcOrderServiceValidations validations,
                                    ICalcOrderRepository calcOrderRepository,
                                    ILogger<CalcOrderService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _validations = validations ?? throw new ArgumentNullException(nameof(validations));
            _calcOrderRepository = calcOrderRepository ?? throw new ArgumentNullException(nameof(calcOrderRepository));
        }

        /// <inheritdoc/>
        public async Task<CalcOrder> CreateAsync(CreateCalcOrder createCalcOrder)
        {
            if (createCalcOrder == null)
                throw new ArgumentNullException(nameof(createCalcOrder));
            
            _logger.LogInformation("Create a new CalcOrder called with name {0} and description {1}",
                                                createCalcOrder.Name,
                                                createCalcOrder.Description);
            _validations.Validate(createCalcOrder);
            var result = await _calcOrderRepository.CreateAsync(new CalcOrderDbEntity()
            {
                Name = createCalcOrder.Name,
                Description = createCalcOrder.Description,
                CustomerName = "Default",

            });
            await _calcOrderRepository.SaveChangesAsync();
            return new CalcOrder(result.Id, result.Name, result.Description);
        }

        /// <inheritdoc/>
        public async Task<CalcOrder?> UpdateAsync(int id, UpdateCalcOrder updateCalcOrder)
        {
            if (updateCalcOrder == null)
                throw new ArgumentNullException(nameof(updateCalcOrder));

            _logger.LogInformation("Update CalcOrder with id: {0} set Name: {1} and set Description: {2}",
                                    id, updateCalcOrder.Name, updateCalcOrder.Description);
            _validations.Validate(updateCalcOrder);
            var result = await _calcOrderRepository.UpdateAsync(id, updateCalcOrder.Name, updateCalcOrder.Description);
            await _calcOrderRepository.SaveChangesAsync();
            return new CalcOrder(result.Id, result.Name, result.Description);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("DeleteAsync with id {0}", id);

            await _calcOrderRepository.DeleteAsync(id);

            await _calcOrderRepository.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<CalcOrder> GetAsync(int id)
        {
            _logger.LogInformation("GetAsync with id {0}", id);

            var result = await _calcOrderRepository.GetByIdAsync(id);
            
            return new CalcOrder(result.Id, result.Name, result.Description);
        }

        /// <inheritdoc/>
        public async Task<IList<CalcOrder>> GetByNameAsync(string name)
        {
            _logger.LogInformation("GetByNameAsync with name {0}", name);

            var result = await _calcOrderRepository.GetByNameAsync(name);

            return result.ConvertAll(i => new CalcOrder(i.Id, i.Name, i.Description));
        }

        /// <inheritdoc/>
        public async Task<IList<CalcOrder>> ListAsync()
        {
            _logger.LogInformation("ListAsync");

            var result = await _calcOrderRepository.GetAllAsync();
            
            return result.ConvertAll(i => new CalcOrder(i.Id, i.Name, i.Description));
        }


    }
}
