using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingAPizza.DataAccess;
using OrderingAPizza.DataAccess.Entities;
using OrderingAPizza.ApplicationServices.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderingAPizza.ApplicationServices.API.Domain.Models;

namespace OrderingAPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaTypeController : APIControllerBase
    {
        private readonly IMediator mediator;

        public PizzaTypeController(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllPizzaTypess([FromQuery] GetPizzaTypesRequest request)
        {
            {
                var response = await this.mediator.Send(request);
                return this.Ok(response);
            }
        }
        

        [HttpPost]
        [Route("")]
        public Task<IActionResult> AddPizzaType([FromBody] AddPizzaTypeRequest request)
        {
            return this.HandleRequest<AddPizzaTypeRequest, AddPizzaTypeResponse>(request);
        }

        [HttpDelete]
        [Route("{pizzaTypeId}")]
        public async Task<IActionResult> DeletePizzaType([FromRoute] DeletePizzaTypeRequest request)
        {
            var response = await this.mediator.Send(request);
            return this.Ok(response);
        }
        
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> PutPizzaType([FromBody] PutPizzaTypeRequest request)
        {
            var response = await this.mediator.Send(request);
            return this.Ok(response);
        }
    }
}
