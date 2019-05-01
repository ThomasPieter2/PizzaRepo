using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaReservation.Models;
using PizzaReservation.Models.Repositories;
using PizzaReservation.WebApp.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace PizzaReservation.WebApp.Controllers
{
    [Authorize]
    public class PizzaController : Controller
    {
        private readonly IPizzaRepo _pizzaRepo;
        private readonly IToppingRepo _toppingRepo;
        private readonly ISizeRepo _sizeRepo;
        private readonly ILogger<PizzaController> _logger;

        public PizzaController(IPizzaRepo pizzaRepo, IToppingRepo toppingRepo, ISizeRepo sizeRepo, ILogger<PizzaController> logger)
        {
            _pizzaRepo = pizzaRepo;
            _toppingRepo = toppingRepo;
            _sizeRepo = sizeRepo;
            _logger = logger;
        }

        #region GET
        // GET: Pizza
        public async Task<IActionResult> Index()
        {
            var pizzas = await _pizzaRepo.GetPizzasAsync();

            return View(pizzas);
        }

        // GET: Pizza/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var pizza = await _pizzaRepo.GetPizzaAsync(id);
            PizzaVM vm = new PizzaVM(_pizzaRepo, _toppingRepo, _sizeRepo, pizza);
            return View(vm);
        }
        #endregion

        #region POST

        // GET: Pizza/Create
        public IActionResult Create()
        {
            Pizza pizza = new Pizza();
            PizzaVM vm = new PizzaVM(_pizzaRepo, _toppingRepo, _sizeRepo, pizza);
            return View(vm);
        }

        // POST: Pizza/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection frm, PizzaVM vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!await _pizzaRepo.CreatePizzaAsync(vm.Pizza, vm.SelectedToppingsString, vm.SelectedSizesString)) throw new Exception();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("CreateError", "Creating pizza failed");
                    return View(new PizzaVM(_pizzaRepo, _toppingRepo, _sizeRepo, vm.Pizza));
                }
            }
            else
            {
                vm.SelectedSizes = await _pizzaRepo.GetPizzaSizes(vm.Pizza.Name);
                vm.SelectedToppings = await _pizzaRepo.GetPizzaToppings(vm.Pizza.Name);
                return View(vm);
            }
        }
        #endregion

        #region PUT
        // GET: Pizza/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var pizza = await _pizzaRepo.GetPizzaAsync(id);

            PizzaVM vm = new PizzaVM(_pizzaRepo, _toppingRepo, _sizeRepo, pizza);
            return View(vm);
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PizzaVM vm)
        {
            try
            {
                if (!await _pizzaRepo.UpdatePizzaAsync(vm.Pizza.PizzaId, vm.Pizza, vm.SelectedSizesString, vm.SelectedToppingsString)) return View(vm);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region DELETE
        // GET: Pizza/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var pizza = await _pizzaRepo.GetPizzaAsync(id);
            return View(pizza);
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, IFormCollection collection)
        {
            try
            {
                if (!await _pizzaRepo.RemovePizzaAsync(id)) return BadRequest();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        #endregion
    }
}