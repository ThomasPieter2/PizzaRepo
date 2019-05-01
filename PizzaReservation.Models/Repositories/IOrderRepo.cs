using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaReservation.Models.Repositories
{
    public interface IOrderRepo
    {
        Task<bool> CreateOrderAsync(Order order, string[] pizzas);
        Task<Order> GetOrderAsync(Guid id);
        Task<List<PizzaOrder>> GetOrderPizzasAsync(Guid id);
        Task<List<Order>> GetOrdersAsync();
        Task<List<Size>> GetOrderSizes(Guid id);
        Task<List<Pizza>> GetPizzasAsync(Guid id);
        Task<bool> RemoveOrderAsync(Guid id);
        Task UpdateOrderAsync(Guid id, Order order, string[] pizzas);
    }
}