namespace EcommerceProject.Models
{
    public class CartItemViewModel
    {
        public List<CartItemModel> CartItem { get; set; }
        public bool isAuthenticated { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
