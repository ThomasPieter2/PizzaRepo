using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PizzaReservation.Models
{
    public class Topping
    {
        [Key]
        public Guid ToppingId { get; set; }

        [Required (ErrorMessage ="The topping needs a name!")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The topping needs a price!")]
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }

        [Required]
        public bool Vegetarian { get; set; }

        public ICollection<PizzaTopping> PizzaToppings { get; set; }
    }
}
