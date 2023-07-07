using molecules.core.aggregates;
using molecules.core.valueobjects.CalcOrder;

namespace molecules.core.services
{
    /// <summary>
    /// Service with all the business logic for CalcOrder
    /// </summary>
    public interface ICalcOrderService
    {

        /// <summary>
        /// Get the list of all ongoing CalcOrders
        /// </summary>
        /// <returns>A list of CalcOrders</returns>
        public Task<IList<CalcOrder>> ListAsync();

        /// <summary>
        /// Get the CalcOrder with the specified id
        /// </summary>
        /// <param name="id">The id of a calculation order</param>
        /// <returns>The requested CalcOrder</returns>
        public Task<CalcOrder> GetAsync(int id);

        /// <summary>
        /// Get the CalcOrders that correspond to the specified name
        /// </summary>
        /// <param name="name">The name or the name pattern to search for</param>
        /// <returns>The corresponding CalcOrders</returns>
        public Task<IList<CalcOrder>> GetByNameAsync(string name);

        /// <summary>
        /// Create a CalcOrder with name and description
        /// </summary>
        /// <param name="createCalcOrder">The name and description for the calc order</param>
        /// <returns>The created calc order</returns>
        public Task<CalcOrder> CreateAsync(CreateCalcOrder createCalcOrder);

        /// <summary>
        /// Update a CalcOrder with the specified id
        /// </summary>
        /// <param name="id">The id of the CalcOrder to be updated</param>
        /// <param name="updateCalcOrder">The name and description to be updated</param>
        /// <returns>The updated calcorder</returns>
        public Task<CalcOrder> UpdateAsync(int id, UpdateCalcOrder updateCalcOrder);


    }
}
