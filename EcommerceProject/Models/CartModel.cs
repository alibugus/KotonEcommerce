namespace EcommerceProject.Models
{
    public class CartModel
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign key for AppUser
        public AppUser User { get; set; }
        public ICollection<CartDetailModel> CartDetails { get; set; }
    }
}
