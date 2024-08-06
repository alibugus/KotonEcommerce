using EcommerceProject.Models;
using System.Collections.Generic;

namespace EcommerceProject.Services.Interface
{
    public interface IOrderService
    {
        void PlaceOrder(OrderModel order, List<CartItemModel> cartItems);
        IEnumerable<OrderModel> GetAllOrders();
        OrderModel GetOrderById(int orderId);
        IEnumerable<OrderModel> GetOrdersByUserId(int userId); // Kullanıcı ID'sine göre siparişleri getirme
        void AddOrderDetail(OrderDetailModel orderDetail);
        IEnumerable<ProductModel> GetOrderProducts(int orderId);
    }
}
