using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaReservation.Models;
using PizzaReservation.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaReservation.WebApp.ViewModels
{
    public class PizzaVM
    {
        public PizzaVM()
        {

        }

        public PizzaVM(IPizzaRepo pizzaRepo, IToppingRepo toppingRepo, ISizeRepo sizeRepo, Pizza pizza)
        {
            Pizza = pizza;

            SelectedToppings = pizzaRepo.GetPizzaToppings(pizza.Name).Result;
            Toppings = new MultiSelectList(toppingRepo.GetToppingsAsync().Result, "ToppingId", "Name", SelectedToppings);

            SelectedSizes = pizzaRepo.GetPizzaSizes(pizza.Name).Result;
            Sizes = new MultiSelectList(sizeRepo.GetSizesAsync().Result, "SizeId", "Name", SelectedSizes);
        }

        public Pizza Pizza { get; set; }

        #region Sizes
        [Display(Name = "Select the pizza sizes:")]
        public MultiSelectList Sizes { get; set; }

        public IEnumerable<Size> SelectedSizes { get; set; }

        [Required(ErrorMessage ="Choose at least one size!")]
        public string[] SelectedSizesString { get; set; }
        #endregion

        #region Toppings
        [Display(Name = "Select the pizza toppings:")]
        public MultiSelectList Toppings { get; set; }

        public IEnumerable<Topping> SelectedToppings { get; set; }

        [Required(ErrorMessage = "Choose at least one toppings!")]
        public string[] SelectedToppingsString { get; set; }
        #endregion
    }
}
