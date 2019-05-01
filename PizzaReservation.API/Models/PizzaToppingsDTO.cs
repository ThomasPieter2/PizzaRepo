using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaReservation.API.Models
{
    public class PizzaToppingsDTO
    {
        public PizzaDTO Pizza { get; set; }
        public ToppingDTO Topping { get; set; }
    }
}
