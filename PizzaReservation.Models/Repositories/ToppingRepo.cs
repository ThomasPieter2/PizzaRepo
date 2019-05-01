
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PizzaReservation.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaReservation.Models.Repositories
{
    public class ToppingRepo : IToppingRepo
    {
        private readonly PizzaReservationContext _pizzaReservationContext;
        private readonly ILogger<ToppingRepo> _logger;

        public ToppingRepo(PizzaReservationContext pizzaReservationContext, ILogger<ToppingRepo> logger)
        {
            _pizzaReservationContext = pizzaReservationContext;
            _logger = logger;
        }

        #region Read (GET)

        public async Task<List<Topping>> GetToppingsAsync()
        {
            try
            {
                return await _pizzaReservationContext.tblToppings.OrderBy(t => t.Name).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
                throw ex;
            }
        }

        public async Task<Topping> GetToppingAsync(Guid id)
        {
            try
            {
                return await _pizzaReservationContext.tblToppings.AsNoTracking().OrderBy(t => t.Name).FirstOrDefaultAsync(t => t.ToppingId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
                throw ex;
            }
        }

        #endregion

        #region Create (POST)

        public async Task<bool> CreateToppingAsync(Topping topping)
        {
            try
            {
                var search = await _pizzaReservationContext.tblToppings.FirstOrDefaultAsync(t => t.Name == topping.Name);
                if (search == null)
                {
                    topping.ToppingId = Guid.NewGuid();
                    await _pizzaReservationContext.tblToppings.AddAsync(topping);
                    await _pizzaReservationContext.SaveChangesAsync();
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        #endregion

        #region Update (PUT)

        public async Task<Topping> UpdateToppingAsync(Topping topping)
        {
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    _pizzaReservationContext.tblToppings.Update(topping);
                });

                await _pizzaReservationContext.SaveChangesAsync();
                return topping;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        #endregion

        #region DELETE (DELETE)

        public async Task<bool> RemoveToppingAsync(Guid id)
        {
            try
            {
                var topping = await _pizzaReservationContext.tblToppings.FindAsync(id);
                var pizzaToppings = await _pizzaReservationContext.tblPizzaToppings.Where(t => t.ToppingId == id).ToListAsync();

                if (topping == null) return false;
                _pizzaReservationContext.tblPizzaToppings.RemoveRange(pizzaToppings);
                _pizzaReservationContext.tblToppings.Remove(topping);

                await _pizzaReservationContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        #endregion
    }
}
