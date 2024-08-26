using EcommerceProject.Models;
using System.Threading.Tasks;

public interface ICouponRepository
{
    Task<CouponModel> GetCouponByCodeAsync(string code);
    Task<bool> IsCouponValidAsync(string code);
    Task<decimal> GetDiscountAmountAsync(string code);
    Task<IEnumerable<UserCouponModel>> GetUserActiveCouponsAsync(int guestId);
}
