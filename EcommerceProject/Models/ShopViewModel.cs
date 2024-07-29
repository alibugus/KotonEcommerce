using System.Collections.Generic;

namespace EcommerceProject.Models
{
    public class ShopViewModel
    {
        public IEnumerable<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
        public IEnumerable<ProductModel> Products { get; set; } = new List<ProductModel>();
        public IEnumerable<BrandModel> Brands { get; set; }
    }
}
