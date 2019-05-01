using PizzaReservation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaReservation.API.Models
{
    public class OrderDTO
    {
        public Guid OrderId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DeliveryAddress { get; set; }
        public ICollection<PizzaOrderDTO> PizzaOrders { get; set; }
    }
}
