using EcommerceProject.Models;

namespace EcommerceProject.Services.Interface
{
    public interface IAddressService
    {
        public List<AddressModel> GetAddressesByUserId(int userId);
        public AddressModel GetAddressById(int addressId);
        void AddAddress(AddressModel address);
    }
}
