namespace EcommerceProject.Models
{
    public class CartDetailModel
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public CartModel Cart { get; set; }
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }

    }
}
