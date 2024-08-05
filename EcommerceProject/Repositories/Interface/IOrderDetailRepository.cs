using EcommerceProject.Models;
using System.Collections.Generic;

namespace EcommerceProject.Repositories.Interface
{
    public interface IOrderDetailRepository
    {
        void AddOrderDetail(OrderDetailModel orderDetail);
        IEnumerable<OrderDetailModel> GetOrderDetailsByOrderId(int orderId);
    }
}
