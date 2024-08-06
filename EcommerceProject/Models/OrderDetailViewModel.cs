namespace EcommerceProject.Models
{
    public class OrderDetailViewModel
    {   
        
        public OrderDetailModel OrderDetails { get; set; }
        public IEnumerable<ProductModel> Product { get; set; }
        public OrderModel Order { get; set; }
    }
}
