using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingAPizza.ApplicationServices.API.Domain;
using System.Threading.Tasks;

namespace OrderingAPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzasController : ControllerBase
    {
        private readonly IMediator mediator;

        public PizzasController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllPizzas([FromQuery] GetPizzasRequest request)
        {
            var response = await this.mediator.Send(request);
            return this.Ok(response);
        }
    }
}
