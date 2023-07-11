using Microsoft.AspNetCore.Mvc;
using molecules.api.Filter;
using molecules.core.aggregates;
using molecules.core.services;
using molecules.core.valueobjects.CalcOrderItem;

namespace molecules.api.Controllers
{
    /// <summary>
    /// Endpoint to handle CRUD operation for the Calculation Order Item
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class CalcOrderItemController : ControllerBase
    {

        private readonly ILogger<CalcOrderItemController> _logger;

        private readonly ICalcOrderItemService _calcOrderItemService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="calcOrderItemService">The servoce with the busines logic in</param>
        /// <param name="logger">Logger</param>
        /// <exception cref="ArgumentNullException">input parameters should not be null</exception>
        public CalcOrderItemController(ICalcOrderItemService calcOrderItemService,
                    ILogger<CalcOrderItemController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _calcOrderItemService = calcOrderItemService ?? throw new ArgumentNullException(nameof(calcOrderItemService));
        }

        /// <summary>
        /// Create a new CalcOrderItems
        /// </summary>
        /// <param name="createCalcOrderItem">The name and the description of the new CalcOrder</param>
        /// <param name="calcOrderId">The calcorder to who the tiem belongs</param>
        /// <returns>The newly created CalcOrderItem</returns>
        /// <response code="201">The CalcOrderItem was created</response>
        /// <response code="422">Failed to create the calcorderitem because the input was invalid</response>
        /// <response code="500">An unexpected error happend</response>
        [HttpPost()]
        [Route("calcorderitem/{calcOrderId}/create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ServiceValidationError), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ServiceError), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CalcOrderItem>> CreateAsync([FromRoute]int calcOrderId, [FromBody] CreateCalcOrderItem createCalcOrderItem)
        {
            _logger.LogInformation("Create calcorder item for calcOrder {0} and molecule {1} with details {3}",
                                        calcOrderId,
                                            createCalcOrderItem.MoleculeName,
                                                createCalcOrderItem.CalcDetails);

            return StatusCode(StatusCodes.Status201Created, await _calcOrderItemService.CreateAsync(calcOrderId, createCalcOrderItem));

        }

        /// <summary>
        /// Delete a calcorder item
        /// </summary>
        /// <param name="id">The id of the calcorderitem to be deleted</param>
        /// <returns></returns>
        /// <response code="200">The CalcOrders was deleted</response>
        /// <response code="404">No CalcOrder found for the specified id</response>
        /// <response code="500">An unexpected error happend</response>
        [HttpDelete()]
        [Route("calcorderitem/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceError), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAsync([FromRoute]int id)
        {
            _logger.LogInformation("Delete calcorder item with id {0}", id);
            await _calcOrderItemService.DeleteAsync(id);
            return Ok();
        }

    }
}
