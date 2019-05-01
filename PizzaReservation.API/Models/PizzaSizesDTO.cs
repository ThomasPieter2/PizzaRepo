using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaReservation.API.Models
{
    public class PizzaSizesDTO
    {
        public PizzaDTO Pizza { get; set; }
        public SizeDTO Size { get; set; }
    }
}
