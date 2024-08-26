using Data.Models;
using System.Collections.Generic;

namespace Data.Repositories.Interface
{
    public interface IOrderRepository
    {
        void AddOrder(OrderModel order);
        void AddGuestOrder(GuestOrderModel order);
        IEnumerable<OrderModel> GetAllOrders();
        OrderModel GetOrderById(int orderId);
        IEnumerable<GuestOrderModel> GetAllGuestOrders();
        IEnumerable<OrderModel> GetOrdersByUserId(int userId); // Kullanıcı ID'sine göre siparişleri getirme
        IEnumerable<ProductModel> GetProductsByIds(IEnumerable<int> productIds);
        IEnumerable<GuestOrderModel> GetGuestOrdersByGuestId(string guestId); // Kullanıcı ID'sine göre siparişleri getirme
    }
}
