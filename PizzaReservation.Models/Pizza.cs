using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaReservation.Models
{
    public class Pizza
    {
        [Key]
        public Guid PizzaId { get; set; }

        [Required(ErrorMessage ="The pizza needs a name!")]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(300)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Display(Name = "Toppings")]
        public ICollection<PizzaTopping> PizzaToppings { get; set; }

        [Display(Name = "Sizes")]
        public ICollection<PizzaSize> PizzaSizes { get; set; }

        [Display(Name = "Orders")]
        public ICollection<PizzaOrder> PizzaOrders { get; set; }        
    }
}
