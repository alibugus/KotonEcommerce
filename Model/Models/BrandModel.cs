namespace Data.Models
{
    public class BrandModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductModel> Products { get; set; }
    }
}
