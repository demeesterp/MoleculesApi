using Microsoft.AspNetCore.Mvc;
using molecules.core.aggregates;
using molecules.core.valueobjects.CalcOrder;

namespace molecules.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalcOrderController : ControllerBase
    {
        private readonly ILogger<CalcOrderController> _logger;

        public CalcOrderController(ILogger<CalcOrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("calcorders")]
        public IEnumerable<CalcOrder> List()
        {
            _logger.LogInformation("Get the list of all Calculation Orders called");
            return new List<CalcOrder>();
        }
        [HttpGet]
        [Route("calcorder/{id}")]
        public CalcOrder Get(int id)
        {
            _logger.LogInformation("Get a calculation order by id:{id}", id);
            return new CalcOrder() { Id = id};
        }

        [HttpGet]
        [Route("calcorder/{name}")]
        public IEnumerable<CalcOrder> GetByName(string name)
        {
            _logger.LogInformation("Get a calculation order by name:{name}", name);
            return new List<CalcOrder>() { new CalcOrder(name) };
        }

        [HttpPost]
        [Route("calcorder/create")]
        public CalcOrder Create([FromBody]CreateCalcOrder createCalcOrder)
        {
            _logger.LogInformation("Create a calculation order with name:{name} and description:{description}",
                                            createCalcOrder.Name,
                                                createCalcOrder.Description);
            return new CalcOrder(createCalcOrder.Name, createCalcOrder.Description);
        }

        [HttpPut]
        [Route("calcorder/update")]
        public CalcOrder Update([FromRoute]int id, [FromBody]UpdateCalcOrder updateCalcOrder)
        {
            _logger.LogInformation("Update a calculation order with name:{name} and description:{description}",
                                updateCalcOrder.Name,
                                    updateCalcOrder.Description);
            return new CalcOrder(updateCalcOrder.Name, updateCalcOrder.Description) { Id=id};
        }


    }
}