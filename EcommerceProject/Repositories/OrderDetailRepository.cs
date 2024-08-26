using EcommerceProject.Database;
using EcommerceProject.Models;
using EcommerceProject.Repositories.Interface;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceProject.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddOrderDetail(OrderDetailModel orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();
        }
        public void AddGuestOrderDetail(GuestOrderDetailModel GuestorderDetail)
        {
            _context.GuestOrderDetails.Add(GuestorderDetail);
            _context.SaveChanges();
        }

        public IEnumerable<OrderDetailModel> GetOrderDetailsByOrderId(int orderId)
        {
            return _context.OrderDetails.Where(od => od.OrderId == orderId).ToList();
        }
        public IEnumerable<GuestOrderDetailModel> GetGuestOrderDetailsByOrderId(int orderId)
        {
            return _context.GuestOrderDetails.Where(od => od.OrderId == orderId).ToList();
        }
    }
}
