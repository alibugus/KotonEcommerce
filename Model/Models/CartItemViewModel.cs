namespace Data.Models
{
    public class CartItemViewModel
    {
        public List<CartItemModel> CartItem { get; set; }
        public int CartItemCount { get; set; }
        public bool isAuthenticated { get; set; }
        public string CouponCode { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public Dictionary<string,string> ValidationErrors { get; set; } // Update type


    }
}
