using Microsoft.AspNetCore.Identity;

namespace EcommerceProject.Models
{
    public class AppUser:IdentityUser<int>
    {
        ublic string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public int ConfirmCode { get; set; }
        public ICollection<OrderModel> Orders { get; set; }
        public ICollection<CartModel> Carts { get; set; }
        public ICollection<WishlistModel> Wishlists { get; set; }
        public ICollection<ReviewModel> Reviews { get; set; }
    }
}
