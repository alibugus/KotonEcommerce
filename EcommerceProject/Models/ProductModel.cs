namespace EcommerceProject.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
        public string ImageUrl { get; set; }
    }
}
