using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaReservation.Models
{
    public class Size
    {
        [Key]
        public Guid SizeId { get; set; }

        [Required(ErrorMessage = "The size needs a name!")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The size needs a diameter!")]
        public int Diameter { get; set; }

        [Required(ErrorMessage = "The size needs a price!")]
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }

        public ICollection<PizzaSize> PizzaSizes { get; set; }
        public ICollection<PizzaOrder> PizzaOrders { get; set; }
    }
}