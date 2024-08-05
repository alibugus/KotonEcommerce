using EcommerceProject.Database;
using EcommerceProject.Models;
using EcommerceProject.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceProject.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
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
    }
}
