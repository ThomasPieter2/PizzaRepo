using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaReservation.Models;
using PizzaReservation.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaReservation.WebApp.Controllers
{
    [Authorize]
    public class SizeController : Controller
    {
        private readonly ISizeRepo _sizeRepo;

        public SizeController(ISizeRepo sizeRepo)
        {
            _sizeRepo = sizeRepo;
        }

        #region GET
        // GET: Sizes
        public async Task<IActionResult> Index()
        {
            var sizes = await _sizeRepo.GetSizesAsync();
            return View(sizes.OrderBy(s => s.Diameter).ToList());
        }

        // GET: Sizes/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var size = await _sizeRepo.GetSizeAsync(id);
            return View(size);
        }
        #endregion

        #region POST
        // GET: Sizes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sizes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Name,Diameter,Price")]IFormCollection collection, [Bind("Name,Diameter,Price")]Size size)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!await _sizeRepo.CreateSizeAsync(size)) throw new Exception();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.TryAddModelError("CreateFailed", "Unable to create size");
                    return View(size);
                }
            }
            else
            {
                return View(size);
            }
        }
        #endregion

        #region EDIT
        // GET: Sizes/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var size = await _sizeRepo.GetSizeAsync(id);
            return View(size);
        }

        // POST: Sizes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, [Bind("SizeId,Name,Diameter,Price")]IFormCollection collection, [Bind("SizeId,Name,Diameter,Price")]Size size)
        {
            if (id != size.SizeId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _sizeRepo.UpdateSizeAsync(size);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.TryAddModelError("EditFailed", "Unable to edit size");
                    return View(size);
                }
            }
            else
            {
                return View(size);
            }
        }
        #endregion

        #region DELETE
        // GET: Sizes/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            return View(await _sizeRepo.GetSizeAsync(id));
        }

        // POST: Sizes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, IFormCollection collection)
        {
            try
            {
                await _sizeRepo.RemoveSizeAsync(id);

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