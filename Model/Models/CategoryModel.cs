using System.Text.Json.Serialization;

namespace Data.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<ProductModel> Products { get; set; }
    }
}
