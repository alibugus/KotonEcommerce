using System.Collections.Generic;
using System.Threading.Tasks;

public interface IDiscountService
{
    Task<bool> ValidateCouponCodeAsync(string couponCode);
    Task<decimal> GetDiscountAmountAsync(string couponCode);
    Task ApplyCouponToUserAsync(int userId, string couponCode);
    Task RemoveCouponFromUserAsync(int userId, string couponCode);
    Task<bool> CanApplyCouponAsync(int userId, string couponCode);
    Task<IEnumerable<UserCouponModel>> GetUserActiveCouponsAsync(int userId); // Yeni metod
    Task<IEnumerable<UserCouponModel>> GetUserCouponsAsync(int userId); // Yeni metod
    Task<UserCouponModel> GetUserCouponAsync(int userId, string couponCode);
    Task DeactivateUserCouponAsync(int userId, string couponCode); // Güncellenmiş metod
    Task MarkCouponAsUsedAsync(int userId, string couponCode);

    Task<Dictionary<string, string>> ValidateCouponAsync(int userId, string couponCode);//hataları döndüren fonk
}
