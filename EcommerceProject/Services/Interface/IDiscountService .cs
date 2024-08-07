namespace EcommerceProject.Services.Interface
{
    public interface IDiscountService
    {
        Task<decimal> GetDiscountAmountAsync(string couponCode);
        Task<bool> ValidateCouponCodeAsync(string couponCode);
    }
}
