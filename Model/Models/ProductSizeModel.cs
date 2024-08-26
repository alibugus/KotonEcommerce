using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Data.Models
{
    public class ProductSizeModel
    {
         [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        

        [ForeignKey("ProductId")]
        [JsonIgnore]
        public ProductModel Product { get; set; }

        [Required]
        public string Size { get; set; }

        [Required]
        public int StockQuantity { get; set; }
    }
}