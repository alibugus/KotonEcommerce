using Data.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Data.Database;

public class UserCouponRepository : IUserCouponRepository
{
    private readonly ApplicationDbContext _context;

    public UserCouponRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<UserCouponModel?> GetUserCouponAsync(int userId, int couponId)
    {
        return await _context.UserCoupons
            .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CouponId == couponId);
    }

    public async Task<IEnumerable<UserCouponModel>> GetUserActiveCouponsAsync(int userId)
    {
        return await _context.UserCoupons
            .Where(uc => uc.UserId == userId && uc.IsActive) // Sadece aktif kuponları getir
            .Include(uc => uc.Coupon)
            .ToListAsync() ?? new List<UserCouponModel>();
    }

    public async Task<IEnumerable<UserCouponModel>> GetUserCouponsAsync(int userId)
    {
        return await _context.UserCoupons
            .Where(uc => uc.UserId == userId)
            .ToListAsync() ?? new List<UserCouponModel>();
    }

    public async Task AddUserCouponAsync(UserCouponModel userCoupon)
    {
        if (userCoupon == null) throw new ArgumentNullException(nameof(userCoupon));

        await _context.UserCoupons.AddAsync(userCoupon);
        await SaveChangesAsync();
    }

    public async Task RemoveUserCouponAsync(int userId, int couponId)
    {
        var userCoupon = await GetUserCouponAsync(userId, couponId);
        if (userCoupon != null)
        {
            _context.UserCoupons.Remove(userCoupon);
            await SaveChangesAsync();
        }
    }

    public async Task DeactivateUserCouponAsync(int userId, string couponCode)
    {
        var userCoupon = await _context.UserCoupons
            .Include(uc => uc.Coupon)
            .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.Coupon.Code == couponCode);

        if (userCoupon != null)
        {
            userCoupon.IsActive = false;
            _context.UserCoupons.Update(userCoupon);
            await SaveChangesAsync();
        }
    }

    public async Task<UserCouponModel?> GetUserCouponAsync(int userId, string couponCode)
    {
        // Önce Coupon tablosundan CouponId'yi alalım
        var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Code == couponCode);
        if (coupon == null)
        {
            return null; // Eğer kupon bulunamazsa null döndür
        }

        // Şimdi UserCoupons tablosundan sorgulama yapalım
        return await _context.UserCoupons
            .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CouponId == coupon.Id);
    }

    public async Task UpdateUserCouponAsync(UserCouponModel userCoupon)
    {
        

        _context.UserCoupons.Update(userCoupon);
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
