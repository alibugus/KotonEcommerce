namespace EcommerceProject.Models
{
    public class ProductDetailsViewModel
    {
        public ProductModel Product { get; set; }
        public IEnumerable<ProductImageModel> Images { get; set; }
        public ModelInformationModel ModelInformation { get; set; }

        public IEnumerable<ProductSizeModel> ProductSizes { get; set; }
    }
}
