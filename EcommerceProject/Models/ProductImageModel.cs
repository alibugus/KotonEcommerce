namespace EcommerceProject.Models
{
    public class ProductImageModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
        public ProductModel Product { get; set; }
    }
}
