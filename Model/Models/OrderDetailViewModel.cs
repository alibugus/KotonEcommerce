namespace Data.Models
{
    public class OrderDetailViewModel
    {   
        
        public IEnumerable<OrderDetailModel> OrderDetails { get; set; }
        public IEnumerable<ProductModel> Product { get; set; }
        public OrderModel Order { get; set; }
        
    }
}
