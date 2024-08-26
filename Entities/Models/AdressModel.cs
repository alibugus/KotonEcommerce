namespace EcommerceProject.Models
{
    public class AddressModel
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }

        public string FullAddress => $"{Address}, {City}, {State}, {Country}, {ZipCode}";
    }
}
