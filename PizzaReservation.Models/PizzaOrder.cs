using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PizzaReservation.Models
{
    public class PizzaOrder
    {
        public Guid PizzaId { get; set; }
        public Pizza Pizza { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid SizeId { get; set; }
        public Size Size { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public double Price { get; set; }
    }
}
