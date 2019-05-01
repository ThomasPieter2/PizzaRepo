using PizzaReservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaReservation.API.Models
{
    public class PizzaDTO
    {
        public Guid PizzaId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<PizzaSizesDTO> PizzaSizes { get; set; }
        public ICollection<PizzaToppingsDTO> PizzaToppings { get; set; }
    }
}
