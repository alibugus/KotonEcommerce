using EcommerceProject.Models;
using System.Collections.Generic;

namespace EcommerceProject.Services.Interface
{
    public interface IOrderService
    {
        //User
        void PlaceOrder(OrderModel order, List<CartItemModel> cartItems);
        IEnumerable<OrderModel> GetAllOrders();
        OrderModel GetOrderById(int orderId);
        IEnumerable<OrderModel> GetOrdersByUserId(int userId); // Kullanıcı ID'sine göre siparişleri getirme
        void AddOrderDetail(OrderDetailModel orderDetail);
        void AddGuestOrderDetail(GuestOrderDetailModel guestOrderDetail);
        IEnumerable<ProductModel> GetOrderProducts(int orderId);
        //Guest
        Task SendHttpToUpdateQuantity(int orderId);
        void GuestPlaceOrder(GuestOrderModel order, List<CartItemModel> cartItems);
        IEnumerable<GuestOrderModel> GetAllGuestOrders();
        IEnumerable<GuestOrderModel> GetGuestOrdersByGuestId(string GuestId); // Kullanıcı ID'sine göre siparişleri getirme
    }
}
