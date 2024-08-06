

namespace EcommerceProject.Models
{
    public class CheckOutViewModel
    {
        public List<CartItemModel> CartItems { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OrderNotes { get; set; }

        public List<AddressModel> SavedAddresses { get; set; }
        public int SelectedAddressId { get; set; }

     
    }
}
