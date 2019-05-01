using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaReservation.Models.Repositories
{
    public interface IPizzaRepo
    {
        Task<bool> CreatePizzaAsync(Pizza pizza, string[] topping, string[] sizes);
        Task<Pizza> GetPizzaAsync(Guid id);
        Task<List<Pizza>> GetPizzasAsync();
        Task<List<Size>> GetPizzaSizes(string name);
        Task<List<Topping>> GetPizzaToppings(string name);
        Task<bool> RemovePizzaAsync(Guid id);
        Task<bool> UpdatePizzaAsync(Guid id, Pizza pizza, string[] sizes, string[] toppings);
    }
}