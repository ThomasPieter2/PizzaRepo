using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaReservation.Models;
using PizzaReservation.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaReservation.WebApp.ViewModels
{
    public class PizzaOrdersVM
    {
        public Order Order { get; set; }

        // Voor OrderDetails --> IEnumerable, SELECTED omdat deze al in de database zitten
        public IEnumerable<Pizza> SelectedPizzas { get; set; }
        public IEnumerable<PizzaOrder> SelectedOrderPizzas { get; set; }
        public IEnumerable<Size> SelectedOrderSizes { get; set; }
        public IEnumerable<Size> SelectedPizzaSizes { get; set; }

        //Voor Create Order --> MultiSelectlists
        public MultiSelectList Pizzas { get; set; }               //Alle pizzas
        public string[] SelectedPizzasString { get; set; }        //helper voor HTTP 

        public MultiSelectList OrderPizzas { get; set; }
        public string[] SelectedOrderPizzasString { get; set; }   //helper voor HTTP 

        public MultiSelectList Sizes { get; set; }
        public string[] SelectedSizesString { get; set; }    //helper voor HTTP 

        public Dictionary<string, SelectList> PizzaSizes { get; set; }

        public PizzaOrdersVM(IOrderRepo orderRepo, IPizzaRepo pizzaRepo, Order order)
        {
            Order = order;

            SelectedPizzas = orderRepo.GetPizzasAsync(order.OrderId).Result;            //Voor de pizza's in een order
            SelectedOrderPizzas = orderRepo.GetOrderPizzasAsync(order.OrderId).Result;  //Voor de prijs uit de tussentabel te halen
            SelectedOrderSizes = orderRepo.GetOrderSizes(order.OrderId).Result;         //Om de grootte van de pizza op te halen

            Pizzas = new MultiSelectList(pizzaRepo.GetPizzasAsync().Result, "PizzaId", "Name", SelectedPizzas);

            PizzaSizes = new Dictionary<string, SelectList>();

            foreach (var pizza in pizzaRepo.GetPizzasAsync().Result)
            {
                PizzaSizes.Add(pizza.Name, new SelectList(pizzaRepo.GetPizzaSizes(pizza.Name).Result, "SizeId", "Name", SelectedPizzaSizes));
            }
        }

    }
}
