namespace EcommerceProject.Models
{
    public class OrderDetailModel
    { 


    public int Id { get; set; }
    public int OrderId { get; set; }
    public OrderModel Order { get; set; }
    public int ProductId { get; set; }
    public ProductModel Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
}
