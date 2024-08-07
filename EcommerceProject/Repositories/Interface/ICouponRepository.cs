using EcommerceProject.Models;

namespace EcommerceProject.Repositories.Interface
{
    public interface ICouponRepository
    {
        Task<CouponModel> GetCouponByCodeAsync(string couponCode);
        Task<bool> IsCouponValidAsync(string couponCode);
    }
}
