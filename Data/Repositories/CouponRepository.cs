using Data.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Database;

public class CouponRepository : ICouponRepository
{
    private readonly ApplicationDbContext _context;

    public CouponRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CouponModel> GetCouponByCodeAsync(string code)
    {
        return await _context.Coupons
            .FirstOrDefaultAsync(c => c.Code == code && c.IsActive && c.ExpiryDate > DateTime.Now);
    }

    public async Task<bool> IsCouponValidAsync(string code)
    {
        var coupon = await GetCouponByCodeAsync(code);
        return coupon != null;
    }

    public async Task<decimal> GetDiscountAmountAsync(string code)
    {
        var coupon = await GetCouponByCodeAsync(code);
        return coupon != null ? coupon.DiscountAmount : 0;
    }
    public async Task<IEnumerable<UserCouponModel>> GetUserActiveCouponsAsync(int guestId)
    {
        return await _context.UserCoupons
                             .Include(uc => uc.Coupon)
                             .Where(uc => uc.UserId == guestId && uc.Coupon.IsActive)
                             .ToListAsync();  // Kullanıcının aktif kuponlarını listele
    }
}
