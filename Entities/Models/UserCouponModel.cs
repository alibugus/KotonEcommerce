using EcommerceProject.Models;

public class UserCouponModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public AppUser User { get; set; }
    public int CouponId { get; set; }
    public CouponModel Coupon { get; set; }
    public DateTime UsedDate { get; set; }
    public bool IsActive { get; set; }
    public bool IsUsed { get; set; } // Yeni özellik
}
