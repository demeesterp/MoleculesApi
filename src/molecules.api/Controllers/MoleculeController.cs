using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using molecules.api.Filter;
using molecules.core.aggregates;
using molecules.core.services.CalcMolecules;

namespace molecules.api.Controllers
{
    /// <summary>
    /// Endpint to retrieve molecule data
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("molecule")]
    [AllowAnonymous]
    public class MoleculeController : ControllerBase
    {

        private readonly ICalcMoleculeService _service;

        private readonly ILogger<MoleculeController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service">Molecule service </param>
        /// <param name="logger">Logger</param>
        public MoleculeController(ICalcMoleculeService service, ILogger<MoleculeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Get a Molecule for the specified id
        /// </summary>
        /// <param name="id">The id of the molecule</param>
        /// <returns>The molecule corresponding to the id</returns>
        /// <response code="200">The requested molecule</response>
        /// <response code="404">There was no molecule</response>
        /// <response code="500">An unexpected error happend</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceError), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CalcMolecule>> GetAsync([FromRoute] int id)
        {
            _logger.LogInformation("Get a molecule by id:{id}", id);
            var result = await _service.GetAsync(id);
            return Ok(result);
        }


        /// <summary>
        /// Get the list of molecules b y name
        /// </summary>
        /// <param name="name">The name of the molecule</param>
        /// <returns>The list of corresponding molecules</returns>
        /// <response code="200">The requested molecule</response>
        /// <response code="500">An unexpected error happend</response>
        [HttpGet]
        [Route("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceError), StatusCodes.Status500InternalServerError)]
        public async  Task<ActionResult<List<CalcMolecule>>> GetByNameAsync([FromRoute] string name)
        {
            _logger.LogInformation("Get a molecule by name:{name}", name);
            var result = await _service.FindAllByNameAsync(name);

            return Ok(result);
        }



    }
}
