using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaReservation.Models;
using PizzaReservation.Models.Repositories;
using System.Collections.Generic;

namespace PizzaReservation.WebApp.ViewModels
{
    public class OrderVM
    {
        public OrderVM()
        {

        }

        public OrderVM(Order order, IOrderRepo orderRepo, IPizzaRepo pizzaRepo)
        {
            Order = order;

            SelectedPizzas = orderRepo.GetPizzasAsync(order.OrderId).Result;
            Pizzasorder = new MultiSelectList(pizzaRepo.GetPizzasAsync().Result, "PizzaId", "Name", SelectedPizzas);
            
            foreach (var pizza in pizzaRepo.GetPizzasAsync().Result)
            {
                Dictionary<Pizza, List<Size>> PizzaSizes = new Dictionary<Pizza, List<Size>>();
                PizzaSizes.Add(pizza, pizzaRepo.GetPizzaSizes(pizza.Name).Result);
            }
        }

        public Order Order { get; set; }

        public IEnumerable<Pizza> SelectedPizzas { get; set; }
        public MultiSelectList Pizzasorder { get; set; }
        public string[] SelectedPizzasString { get; set; }

        public Dictionary<Pizza, List<Size>> PizzaSizes { get; set; }
    }
}
