namespace EcommerceProject.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } // Optional
        public int StockQuantity { get; set; } // Optional
        public string Size { get; set; } // Optional
        public string Color { get; set; } // Optional

        // Foreign Key for Category
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }

        // Foreign Key for Brand
        public int BrandId { get; set; }
        public BrandModel Brand { get; set; }

        public string ImageUrl { get; set; }
    }
}
