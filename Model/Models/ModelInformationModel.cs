using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class ModelInformationModel
    {
        [Key] // Explicitly marking Id as the primary key
        public int Id { get; set; } // Ensure this is public and has a getter and setter

        public string Height { get; set; }
        public string JeansSize { get; set; }
        public string ShirtSize { get; set; }
        public string ChestSize { get; set; }
        public string HipSize { get; set; }
    }
}
