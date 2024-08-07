namespace EcommerceProject.Models
{
    public class CouponModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool IsActive { get; set; }  // Kuponun geçerli olup olmadığını kontrol eder
        public DateTime ExpiryDate { get; set; }
    }
}
