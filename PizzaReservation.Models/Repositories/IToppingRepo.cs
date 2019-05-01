using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaReservation.Models.Repositories
{
    public interface IToppingRepo
    {
        Task<bool> CreateToppingAsync(Topping topping);
        Task<Topping> GetToppingAsync(Guid id);
        Task<List<Topping>> GetToppingsAsync();
        Task<bool> RemoveToppingAsync(Guid id);
        Task<Topping> UpdateToppingAsync(Topping topping);
    }
}