using Microsoft.AspNetCore.Mvc;
using molecules.api.Filter;
using molecules.core.aggregates;
using molecules.core.services;
using molecules.core.valueobjects.CalcOrder;

namespace molecules.api.Controllers
{
    /// <summary>
    /// Endpoint to handle all the CRUD operations for the Calculation Order
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class CalcOrderController : ControllerBase
    {
        private readonly ILogger<CalcOrderController> _logger;

        private readonly ICalcOrderService _calcOrderService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="calcOrderService">The service with the implementation</param>
        /// <param name="logger">The logger</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CalcOrderController(ICalcOrderService calcOrderService, ILogger<CalcOrderController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _calcOrderService = calcOrderService ?? throw new ArgumentNullException(nameof(calcOrderService));
        }

        /// <summary>
        /// Get the list of all CalcOrders
        /// </summary>
        /// <returns>All CalcOrders</returns>
        /// <response code="200">A list of all CalcOrders</response>
        /// <response code="204">No CalcOrders found</response>
        /// <response code="500">An unexpected error happend</response>
        [HttpGet]
        [Route("calcorders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceError), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<CalcOrder>>> ListAsync()
        {
            _logger.LogInformation("Get the list of all Calculation Orders called");
            var result = await _calcOrderService.ListAsync();
            if ( result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Get a CalcOrder for the specified id
        /// </summary>
        /// <param name="id">The id of the CalcOrder</param>
        /// <returns>The CalcOrder corresponding to the id</returns>
        /// <response code="200">A list of all CalcOrders</response>
        /// <response code="404">There was no calcorder with the specified name</response>
        /// <response code="500">An unexpected error happend</response>
        [HttpGet]
        [Route("calcorder/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceError), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CalcOrder>> GetAsync([FromRoute]int id)
        {
            _logger.LogInformation("Get a calculation order by id:{id}", id);
            var result = await _calcOrderService.GetAsync(id);
            if ( result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get the list of all CalcOrders the matches the specified name
        /// </summary>
        /// <param name="name">The name to lookup</param>
        /// <returns>The list of CalcOrders that corresponds</returns>
        /// <response code="200">A list of all CalcOrders</response>
        /// <response code="204">No CalcOrders found</response>
        /// <response code="500">An unexpected error happend</response>
        [HttpGet]
        [Route("calcorders/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceError), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<CalcOrder>>> GetByNameAsync([FromRoute] string name)
        {
            _logger.LogInformation("Get a calculation order by name:{name}", name);
            var result = await _calcOrderService.GetByNameAsync(name);
            if ( result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }
        /// <summary>
        /// Create a new CalcOrders
        /// </summary>
        /// <param name="createCalcOrder">The name and the description of the new CalcOrder</param>
        /// <returns>The newly created CalcOrder</returns>
        /// <response code="201">The CalcOrders was created</response>
        /// <response code="422">Failed to create the calcorder because the input was invalid</response>
        /// <response code="500">An unexpected error happend</response>
        [HttpPost()]
        [Route("calcorder/create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ServiceValidationError), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ServiceError), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CalcOrder>> CreateAsync([FromBody]CreateCalcOrder createCalcOrder)
        {
            _logger.LogInformation("Create a calculation order with name:{name} and description:{description}",
                                            createCalcOrder.Name,
                                                createCalcOrder.Description);

            return StatusCode(StatusCodes.Status201Created, await _calcOrderService.CreateAsync(createCalcOrder));

        }

        /// <summary>
        /// Update the CalcOrder
        /// </summary>
        /// <param name="id">The id of the CalcOrder to update</param>
        /// <param name="updateCalcOrder">The new name and the new description</param>
        /// <returns>The updated CalcOrder</returns>
        /// <response code="200">The CalcOrders was updated</response>
        /// <response code="404">No CalcOrder found for the specified id</response>
        /// <response code="422">Failed to update calcorder because the input was invalid</response>
        /// <response code="500">An unexpected error happend</response>
        [HttpPut]
        [Route("calcorder/update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceError),StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ServiceValidationError), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<CalcOrder>> UpdateAsync([FromRoute]int id, [FromBody]UpdateCalcOrder updateCalcOrder)
        {
            _logger.LogInformation("Update a calculation order with name:{name} and description:{description}",
                                updateCalcOrder.Name,
                                    updateCalcOrder.Description);
            var result = await _calcOrderService.UpdateAsync(id, updateCalcOrder);
            if ( result != null)
            {
                return Ok(result);
            }
            else
            {
               return NotFound();
            }
        }


    }
}