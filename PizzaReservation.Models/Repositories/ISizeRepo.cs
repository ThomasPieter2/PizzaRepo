using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaReservation.Models.Repositories
{
    public interface ISizeRepo
    {
        Task<bool> CreateSizeAsync(Size size);
        Task<Size> GetSizeAsync(Guid id);
        Task<List<Size>> GetSizesAsync();
        Task<bool> RemoveSizeAsync(Guid id);
        Task<Size> UpdateSizeAsync(Size size);
    }
}