using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingAPizza.ApplicationServices.API.Domain;
using OrderiingAPizza.DataAccess;
using OrderiingAPizza.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingAPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly IMediator mediator;

        public IngredientsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllIngredients([FromQuery] GetIngredientsRequest request)
        {
            var response = await this.mediator.Send(request);
            return this.Ok(response);
        }
    }
}
