using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaReservation.API.Models
{
    public class SizeDTO
    {
        public Guid SizeId { get; set; }
        public string Name { get; set; }
        public int Diameter { get; set; }
        public decimal Price { get; set; }
    }
}
