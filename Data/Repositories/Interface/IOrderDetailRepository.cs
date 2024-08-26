using Data.Models;
using System.Collections.Generic;

namespace Data.Repositories.Interface
{
    public interface IOrderDetailRepository
    {
        void AddOrderDetail(OrderDetailModel orderDetail);
        void AddGuestOrderDetail(GuestOrderDetailModel orderDetail);
        IEnumerable<OrderDetailModel> GetOrderDetailsByOrderId(int orderId);
        
        IEnumerable<GuestOrderDetailModel> GetGuestOrderDetailsByOrderId(int orderId);
    }
}
