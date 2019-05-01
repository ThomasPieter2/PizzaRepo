using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaReservation.Models
{
    public class PizzaSize
    {
        public Guid PizzaId { get; set; }
        public Pizza Pizza { get; set; }
        public Guid SizeId { get; set; }
        public Size Size { get; set; }
    }
}
