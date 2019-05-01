using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaReservation.API.Models;
using PizzaReservation.Models;
using PizzaReservation.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrderRepo _ordersRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderRepo ordersRepo, IMapper mapper, ILogger<OrdersController> logger)
        {
            _ordersRepo = ordersRepo;
            _mapper = mapper;
            _logger = logger;
        }


        #region Read (GET)

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            _logger.LogInformation("api/orders - USED");

            var orders = await _ordersRepo.GetOrdersAsync();
            List<OrderDTO> ordersdto = new List<OrderDTO>();
            orders.ForEach(s => ordersdto.Add(_mapper.Map<OrderDTO>(s)));

            return ordersdto;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> getOrder(Guid id)
        {
            _logger.LogInformation($"api/orders/{id} - USED");

            var order = await _ordersRepo.GetOrderAsync(id);
            if (order == null)
            {
                _logger.LogInformation($"Unknown OrderId ({id} asked");
                return NotFound();
            }
            return _mapper.Map<OrderDTO>(order);
        }

        #endregion

        #region Create (POST)

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> CreatePizzaAsync([FromBody] Order order)
        {
            try
            {
                if (order == null)
                {
                    _logger.LogInformation("Pizza object bevat niks");
                    BadRequest();
                }
                await _ordersRepo.CreateOrderAsync(order, null);
                //_logger.LogWarning("Pizza already exist");
                //return Conflict(new { message = $"An existing record with the name '{pizza.Name}' was already found." });


                return CreatedAtAction(nameof(getOrder), new { id = order.OrderId }, order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        #endregion

        #region Update (PUT)

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderAsync(Guid id, [FromBody] Order order)
        {

            if (order != null)
            {
                if (order.OrderId == id)
                {
                    if (await _ordersRepo.GetOrderAsync(id) == null) return BadRequest();
                    order.OrderId = id;
                    await _ordersRepo.UpdateOrderAsync(id, order, null);
                    return NoContent();
                }
            }
            return BadRequest("Something is wrong");
        }

        #endregion

        #region Delete (DELETE)

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (await _ordersRepo.RemoveOrderAsync(id)) return NoContent();
            else return NotFound();
        }

        #endregion
    }
}
