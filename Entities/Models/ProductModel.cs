using EcommerceProject.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
    public string Color { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [JsonIgnore]
    public CategoryModel Category { get; set; }

    [Required]
    public int BrandId { get; set; }

    public int ModelInformationId { get; set; }

    public BrandModel Brand { get; set; }

    public string ImageUrl { get; set; }  // Main product image URL

    [JsonIgnore]
    public List<ProductImageModel> ProductImages { get; set; }  // Related images

    [JsonIgnore]
    public ICollection<ProductSizeModel> ProductSizes { get; set; }


    public double Rating { get; set; }

    public int ReviewCount { get; set; }

    public DateTime CreatedDate { get; set; }
}
