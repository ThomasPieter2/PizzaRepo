using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzaReservation.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        [Required (ErrorMessage = "U need a name to order!")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required (ErrorMessage = "U need an email to order!")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "U need an phone number to order!")]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string DeliveryAddress { get; set; }

        public ICollection<PizzaOrder> PizzaOrders { get; set; }
    }
}
