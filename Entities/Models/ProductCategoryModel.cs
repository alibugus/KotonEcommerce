using System.ComponentModel.DataAnnotations;

namespace EcommerceProject.Models
{
    public class ProductCategoryModel
    {
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }

        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
    }
}
