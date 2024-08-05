using EcommerceProject.Models;
using EcommerceProject.Repositories.Interface;
using EcommerceProject.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceProject.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public void PlaceOrder(OrderModel order, List<CartItemModel> cartItems)
        {
            // Save the order
            _orderRepository.AddOrder(order);

            // Retrieve the order ID after saving
            var savedOrder = _orderRepository.GetOrdersByUserId(order.UserId)
                                  .OrderByDescending(o => o.Id)
                                  .FirstOrDefault();

            if (savedOrder != null)
            {
                // Save order details
                foreach (var cartItem in cartItems)
                {
                    var orderDetail = new OrderDetailModel
                    {
                        OrderId = savedOrder.Id, // Use the new order ID
                        ProductId = cartItem.Product.Id,
                        Quantity = cartItem.Quantity,
                        Price = cartItem.Product.Price
                    };
                    _orderDetailRepository.AddOrderDetail(orderDetail);
                }
            }
        }

        public void AddOrderDetail(OrderDetailModel orderDetail)
        {
            _orderDetailRepository.AddOrderDetail(orderDetail);
        }

        public IEnumerable<OrderModel> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }
  
        public OrderModel GetOrderById(int orderId)
        {
            return _orderRepository.GetOrderById(orderId);
        }

        public IEnumerable<OrderModel> GetOrdersByUserId(int userId)
        {
            return _orderRepository.GetOrdersByUserId(userId);
        }
    }
}
