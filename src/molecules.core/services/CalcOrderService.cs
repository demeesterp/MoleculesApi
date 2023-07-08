using Microsoft.Extensions.Logging;
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

        #endregion

        /// <summary>
        /// Construct the CalcOrderService
        /// </summary>
        /// <param name="validations">The validation service helper</param>
        /// <param name="logger">The logger</param>
        public CalcOrderService(ICalcOrderServiceValidations validations, ILogger<CalcOrderService> logger)
        {
            _validations = validations ?? throw new ArgumentNullException(nameof(validations));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<CalcOrder> CreateAsync(CreateCalcOrder createCalcOrder)
        {
            _logger.LogInformation("Create a new CalcOrder called {0}", createCalcOrder.Name);
            _validations.Validate(createCalcOrder);
            CalcOrder calcOrder = new CalcOrder(createCalcOrder.Name, createCalcOrder.Description);
            
            return await Task.FromResult(calcOrder);
        }

        /// <inheritdoc/>
        public async Task<CalcOrder> UpdateAsync(int id, UpdateCalcOrder updateCalcOrder)
        {
            _logger.LogInformation("Update CalcOrder with id: {0} set Name: {1} and set Description: {2}",
                                    id, updateCalcOrder.Name, updateCalcOrder.Description);
            _validations.Validate(updateCalcOrder);
            
            return await Task.FromResult(new CalcOrder());
        }

        /// <inheritdoc/>
        public async Task<CalcOrder> GetAsync(int id)
        {
            _logger.LogInformation("GetAsync with id {0}", id);
            
            return await Task.FromResult(new CalcOrder());
        }

        /// <inheritdoc/>
        public async Task<IList<CalcOrder>> GetByNameAsync(string name)
        {
            _logger.LogInformation("GetByNameAsync with name {0}", name);
            
            return await Task.FromResult(new List<CalcOrder>());
        }

        /// <inheritdoc/>
        public async Task<IList<CalcOrder>> ListAsync()
        {
            _logger.LogInformation("ListAsync");
            
            return await Task.FromResult(new List<CalcOrder>());
        }

        
    }
}
