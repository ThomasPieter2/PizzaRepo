using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PizzaReservation.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaReservation.Models.Repositories
{
    public class OrderRepo : IOrderRepo
    {
        private readonly PizzaReservationContext _pizzaReservation;
        private readonly ILogger<OrderRepo> _logger;
        private readonly IPizzaRepo _pizzaRepo;
        private readonly ISizeRepo _sizeRepo;

        public OrderRepo(PizzaReservationContext pizzaReservation, ILogger<OrderRepo> logger, IPizzaRepo pizzaRepo, ISizeRepo sizeRepo)
        {
            _pizzaReservation = pizzaReservation;
            _logger = logger;
            _pizzaRepo = pizzaRepo;
            _sizeRepo = sizeRepo;
        }

        #region Read (GET)

        public async Task<List<Order>> GetOrdersAsync()
        {
            try
            {
                List<Order> listOrders = new List<Order>();
                foreach (var o in _pizzaReservation.tblOrders) listOrders.Add(await GetOrderAsync(o.OrderId));
                return listOrders;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        } //Alle orders

        public async Task<Order> GetOrderAsync(Guid id)
        {
            try
            {

                //var order = await _pizzaReservation.tblOrders.AsNoTracking().OrderBy(o => o.Name).FirstOrDefaultAsync(o => o.OrderId == id);

                //var order = await _pizzaReservation.tblOrders
                //.Include(o => o.PizzaOrders).ThenInclude(po => po.Pizza).Where(o => o.OrderId == id)
                //.Include(o => o.PizzaOrders).ThenInclude(po => po.Size).Where(o => o.OrderId == id)
                //.SingleOrDefaultAsync();

                var order = await _pizzaReservation.tblOrders
                .Include(o => o.PizzaOrders).ThenInclude(po => po.Pizza)
                .Include(o => o.PizzaOrders).ThenInclude(po => po.Size)
                .Where(o => o.OrderId == id).AsNoTracking().SingleOrDefaultAsync();

                //IEnumerable<Order> orders = await _pizzaReservation.tblOrders.ToListAsync();
                //IEnumerable<PizzaOrder> pizzaOrders = await _pizzaReservation.tblPizzaOrders.ToListAsync();
                //IEnumerable<Pizza> pizzas = await _pizzaReservation.tblPizzas.ToListAsync();
                //IEnumerable<Size> sizes = await _pizzaReservation.tblSizes.ToListAsync();

                //Order ordersbyid = (
                //    from o in orders
                //    join po in pizzaOrders on o.OrderId equals po.OrderId
                //    join p in pizzas on po.PizzaId equals p.PizzaId
                //    where o.OrderId == id

                //    select new Order()
                //    {
                //        Name = o.Name,
                //        Email = o.Email,
                //        PhoneNumber = o.PhoneNumber,
                //        OrderId = o.OrderId,
                //        DeliveryAddress = o.DeliveryAddress,
                //        PizzaOrders = o.PizzaOrders.Select(po => new PizzaOrder
                //        {
                //            PizzaId = po.PizzaId,
                //            Pizza = po.Pizza,
                //            Size = po.Size,
                //            Price = po.Price
                //        }).ToList()
                //    }
                //    ).FirstOrDefault();

                //return ordersbyid;

                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        } //1 order

        public async Task<List<Pizza>> GetPizzasAsync(Guid id)
        {
            try
            {
                return await (from o in _pizzaReservation.tblOrders
                              join po in _pizzaReservation.tblPizzaOrders on o.OrderId equals po.OrderId
                              join p in _pizzaReservation.tblPizzas on po.PizzaId equals p.PizzaId
                              where o.OrderId == id
                              select new Pizza()
                              {
                                  PizzaId = p.PizzaId,
                                  Name = p.Name
                              }).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        } //Pizza's uit order

        public async Task<List<PizzaOrder>> GetOrderPizzasAsync(Guid id)
        {
            try
            {
                return await (from o in _pizzaReservation.tblOrders
                              join po in _pizzaReservation.tblPizzaOrders on o.OrderId equals po.OrderId
                              where o.OrderId == id
                              select new PizzaOrder()
                              {
                                  Price = po.Price
                              }).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        } //Alle records tussentabel van 1 order

        public async Task<List<Size>> GetOrderSizes(Guid id)
        {
            try
            {
                return await (from o in _pizzaReservation.tblOrders
                              join po in _pizzaReservation.tblPizzaOrders on o.OrderId equals po.OrderId
                              join s in _pizzaReservation.tblSizes on po.SizeId equals s.SizeId
                              where o.OrderId == id
                              select new Size()
                              {
                                  Name = s.Name
                              }).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        } //Grootte per pizza uit een order

        #endregion

        #region Create (POST)
        public async Task<bool> CreateOrderAsync(Order order, string[] pizzas)
        {
            try
            {
                var search = await _pizzaReservation.tblOrders.FirstOrDefaultAsync(o => o.Name == order.Name);
                if (search == null)
                {
                    if (order.OrderId == Guid.Empty)
                    {
                        order.OrderId = Guid.NewGuid();
                        //foreach (var po in order.PizzaOrders) po.OrderId = order.OrderId;
                    }

                    if (pizzas != null)
                    {
                        foreach (var p in pizzas) _pizzaReservation.tblPizzaOrders.Add(new PizzaOrder { OrderId = order.OrderId, PizzaId = Guid.Parse(p) });
                    }
                    await _pizzaReservation.tblOrders.AddAsync(order);
                    await _pizzaReservation.SaveChangesAsync();
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
                throw ex;
            }
        }

        #endregion

        #region Update (PUT)

        public async Task UpdateOrderAsync(Guid id, Order order, string[] pizzas)
        {
            try
            {
                //var order = await _pizzaReservation.tblOrders.FindAsync(id);
                var pizzaOrders = await _pizzaReservation.tblPizzaOrders.Where(po => po.OrderId == id).ToListAsync();

                _pizzaReservation.tblPizzaOrders.RemoveRange(pizzaOrders);

                if (pizzas != null)
                {
                    foreach (var p in pizzas) _pizzaReservation.tblPizzaOrders.Add(new PizzaOrder { OrderId = order.OrderId, PizzaId = Guid.Parse(p) });
                }

                await Task.Factory.StartNew(() =>
                {
                    _pizzaReservation.tblOrders.Update(order);
                });

                await _pizzaReservation.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        #endregion

        #region Delete (DELETE)

        public async Task<bool> RemoveOrderAsync(Guid id)
        {
            try
            {
                var order = await _pizzaReservation.tblOrders.FindAsync(id);
                var pizzaOrders = await _pizzaReservation.tblPizzaOrders.Where(po => po.OrderId == id).ToListAsync();

                if (order == null) return false;
                _pizzaReservation.tblPizzaOrders.RemoveRange(pizzaOrders);
                _pizzaReservation.tblOrders.Remove(order);

                await _pizzaReservation.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        #endregion
    }
}
