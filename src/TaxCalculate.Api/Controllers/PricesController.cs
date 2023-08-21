using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaxCalculate.Application.Handlers.Interfaces;
using TaxCalculate.Application.Requests;
using TaxCalculate.Application.Responses;

namespace TaxCalculate.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PricesController : ControllerBase
    {
        private readonly ICalculateValuesHandler _handler;

        public PricesController(ICalculateValuesHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        [Route("calculate")]
        [ProducesResponseType(typeof(CalculateValuesResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public IActionResult Calculate([FromBody] CalculateValuesRequest request)
        {
            return Ok(_handler.Handle(request));
        }
    }
}