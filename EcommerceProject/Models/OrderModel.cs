namespace EcommerceProject.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign key for AppUser
        public AppUser User { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderDetailModel> OrderDetails { get; set; }
    }
}
