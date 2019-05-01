using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaReservation.Models;
using PizzaReservation.Models.Repositories;

namespace PizzaReservation.WebApp.Controllers
{
    [Authorize]
    public class ToppingController : Controller
    {
        private readonly IToppingRepo _toppingRepo;

        public ToppingController(IToppingRepo toppingRepo)
        {
            _toppingRepo = toppingRepo;
        }

        public async Task<IActionResult> Index()
        {
            var toppings = await _toppingRepo.GetToppingsAsync();
            return View(toppings.OrderBy(t => t.Name).ToList());
        }

        // GET: Toppings/Details/5
        public async Task<ActionResult> Details(Guid toppingId)
        {
            var topping = await _toppingRepo.GetToppingAsync(toppingId);
            return View(topping);
        }

        // GET: Toppings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Toppings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Vegetarian,Price")]IFormCollection collection, [Bind("Name,Vegetarian,Price")]Topping topping)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!await _toppingRepo.CreateToppingAsync(topping)) throw new Exception();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.TryAddModelError("CreateFailed", "Unable to create topping");
                    return View(topping);
                }
            }
            else
            {
                return View(topping);
            }
        }

        // GET: Toppings/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            return View(await _toppingRepo.GetToppingAsync(id));
        }

        // POST: Toppings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ToppingId,Name,Vegetarian,Price")]IFormCollection collection, [Bind("ToppingId,Name,Vegetarian,Price")]Topping topping)
        {
            if (id != topping.ToppingId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    await _toppingRepo.UpdateToppingAsync(topping);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.TryAddModelError("EditFailed", "Unable to edit topping");
                    return View(topping);
                }
            }
            else return View(topping);
        }

        // GET: Sizes/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            return View(await _toppingRepo.GetToppingAsync(id));
        }

        // POST: Sizes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, IFormCollection collection)
        {
            try
            {
                await _toppingRepo.RemoveToppingAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}