using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaReservation.Models
{
    public class PizzaTopping
    {
        public Guid PizzaId { get; set; }
        public Pizza Pizza { get; set; }
        public Guid ToppingId { get; set; }
        public Topping Topping { get; set; }
    }
}
