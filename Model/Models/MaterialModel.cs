using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class MaterialModel
    {
        
            [Key] // Explicitly marking Id as the primary key
            public int Id { get; set; } // Ensure this is public and has a getter and setter

            public string Polyester { get; set; }
            public string Cotton { get; set; }
        
        
    }
}
