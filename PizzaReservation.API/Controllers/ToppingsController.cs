using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaReservation.API.Models;
using PizzaReservation.Models;
using PizzaReservation.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToppingsController : ControllerBase
    {
        private readonly IToppingRepo _toppingsRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<ToppingsController> _logger;

        public ToppingsController(IToppingRepo toppingsRepo, IMapper mapper, ILogger<ToppingsController> logger)
        {
            _toppingsRepo = toppingsRepo;
            _mapper = mapper;
            _logger = logger;
        }

        #region Read (GET)

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToppingDTO>>> GetToppings()
        {
            _logger.LogInformation($"api/toppings - USED");

            var toppings = await _toppingsRepo.GetToppingsAsync();
            List<ToppingDTO> toppingsdto = new List<ToppingDTO>();
            toppings.ForEach(s => toppingsdto.Add(_mapper.Map<ToppingDTO>(s)));

            return Ok(toppingsdto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToppingDTO>> GetTopping(Guid id)
        {
            _logger.LogInformation($"api/toppings/{id} - USED");

            var topping = await _toppingsRepo.GetToppingAsync(id);
            if (topping == null)
            {
                _logger.LogInformation($"Unknown toppingID ({id} asked");
                return NotFound();
            }
            return _mapper.Map<ToppingDTO>(topping);
        }

        #endregion

        #region Create (POST)

        [HttpPost]
        public async Task<IActionResult> CreateToppingAsync([FromBody] Topping topping)
        {
            if (topping == null) BadRequest();
            if (await _toppingsRepo.CreateToppingAsync(topping)) return CreatedAtAction(nameof(GetTopping), new { id = topping.ToppingId }, topping);
            else return Conflict($"Item already exists");
        }

        #endregion

        #region Update (PUT)

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateToppingAsync(Guid id,[FromBody] Topping topping)
        {
            var search = await _toppingsRepo.GetToppingAsync(id);
            if (search == null) return BadRequest();
            topping.ToppingId = id;
            await _toppingsRepo.UpdateToppingAsync(topping);
            return NoContent();
        }

        #endregion

        #region Delete (DELETE)

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToppingAsync(Guid id)
        {
            if (await _toppingsRepo.RemoveToppingAsync(id)) return NoContent();
            else return NotFound();
        }

        #endregion
    }
}