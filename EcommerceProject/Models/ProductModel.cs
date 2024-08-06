using System.ComponentModel.DataAnnotations;

namespace EcommerceProject.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        [Required]
        public string Size { get; set; }

        [Required]
        public string Color { get; set; }  // Ensure this property is required and has a value

        [Required]
        public int CategoryId { get; set; }

        public CategoryModel Category { get; set; }

        [Required]
        public int BrandId { get; set; }

        public BrandModel Brand { get; set; }
        public string ImageUrl { get; set; }
    }
}
