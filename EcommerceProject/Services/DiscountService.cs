using EcommerceProject.Models;
using Microsoft.AspNetCore.Identity;

public class DiscountService : IDiscountService
{
    private readonly ICouponRepository _couponRepository;
    private readonly IUserCouponRepository _userCouponRepository;
    private readonly UserManager<AppUser> _userManager;

    public DiscountService(ICouponRepository couponRepository, IUserCouponRepository userCouponRepository, UserManager<AppUser> userManager)
    {
        _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
        _userCouponRepository = userCouponRepository ?? throw new ArgumentNullException(nameof(userCouponRepository));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<IEnumerable<UserCouponModel>> GetUserActiveCouponsAsync(int userId)
    {
        return await _userCouponRepository.GetUserActiveCouponsAsync(userId);
    }

    public async Task<bool> ValidateCouponCodeAsync(string couponCode)
    {
        return await _couponRepository.IsCouponValidAsync(couponCode);
    }

    public async Task<decimal> GetDiscountAmountAsync(string couponCode)
    {
        return await _couponRepository.GetDiscountAmountAsync(couponCode);
    }

    public async Task ApplyCouponToUserAsync(int userId, string couponCode)
    {
        var coupon = await _couponRepository.GetCouponByCodeAsync(couponCode);
        if (coupon != null)
        {
            var userCoupon = new UserCouponModel
            {
                UserId = userId,
                CouponId = coupon.Id,
                UsedDate = DateTime.Now,
                IsActive = true // Kuponun aktif olduğunu belirtin
            };
            await _userCouponRepository.AddUserCouponAsync(userCoupon);
        }
    }

    public async Task RemoveCouponFromUserAsync(int userId, string couponCode)
    {
        var coupon = await _couponRepository.GetCouponByCodeAsync(couponCode);
        if (coupon != null)
        {
            await _userCouponRepository.RemoveUserCouponAsync(userId, coupon.Id);
        }
    }

    public async Task DeactivateUserCouponAsync(int userId, string couponCode)
    {
        await _userCouponRepository.DeactivateUserCouponAsync(userId, couponCode);
        
    }

    public async Task<bool> CanApplyCouponAsync(int userId, string couponCode)
    {
        var appliedCoupons = await _userCouponRepository.GetUserCouponsAsync(userId);

        // Check if the user has already applied the same coupon
        if (appliedCoupons.Any(c => c.Coupon != null && c.Coupon.Code == couponCode))
        {
            return false;
        }

        // Check if there is already an applied coupon
        if (appliedCoupons.Any(c => c.Coupon != null && c.IsActive))
        {
            return false;
        }

        return true;
    }
    public async Task<Dictionary<string, string>> ValidateCouponAsync(int userId, string couponCode)
    {
        var errors = new Dictionary<string, string>();

        var activeCoupons = (await GetUserActiveCouponsAsync(userId)).ToList();
        if (activeCoupons.Any())
        {
            errors.Add(string.Empty, "You already have an active coupon. Please remove it before applying a new one.");
            return errors;
        }

        if (string.IsNullOrEmpty(couponCode))
        {
            errors.Add(string.Empty, "Coupon code is required.");
            return errors;
        }

        var canApplyCoupon = await CanApplyCouponAsync(userId, couponCode);
        if (!canApplyCoupon)
        {
            errors.Add(string.Empty, "You cannot apply this coupon.");
            return errors;
        }

        var isValid = await ValidateCouponCodeAsync(couponCode);
        if (!isValid)
        {
            errors.Add(string.Empty, "Invalid coupon code.");
            return errors;
        }
        var coupon = await _couponRepository.GetCouponByCodeAsync(couponCode);
        // Kuponun geçerlilik tarihini kontrol edin
        if (coupon.ExpiryDate < DateTime.Now)
        {
            errors.Add(string.Empty, "This coupon has expired.");
            return errors;
        }

        var userCoupon = await GetUserCouponAsync(userId, couponCode);
        if (userCoupon != null && userCoupon.IsUsed)
        {
            errors.Add(string.Empty, "This coupon has already been used.");
            return errors;
        }
        var appliedCoupons = await _userCouponRepository.GetUserCouponsAsync(userId);


        if (couponCode == "SUMMER20" && appliedCoupons.Any(c => c.CouponId == 1))
        {
            errors.Add(string.Empty, "You cannot apply SUMMER20 if you have already used WELCOME10.");
            return errors;
        }


        if (couponCode == "WELCOME10" && appliedCoupons.Any(c => c.CouponId == 2))
        {
            errors.Add(string.Empty, "You cannot apply WELCOME10 if you have already used SUMMER20 .");
            return errors;
        }

        return errors;
    }

    public async Task<IEnumerable<UserCouponModel>> GetUserCouponsAsync(int userId)
    {
        return await _userCouponRepository.GetUserCouponsAsync(userId);
    }

    public async Task<UserCouponModel?> GetUserCouponAsync(int userId, string couponCode)
    {
        var coupon = await _couponRepository.GetCouponByCodeAsync(couponCode);
        if (coupon != null)
        {
            return await _userCouponRepository.GetUserCouponAsync(userId, coupon.Id);
        }
        return null;
    }

    public async Task MarkCouponAsUsedAsync(int userId, string couponCode)
    {
        var coupon = await _couponRepository.GetCouponByCodeAsync(couponCode);
        if (coupon != null)
        {
            var userCoupon = new UserCouponModel
            {
                UserId = userId,
                CouponId = coupon.Id,
                Coupon = coupon,
                IsUsed = true,
                UsedDate = DateTime.Now,
            };
            await _userCouponRepository.UpdateUserCouponAsync(userCoupon);

            await DeactivateUserCouponAsync(userId, couponCode);
            await RemoveCouponFromUserAsync(userId, couponCode);
        }
    }
    public async Task UpdateUserCouponAsync(UserCouponModel userCoupon)
    {
        if (userCoupon == null) throw new ArgumentNullException(nameof(userCoupon));

        await _userCouponRepository.UpdateUserCouponAsync(userCoupon);
    }

    
    
}
