using EcommerceProject.Models;
using System.Collections.Generic;

namespace EcommerceProject.Repositories.Interface
{
    public interface IOrderRepository
    {
        void AddOrder(OrderModel order);
        IEnumerable<OrderModel> GetAllOrders();
        OrderModel GetOrderById(int orderId);
        IEnumerable<OrderModel> GetOrdersByUserId(int userId); // Kullanıcı ID'sine göre siparişleri getirme
    }
}
