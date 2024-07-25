using System.Collections.Generic;

namespace EcommerceProject.Models
{
    public class HomeViewModel
    {
        public IEnumerable<ProductModel> Products { get; set; }
        public IEnumerable<CategoryModel> Categories { get; set; }
    }
}
