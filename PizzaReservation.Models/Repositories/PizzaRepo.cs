using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PizzaReservation.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaReservation.Models.Repositories
{
    public class PizzaRepo : IPizzaRepo
    {
        private readonly PizzaReservationContext _pizzaReservation;
        private readonly ILogger<PizzaRepo> _logger;
        private readonly ISizeRepo _sizeRepo;
        private readonly IToppingRepo _toppingRepo;

        public PizzaRepo(PizzaReservationContext pizzaReservation, ILogger<PizzaRepo> logger, ISizeRepo sizeRepo, IToppingRepo toppingRepo)
        {
            _pizzaReservation = pizzaReservation;
            _logger = logger;
            _sizeRepo = sizeRepo;
            _toppingRepo = toppingRepo;
        }

        #region Read (GET)

        public async Task<List<Pizza>> GetPizzasAsync()
        {
            try
            {
                var pizzas = await _pizzaReservation.tblPizzas
                    .Include(p => p.PizzaSizes).ThenInclude(ps => ps.Size)
                    .Include(p => p.PizzaToppings).ThenInclude(pt => pt.Topping)
                    .OrderBy(p => p.PizzaId)
                    .AsNoTracking().ToListAsync();

                return pizzas;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        public async Task<Pizza> GetPizzaAsync(Guid id)
        {
            try
            {
                var pizza = await _pizzaReservation.tblPizzas
                    .Include(p => p.PizzaSizes).ThenInclude(ps => ps.Size)
                    .Include(p => p.PizzaToppings).ThenInclude(pt => pt.Topping)
                    .OrderBy(p => p.PizzaId)
                    .Where(p => p.PizzaId == id)
                    .AsNoTracking().FirstOrDefaultAsync();

                return pizza;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        public async Task<List<Size>> GetPizzaSizes(string name)
        {
            try
            {
                return await (from p in _pizzaReservation.tblPizzas
                              join ps in _pizzaReservation.tblPizzaSizes on p.PizzaId equals ps.PizzaId
                              join s in _pizzaReservation.tblSizes on ps.SizeId equals s.SizeId
                              where p.Name == name
                              select new Size()
                              {
                                  Name = s.Name,
                                  Diameter = s.Diameter,
                                  Price = s.Price,
                                  SizeId = s.SizeId
                              }).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        public async Task<List<Topping>> GetPizzaToppings(string name)
        {
            try
            {
                return await (from p in _pizzaReservation.tblPizzas
                              join pt in _pizzaReservation.tblPizzaToppings on p.PizzaId equals pt.PizzaId
                              join t in _pizzaReservation.tblToppings on pt.ToppingId equals t.ToppingId
                              where p.Name == name
                              select new Topping()
                              {
                                  Name = t.Name,
                                  Price = t.Price,
                                  ToppingId = t.ToppingId,
                                  Vegetarian = t.Vegetarian
                              }).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        #endregion

        #region Create (POST)

        public async Task<bool> CreatePizzaAsync(Pizza pizza, string[] toppings, string[] sizes)
        {
            try
            {
                var piz = await _pizzaReservation.tblPizzas.Include(p => p.PizzaSizes).Include(p => p.PizzaToppings).FirstOrDefaultAsync(p => p.Name == pizza.Name);
                if (piz == null)
                {
                    if (pizza.PizzaId == Guid.Empty) pizza.PizzaId = Guid.NewGuid();

                    if (sizes != null & toppings != null)
                    {
                        foreach (var s in sizes) _pizzaReservation.tblPizzaSizes.Add(new PizzaSize { PizzaId = pizza.PizzaId, SizeId = Guid.Parse(s) });
                        foreach (var t in toppings) _pizzaReservation.tblPizzaToppings.Add(new PizzaTopping { PizzaId = pizza.PizzaId, ToppingId = Guid.Parse(t) });
                    }

                    await _pizzaReservation.tblPizzas.AddAsync(pizza);
                    await _pizzaReservation.SaveChangesAsync();
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        #endregion

        #region Update (PUT)

        public async Task<bool> UpdatePizzaAsync(Guid id, Pizza pizza, string[] sizes, string[] toppings)
        {
            try
            {
                if (sizes != null & toppings != null)
                {
                    var piz = await _pizzaReservation.tblPizzas.Include(p => p.PizzaSizes).Include(p => p.PizzaToppings).FirstOrDefaultAsync(p => p.PizzaId == id); //bestaande pizza

                    piz.PizzaSizes.Clear();
                    foreach (var s in sizes) piz.PizzaSizes.Add(new PizzaSize { PizzaId = id, SizeId = Guid.Parse(s) });

                    piz.PizzaToppings.Clear();
                    foreach (var t in toppings) piz.PizzaToppings.Add(new PizzaTopping { PizzaId = id, ToppingId = Guid.Parse(t) });

                    _pizzaReservation.Update(piz);
                }
                else _pizzaReservation.Update(pizza);

                await _pizzaReservation.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
                throw ex;
            }
        }

        #endregion

        #region Delete (DELETE)

        public async Task<bool> RemovePizzaAsync(Guid id)
        {
            try
            {
                var piz = await _pizzaReservation.tblPizzas.Include(p => p.PizzaSizes).Include(p => p.PizzaToppings).Include(p => p.PizzaOrders).FirstOrDefaultAsync(p => p.PizzaId == id);
                if (piz == null) return false;
                _pizzaReservation.Remove(piz);

                await _pizzaReservation.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        #endregion
    }
}
