using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaReservation.API.Models;
using PizzaReservation.Models;
using PizzaReservation.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PizzaReservation.API.Controllers
{
    [Route("api/[controller]")]
    public class PizzasController : Controller
    {
        private readonly IPizzaRepo _pizzaRepo;
        private readonly ILogger<PizzasController> _logger;
        private readonly IMapper _mapper;

        public PizzasController(IPizzaRepo pizzaRepo, ILogger<PizzasController> logger, IMapper mapper)
        {
            _pizzaRepo = pizzaRepo;
            _logger = logger;
            _mapper = mapper;
        }

        #region Read (GET)

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaDTO>>> GetPizzasAsync()
        {
            //return await _pizzaRepo.GetPizzasAsync();

            //_logger.LogInformation("api/pizza - USED");

            var pizza = await _pizzaRepo.GetPizzasAsync();
            List<PizzaDTO> pizzadto = new List<PizzaDTO>();
            pizza.ForEach(p => pizzadto.Add(_mapper.Map<PizzaDTO>(p)));

            return pizzadto;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PizzaDTO>> GetPizzaAsync(Guid id)
        {
            //return await _pizzaRepo.GetPizzaAsync(id);

            _logger.LogInformation($"api/pizza/{id} - USED");

            var pizza = await _pizzaRepo.GetPizzaAsync(id);
            var pizzadto = _mapper.Map<PizzaDTO>(pizza);

            return pizzadto;
        }

        [HttpGet("{name}/sizes")]
        public async Task<ActionResult<List<SizeDTO>>> GetPizzaSizeAsync(string name)
        {
            var sizes = await _pizzaRepo.GetPizzaSizes(name);
            List<SizeDTO> sizesdto = new List<SizeDTO>();
            sizes.ForEach(t => sizesdto.Add(_mapper.Map<SizeDTO>(t)));
            return sizesdto;
        }

        [HttpGet("{name}/toppings")]
        public async Task<ActionResult<List<ToppingDTO>>> GetPizzaToppingsAsync(string name)
        {
            var sizes = await _pizzaRepo.GetPizzaToppings(name);
            List<ToppingDTO> toppingdto = new List<ToppingDTO>();
            sizes.ForEach(t => toppingdto.Add(_mapper.Map<ToppingDTO>(t)));
            return toppingdto;
        }

        #endregion

        #region Create (POST)

        [HttpPost]
        public async Task<ActionResult<PizzaDTO>> CreatePizzaAsync([FromBody] Pizza pizza)
        {
            if (pizza == null) return BadRequest();
            if (await _pizzaRepo.CreatePizzaAsync(pizza, null, null)) return CreatedAtAction(nameof(GetPizzaAsync), new { id = pizza.PizzaId }, pizza);
            else return Conflict("Item already exists or some data is missing/wrong.");
        }

        #endregion

        #region Update (PUT)

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePizzaAsync(Guid id, [FromBody] Pizza pizza)
        {
            if (pizza != null)
            {
                if (pizza.PizzaId == id)
                {
                    if (await _pizzaRepo.GetPizzaAsync(id) == null) return BadRequest();
                    pizza.PizzaId = id;
                    if(!await _pizzaRepo.UpdatePizzaAsync(id, pizza, null, null)) return BadRequest();
                    return NoContent();
                }
            }
            return BadRequest("Something is wrong");
        }

        #endregion

        #region Delete (DELETE)

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizzaAsync(Guid id)
        {
            if (await _pizzaRepo.RemovePizzaAsync(id)) return NoContent();
            else return NotFound();
        }

        #endregion
    }
}
