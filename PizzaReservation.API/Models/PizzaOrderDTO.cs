using PizzaReservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaReservation.API.Models
{
    public class PizzaOrderDTO
    {
        public PizzaDTO Pizza { get; set; }        
        public SizeDTO Size { get; set; }        
        public double Price { get; set; }
    }
}
