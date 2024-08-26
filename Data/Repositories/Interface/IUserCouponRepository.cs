using Data.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

public interface IUserCouponRepository
{
    Task<UserCouponModel> GetUserCouponAsync(int userId, int couponId);
    Task<UserCouponModel> GetUserCouponAsync(int userId, string couponCode); // Yeni metod
    Task<IEnumerable<UserCouponModel>> GetUserActiveCouponsAsync(int userId); // Yeni metod
    Task<IEnumerable<UserCouponModel>> GetUserCouponsAsync(int userId);
    Task AddUserCouponAsync(UserCouponModel userCoupon);
    Task RemoveUserCouponAsync(int userId, int couponId);
    Task DeactivateUserCouponAsync(int userId, string couponCode);
    Task UpdateUserCouponAsync(UserCouponModel userCoupon); // Yeni metod
    Task SaveChangesAsync();
}

