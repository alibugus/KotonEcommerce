using EcommerceProject.Repositories.Interface;
using EcommerceProject.Services.Interface;
using System.Threading.Tasks;

namespace EcommerceProject.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly ICouponRepository _couponRepository;

        public DiscountService(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public async Task<decimal> GetDiscountAmountAsync(string couponCode)
        {
            var coupon = await _couponRepository.GetCouponByCodeAsync(couponCode);
            return coupon?.DiscountAmount ?? 0m;
        }

        public async Task<bool> ValidateCouponCodeAsync(string couponCode)
        {
            return await _couponRepository.IsCouponValidAsync(couponCode);
        }
    }
}
