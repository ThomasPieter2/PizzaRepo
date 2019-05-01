using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaReservation.Models;
using PizzaReservation.Models.Repositories;
using PizzaReservation.WebApp.ViewModels;
using System;
using System.Threading.Tasks;

namespace PizzaReservation.WebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IPizzaRepo _pizzaRepo;
        private readonly ISizeRepo _sizeRepo;
        private readonly IOrderRepo _orderRepo;

        public OrderController(IPizzaRepo pizzaRepo, ISizeRepo sizeRepo, IOrderRepo orderRepo)
        {
            _pizzaRepo = pizzaRepo;
            _sizeRepo = sizeRepo;
            _orderRepo = orderRepo;
        }

        #region GET

        public async Task<IActionResult> Index()
        {
            var orders = await _orderRepo.GetOrdersAsync();
            return View(orders);
        }

        // GET: Pizza/Details/5
        public async Task<IActionResult> Details(Guid orderId)
        {
            if (orderId == null) return BadRequest();
            var order = await _orderRepo.GetOrderAsync(orderId);
            PizzaOrdersVM vm = new PizzaOrdersVM(_orderRepo, _pizzaRepo, order);
            return View(vm);
        }


        #endregion

        #region POST

        // GET: Pizza/Create
        public ActionResult Create()
        {
            Order order = new Order();
            PizzaOrdersVM vm = new PizzaOrdersVM( _orderRepo, _pizzaRepo, order);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrderVM vm)
        {
            //if (ModelState.IsValid)
            //{
            try
            {
                if (!await _orderRepo.CreateOrderAsync(vm.Order, vm.SelectedPizzasString)) throw new Exception();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(new OrderVM(vm.Order, _orderRepo, _pizzaRepo));
                throw;
            }
            //}
            //else return View(vm.Order);
        }

        #endregion

        #region DELETE

        // GET: Sizes/Delete
        public async Task<ActionResult> Delete(Guid id)
        {
            return View(await _orderRepo.GetOrderAsync(id));
        }


        // DELETE: Pizza/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Order order, IFormCollection collection)
        {
            try
            {
                // Add delete logic here
                // if (id == null) return NotFound(); //indien nullable id
                var ordercheck = await _orderRepo.GetOrderAsync(order.OrderId);
                if (order == null)
                    return NotFound();

                await _orderRepo.RemoveOrderAsync(order.OrderId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                //var errorvm = new ErrorViewModel();
                //errorvm.RequestId = Convert.ToString(id);
                //errorvm.HttpStatuscode = System.Net.HttpStatusCode.BadRequest;
                return NotFound();

            }
        }

        #endregion

    }
}