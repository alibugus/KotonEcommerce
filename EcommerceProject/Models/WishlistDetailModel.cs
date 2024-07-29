namespace EcommerceProject.Models
{
    public class WishlistDetailModel
    {
        public int Id { get; set; }
        public int WishlistId { get; set; }
        public WishlistModel Wishlist { get; set; }
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }
    }
}
