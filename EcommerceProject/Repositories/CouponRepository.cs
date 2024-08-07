using EcommerceProject.Database;
using EcommerceProject.Models;
using EcommerceProject.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace EcommerceProject.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ApplicationDbContext _context;

        public CouponRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CouponModel> GetCouponByCodeAsync(string couponCode)
        {
            return await _context.Coupons
                .FirstOrDefaultAsync(c => c.Code == couponCode);
        }

        public async Task<bool> IsCouponValidAsync(string couponCode)
        {
            var coupon = await GetCouponByCodeAsync(couponCode);
            return coupon != null && coupon.IsActive;
        }

    }
}
