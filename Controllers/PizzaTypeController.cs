using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderiingAPizza.DataAccess;
using OrderiingAPizza.DataAccess.Entities;
using OrderingAPizza.ApplicationServices.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingAPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaTypeController : ControllerBase
    {
        private readonly IMediator mediator;

        public PizzaTypeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllPizzaTypess([FromQuery] GetPizzaTypeRequest request)
        {
            {
                var response = await this.mediator.Send(request);
                return this.Ok(response);
            }
        }
        

        //[HttpGet]
        //[Route("{pizzaTypeId}")]
        //public PizzaType GetPizzaTypeById(int pizzaTypeId) => this.pizzaTypeRepository.GetById(pizzaTypeId);
    }
}
