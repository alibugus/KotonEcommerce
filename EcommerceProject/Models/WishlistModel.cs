namespace EcommerceProject.Models
{
    public class WishlistModel
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign key for AppUser
        public AppUser User { get; set; }
        public ICollection<WishlistDetailModel> WishlistDetails { get; set; }
    }
}
