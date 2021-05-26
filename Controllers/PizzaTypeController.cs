using Microsoft.AspNetCore.Mvc;
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
    public class PizzaTypeController : ControllerBase
    {
        private readonly IRepository<PizzaType> pizzaTypeRepository;

        public PizzaTypeController(IRepository<PizzaType> pizzaTypeRepository)
        {
            this.pizzaTypeRepository = pizzaTypeRepository;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<PizzaType> GetAllPizzas() => this.pizzaTypeRepository.GetAll();

        [HttpGet]
        [Route("{pizzaTypeId}")]
        public PizzaType GetPizzaTypeById(int pizzaTypeId) => this.pizzaTypeRepository.GetById(pizzaTypeId);
    }
}
