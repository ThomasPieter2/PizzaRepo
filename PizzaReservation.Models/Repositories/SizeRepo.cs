using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PizzaReservation.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaReservation.Models.Repositories
{
    public class SizeRepo : ISizeRepo
    {
        private readonly PizzaReservationContext _pizzaReservationContext;
        private readonly ILogger<SizeRepo> _logger;

        public SizeRepo(PizzaReservationContext pizzaReservationContext, ILogger<SizeRepo> logger)
        {
            _pizzaReservationContext = pizzaReservationContext;
            _logger = logger;
        }

        #region Read (GET)

        public async Task<List<Size>> GetSizesAsync()
        {
            try
            {
                return await _pizzaReservationContext.tblSizes.OrderBy(s => s.Diameter).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
                throw ex;
            }
        }

        public async Task<Size> GetSizeAsync(Guid id)
        {
            try
            {
                return await _pizzaReservationContext.tblSizes.OrderBy(s => s.Diameter).AsNoTracking().FirstOrDefaultAsync(s => s.SizeId == id);
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

        public async Task<bool> CreateSizeAsync(Size size)
        {
            try
            {
                var search = await _pizzaReservationContext.tblSizes.FirstOrDefaultAsync(s => s.Name == size.Name);
                if (search == null)
                {
                    size.SizeId = Guid.NewGuid();
                    await _pizzaReservationContext.tblSizes.AddAsync(size);
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

        public async Task<Size> UpdateSizeAsync(Size size)
        {
            try
            {
                _pizzaReservationContext.tblSizes.Update(size);
                await _pizzaReservationContext.SaveChangesAsync();
                return size;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        #endregion

        #region Delete (DELETE)

        public async Task<bool> RemoveSizeAsync(Guid id)
        {
            try
            {
                var size = await _pizzaReservationContext.tblSizes.FindAsync(id);
                var pizzaSizes = await _pizzaReservationContext.tblPizzaSizes.Where(s => s.SizeId == id).ToListAsync();

                if (size == null) return false;
                _pizzaReservationContext.tblPizzaSizes.RemoveRange(pizzaSizes);
                _pizzaReservationContext.tblSizes.Remove(size);

                await _pizzaReservationContext.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
            
        }

        #endregion
    }
}
