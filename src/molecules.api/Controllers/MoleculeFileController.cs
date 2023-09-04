using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using molecules.api.Filter;
using molecules.core.services;
using System.Net.Mime;
using System.Text;

namespace molecules.api.Controllers
{
    /// <summary>
    /// Endpoint to get various files related to molecules
    /// </summary>
    [ApiController]
    [Route("moleculefiles")]
    public class MoleculeFileController : ControllerBase
    {
        private readonly ILogger<MoleculeFileController> _logger;

        private readonly IMoleculeFileService _moleculeFileService;


        /// <summary>
        /// Controller constructor
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="moleculeFileService">moleculeFileService</param>
        public MoleculeFileController(IMoleculeFileService moleculeFileService, ILogger<MoleculeFileController> logger)
        {
            _logger = logger;
            _moleculeFileService = moleculeFileService;
        }

        /// <summary>
        /// Get a Molecule for the specified id
        /// </summary>
        /// <param name="moleculeid">The id of the molecule</param>
        /// <returns>The xyz file corresponding to the moleculeid</returns>
        /// <response code="200">The requested molecule</response>
        /// <response code="404">There was no molecule</response>
        /// <response code="500">An unexpected error happend</response>
        [HttpGet]
        [Route("xyzfile/{moleculeid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ServiceError), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetXyzFileAsync([FromRoute]int moleculeid)
        {
            string fileName = $"{moleculeid}.xyz";
            string fileContent = await _moleculeFileService.GetXyzFileContentAsync(moleculeid);
            byte[] fileBytes = new byte[fileContent.Length];
            Encoding.UTF8.GetBytes(fileContent, 0, fileContent.Length, fileBytes, 0);
            return File(fileBytes, MediaTypeNames.Application.Octet, fileName);
        }
    }
}
