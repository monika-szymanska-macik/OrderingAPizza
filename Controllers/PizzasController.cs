using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderingAPizza.ApplicationServices.API.Domain;
using System.Threading.Tasks;

namespace OrderingAPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzasController : APIControllerBase
    {
        private readonly IMediator mediator;

        public PizzasController(IMediator mediator, ILogger<PizzasController> logger) : base(mediator)
        {
            this.mediator = mediator;
                logger.LogInformation("We are in pizzas");
        }

        [HttpGet]
        [Route("")]
        public Task<IActionResult> GetAllPizzas([FromQuery] GetPizzasRequest request)
        {
            return this.HandleRequest<GetPizzasRequest, GetPizzasResponse>(request);
        }

        [HttpGet]
        [Route("{pizzaId}")]
        public Task<IActionResult> GetPizzaById([FromRoute] int pizzaId)
        {
            var request = new GetPizzaByIdRequest()
            {
                PizzaId = pizzaId
            };
            return this.HandleRequest<GetPizzaByIdRequest, GetPizzaByIdResponse>(request);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddPizza([FromBody] AddPizzaRequest request)
        {
            var response = await this.mediator.Send(request);
            return this.Ok(response);
        }

        [HttpDelete]
        [Route("{pizzaId}")]
        public async Task<IActionResult> DeletePizza([FromBody] DeletePizzaRequest request)
        {
            var response = await this.mediator.Send(request);
            return this.Ok(response);
        }

    }
}
