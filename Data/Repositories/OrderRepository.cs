using Data.Database;
using Data.Models;
using Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ProductModel> GetProductsByIds(IEnumerable<int> productIds)
        {
            return _context.Products.Where(p => productIds.Contains(p.Id)).ToList();
        }
        public void AddOrder(OrderModel order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public IEnumerable<OrderModel> GetAllOrders()
        {
            return _context.Orders.Include(o => o.OrderDetails).ToList();
        }

        public OrderModel GetOrderById(int orderId)
        {
            return _context.Orders.Include(o => o.OrderDetails)
                                  .FirstOrDefault(o => o.Id == orderId);
        }

        public IEnumerable<OrderModel> GetOrdersByUserId(int userId)
        {
            return _context.Orders.Include(o => o.OrderDetails)
                                  .Where(o => o.UserId == userId)
                                  .ToList();
        }
        public IEnumerable<GuestOrderModel> GetGuestOrdersByGuestId(string guestId)
        {
            return _context.GuestOrders.Include(o => o.OrderDetails)
                                  .Where(o => o.GuestId == guestId)
                                  .ToList();
        }

        public void AddGuestOrder(GuestOrderModel order)
        {
            _context.GuestOrders.Add(order);
            _context.SaveChanges();
        }
       public  IEnumerable<GuestOrderModel> GetAllGuestOrders()
        {
            return _context.GuestOrders.Include(o => o.OrderDetails).ToList();
        }
    }
}
