using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceProject.Models
{
    public class GuestCouponModel
    {
        public int Id { get; set; }
        public string GuestId { get; set; } // Unique ID for guest (can be stored in cookies)
        public int CouponId { get; set; }
        public CouponModel Coupon { get; set; }

        [NotMapped] // This property will not be persisted in the database
        public bool IsActiveCart { get; set; }
        public bool IsUsed { get; set; }
        public DateTime AppliedDate { get; set; }
    }
}
