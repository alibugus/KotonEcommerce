using Data.Models;

namespace Data.Services.Interface
{
    public interface IGuestCouponService
    {
        Task<CartItemViewModel> SetAmount(CartItemViewModel model);

        Task<CouponModel> GetCouponByCode(string code);
        Task<bool>ValidateCoupon(string code);
        Task<CartItemViewModel> ApplyCouponForGuest(string guestId, CartItemViewModel model);
        Task ActivateCoupon(string guestId, string couponCode);
        Task<CouponModel> GetActiveCoupon(string guestId);
        public string GetGuestId();
        Task MarkCouponAsUsedAsync(string guestId, string couponCode);
        Task DeactivateCoupon(string guestId, string couponCode);
        Task<Dictionary<string, string>> ValidateCouponAsync(string guestId, string couponCode);




    }
}
